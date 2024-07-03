using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.Extensions.Options;
using Rong.Volo.Abp.CodeGenerator.Vue.Attributes;
using Rong.Volo.Abp.CodeGenerator.Vue.Models;
using Rong.Volo.Abp.CodeGenerator.Vue.TemplateHelpers.Vbens;
using Volo.Abp;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Reflection;
using Volo.Abp.TextTemplating;

namespace Rong.Volo.Abp.CodeGenerator.Vue
{
    /// <summary>
    /// 代码生成器帮助器
    /// </summary>
    public class CodeGeneratorVueStore : ITransientDependency
    {
        protected CodeGeneratorVueOptions Options;

        protected CodeGeneratorVueModelHelper CodeGeneratorModelStore;
        protected ITemplateDefinitionManager TemplateDefinitionManager;
        protected ITemplateRenderer TemplateRenderer;

        public CodeGeneratorVueStore(
            IOptions<CodeGeneratorVueOptions> options,
            CodeGeneratorVueModelHelper codeGeneratorModelStore,
            ITemplateDefinitionManager templateDefinitionManager,
            ITemplateRenderer templateRenderer)
        {
            TemplateRenderer = templateRenderer;
            TemplateDefinitionManager = templateDefinitionManager;
            CodeGeneratorModelStore = codeGeneratorModelStore;
            Options = options.Value;
        }

        /// <summary>
        /// 开始代码生成
        /// </summary>
        /// <param name="entityBaseType">实体基类。如 typeof(IEntity)</param>
        /// <param name="entityModule">实体所在模块。如 typeof(xxxDomainModule)</param>
        /// <param name="dtoModule">输入输出dto所在模块。如 typeof(xxxApplicationContractsModule)</param>
        /// <param name="apiRootPath">api根路径（如用于生成 getList api路径： '/api/<paramref name="apiRootPath"/>/实体/getList',）</param>
        /// <param name="saveRootPath">要保存到的根目录。如 E:\\MY\\Rong.CodeGenerator\\vue\\vben_demo</param>
        /// <returns></returns>
        public virtual async Task StartAsync(Type entityBaseType, Type entityModule, Type dtoModule, string apiRootPath, string saveRootPath)
        {
            List<TemplateVueModel> list = new List<TemplateVueModel>();

            var entitys = entityModule.Assembly.GetTypes()
                .Where(entityBaseType.IsAssignableFrom);

            var dtos = dtoModule.Assembly.GetTypes();

            foreach (var entity in entitys)
            {
                var name = entity.Name;

                //实体显示名称
                var displayName = GetEntityDisplayName(entity);
                //权限
                var permissionModel = GetTemplateVuePermissionModel(dtos, entity);
                //实体dto
                var entityTypeModel = GetTemplateVueDtoType(dtos, entity);

                var item = new TemplateVueModel(name, displayName, entityTypeModel, permissionModel);
                list.Add(item);
            }

            //开始生成
            await StartAsync(list, apiRootPath, saveRootPath);
        }

        /// <summary>
        /// 开始代码生成
        /// </summary>
        /// <param name="entities">实体模型集合</param>
        /// <param name="apiRootPath">api根路径（如用于生成 getList api路径： '/api/<paramref name="apiRootPath"/>/实体/getList',）</param>
        /// <param name="saveRootPath">要保存到的根目录。如 E:\\MY\\Rong.CodeGenerator\\vue\\vben_demo</param>
        /// <returns></returns>
        public virtual async Task StartAsync(List<TemplateVueModel> entities, string apiRootPath, string saveRootPath)
        {
            Check.NotNull(entities, nameof(entities));
            Check.NotNullOrWhiteSpace(saveRootPath, nameof(saveRootPath));

            var entitys = entities.GroupBy(a => a.Entity)
                .Where(a => a.Count() > 1)
                .Select(a => a.Key)
                .ToList();
            if (entitys.Any())
            {
                throw new UserFriendlyException($"以下名称重复：{entitys.JoinAsString(",")}");
            }

            var tasks = new List<Task>();

            foreach (var item in entities)
            {
                item.EntityCase = item.Entity.ToCamelCase();
                item.ApiRootPath ??= apiRootPath;
                item.Options = Options;

                tasks.Add(Task.Run(async () => { await GenerateForEntityAsync(item, saveRootPath); }));
            }

            //等待任务执行完毕
            await Task.WhenAll(tasks);
        }

        /// <summary>
        /// 生成实体相关类
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="saveRootPath">要保存到的根目录</param>
        /// <returns></returns>
        protected virtual async Task GenerateForEntityAsync(TemplateVueModel entity, string saveRootPath)
        {
            string[] templates = ReflectionHelper.GetPublicConstantsRecursively(typeof(CodeGeneratorVueVbenTemplateNames));

            foreach (var template in templates)
            {
                //模板不存在
                var temp = await TemplateDefinitionManager.GetAsync(template);
                if (temp == null)
                {
                    continue;
                }

                string name = temp.Properties["name"].ToString().Replace("xxx", entity.EntityCase);
                string path = temp.Properties["path"].ToString().Replace("xxx", entity.EntityCase)
                    .Replace("$rootPath", saveRootPath?.TrimEnd('\\', '/'));

                //获取模值
                var model = CodeGeneratorModelStore.GetModel(entity, template);

                //保存
                await SaveAsync(model, template, name, path);
            }

        }

        /// <summary>
        /// 保存文件
        /// </summary>
        /// <returns></returns>
        protected virtual async Task SaveAsync(object? model, string template, string name, string path)
        {
            //模板不存在
            var temp = await TemplateDefinitionManager.GetAsync(template);
            if (temp == null)
            {
                return;
            }

            //保存到的文件夹
            string saveToDirectoryPath = Path.Combine(Directory.GetCurrentDirectory(), path);
            if (!Directory.Exists(saveToDirectoryPath))
            {
                Directory.CreateDirectory(saveToDirectoryPath);
            }

            //保存的文件
            string saveName = Path.Combine(saveToDirectoryPath, name);

            //已存在文件，则不生成
            if (File.Exists(saveName))
            {
                return;
            }

            //渲染生成
            var result = await TemplateRenderer.RenderAsync(
                template,
                model
            );

            //保存文件
            await SaveToFileAsync(result, saveName);
        }

        /// <summary>
        /// 保存为文件
        /// </summary>
        /// <param name="renderResult">字节</param>
        /// <param name="fileName">文件名称（含路径）</param>
        /// <returns></returns>
        protected virtual async Task SaveToFileAsync(string renderResult, string fileName)
        {
            byte[] buffer = renderResult.GetBytes(Encoding.UTF8);
            using FileStream fs = new FileStream(fileName, FileMode.Create, FileAccess.ReadWrite);
            await fs.WriteAsync(buffer, 0, buffer.Length);
        }

        /// <summary>
        /// 获取dto类型
        /// </summary>
        /// <param name="dtoTypes"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        protected virtual TemplateVueDtoType GetTemplateVueDtoType(Type[] dtoTypes, Type entity)
        {
            var name = entity.Name;
            var page = dtoTypes.FirstOrDefault(a => a.Name == $"{name}PageOutput");
            var search = dtoTypes.FirstOrDefault(a => a.Name == $"{name}PageSearchInput");
            var create = dtoTypes.FirstOrDefault(a => a.Name == $"{name}CreateInput");
            var update = dtoTypes.FirstOrDefault(a => a.Name == $"{name}UpdateInput");
            var detail = dtoTypes.FirstOrDefault(a => a.Name == $"{name}DetailOutput");
            var permission = dtoTypes.FirstOrDefault(a => a.Name == $"{name}Permissions");
            string? permissionGroup = permission?.GetField("GroupName")?.GetValue(null)?.ToString();

            var entityTypeModel = new TemplateVueDtoType()
            {
                SearchType = search,
                PageType = page,
                DetailType = detail,
                CreateType = create,
                UpdateType = update,
            };
            return entityTypeModel;

        }

        /// <summary>
        /// 获取dto类型
        /// </summary>
        /// <param name="dtoTypes"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        protected virtual TemplateVuePermissionModel GetTemplateVuePermissionModel(Type[] dtoTypes, Type entity)
        {
            string name = entity.Name;
            var permission = dtoTypes.FirstOrDefault(a => a.Name == $"{name}Permissions");
            string? permissionGroup = permission?.GetField("GroupName")?.GetValue(null)?.ToString();

            var permissionModel = new TemplateVuePermissionModel(name, permissionGroup);

            return permissionModel;
        }

        /// <summary>
        /// 获取实体显示名称
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        protected virtual string GetEntityDisplayName(Type entity)
        {
            var displayName = entity.GetCustomAttribute<TableAttribute>()?.Name ?? entity.Name;

            return displayName;
        }
    }

}