using Microsoft.Extensions.Options;
using Rong.Volo.Abp.CodeGenerator.Vue.Models;
using Rong.Volo.Abp.CodeGenerator.Vue.TemplateHelpers.Vbens;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Reflection;
using Volo.Abp.TextTemplating;

namespace Rong.Volo.Abp.CodeGenerator.Vue
{
    /// <summary>
    /// 代码生成器帮助器
    /// </summary>
    public class RongVoloAbpCodeGeneratorVueStore : ITransientDependency
    {
        protected RongVoloAbpCodeGeneratorVueOptions Options;

        protected RongVoloAbpCodeGeneratorVueModelHelper CodeGeneratorModelStore;
        protected ITemplateDefinitionManager TemplateDefinitionManager;
        protected ITemplateRenderer TemplateRenderer;

        public RongVoloAbpCodeGeneratorVueStore(
            IOptions<RongVoloAbpCodeGeneratorVueOptions> options,
            RongVoloAbpCodeGeneratorVueModelHelper codeGeneratorModelStore,
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
        /// <param name="ignoreEntityTypes">要忽略的实体类型</param>
        /// <returns></returns>
        public virtual async Task StartAsync(Type entityBaseType, Type entityModule, Type dtoModule, string apiRootPath,
            string saveRootPath, Type[]? ignoreEntityTypes = null)
        {
            List<TemplateVueModel> list = new List<TemplateVueModel>();

            var entityTypes = entityModule.Assembly.GetTypes()
                .Where(entityBaseType.IsAssignableFrom);

            var dtoTypes = dtoModule.Assembly.GetTypes();

            foreach (var entityType in entityTypes)
            {
                if (EntityShouldIgnore(ignoreEntityTypes, entityType))
                {
                    continue;
                }

                var name = entityType.Name;

                //实体显示名称
                var displayName = GetEntityDisplayName(entityType);
                //权限
                var permissionModel = GetTemplateVuePermissionModel(dtoTypes, entityType);
                //实体dto
                var entityDtoTypeModel = GetTemplateVueDtoType(dtoTypes, entityType);

                var item = new TemplateVueModel(name, displayName, entityDtoTypeModel, permissionModel);
                list.Add(item);
            }

            //开始生成
            await StartAsync(list, apiRootPath, saveRootPath, null);
        }

        /// <summary>
        /// 开始代码生成
        /// </summary>
        /// <param name="entities">实体模型集合</param>
        /// <param name="apiRootPath">api根路径（如用于生成 getList api路径： '/api/<paramref name="apiRootPath"/>/实体/getList',）</param>
        /// <param name="saveRootPath">要保存到的根目录。如 E:\\MY\\Rong.CodeGenerator\\vue\\vben_demo</param>
        /// <param name="ignoreEntity">要忽略的是实体</param>
        /// <returns></returns>
        public virtual async Task StartAsync(List<TemplateVueModel> entities, string apiRootPath, string saveRootPath, string[]? ignoreEntity = null)
        {
            Check.NotNull(entities, nameof(entities));
            Check.NotNullOrWhiteSpace(saveRootPath, nameof(saveRootPath));

            var entitys = entities
                .Where(a => !string.IsNullOrWhiteSpace(a.Entity))
                .GroupBy(a => a.Entity)
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
                if (EntityShouldIgnore(ignoreEntity, item.Entity))
                {
                    continue;
                }

                item.EntityCase = item.Entity.ToCamelCase();
                if (string.IsNullOrWhiteSpace(item.ApiRootPath))
                {
                    item.ApiRootPath = apiRootPath;
                }

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
            string[] templates =
                ReflectionHelper.GetPublicConstantsRecursively(typeof(RongVoloAbpVueVbenTemplateNames));

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
        /// <param name="entityType"></param>
        /// <returns></returns>
        protected virtual TemplateVueDtoType GetTemplateVueDtoType(Type[] dtoTypes, Type entityType)
        {
            var name = entityType.Name;

            var baseOut = dtoTypes.FirstOrDefault(a => a.Name == $"{name}BaseOutput");
            var page = dtoTypes.FirstOrDefault(a => a.Name == $"{name}PageOutput");
            var detail = dtoTypes.FirstOrDefault(a => a.Name == $"{name}DetailOutput");

            var createOrUpdateBaseType = dtoTypes.FirstOrDefault(a => a.Name == $"{name}CreateOrUpdateInputBase");
            var create = dtoTypes.FirstOrDefault(a => a.Name == $"{name}CreateInput");
            var update = dtoTypes.FirstOrDefault(a => a.Name == $"{name}UpdateInput");

            var search = dtoTypes.FirstOrDefault(a => a.Name == $"{name}PageSearchInput");


            var entityTypeModel = new TemplateVueDtoType()
            {
                BaseOutType = baseOut,
                SearchType = search,
                PageType = page,
                DetailType = detail,
                CreateOrUpdateBaseType = createOrUpdateBaseType,
                CreateType = create,
                UpdateType = update,
            };
            return entityTypeModel;

        }

        /// <summary>
        /// 获取dto类型
        /// </summary>
        /// <param name="dtoTypes"></param>
        /// <param name="entityType"></param>
        /// <returns></returns>
        protected virtual TemplateVuePermissionModel GetTemplateVuePermissionModel(Type[] dtoTypes, Type entityType)
        {
            string name = entityType.Name;
            var permission = dtoTypes.FirstOrDefault(a => a.Name == $"{name}Permissions");
            string? permissionGroup = permission?.GetField("GroupName")?.GetValue(null)?.ToString();

            var permissionModel = new TemplateVuePermissionModel(name, permissionGroup);

            return permissionModel;
        }

        /// <summary>
        /// 获取实体显示名称
        /// </summary>
        /// <param name="entityType"></param>
        /// <returns></returns>
        protected virtual string GetEntityDisplayName(Type entityType)
        {
            var displayName = entityType.GetCustomAttribute<DisplayAttribute>()?.Name ?? entityType.Name;

            return displayName;
        }

        /// <summary>
        /// 实体是否应该忽略
        /// </summary>
        /// <param name="ignoreEntityTypes"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        protected virtual bool EntityShouldIgnore(Type[]? ignoreEntityTypes, Type entity)
        {
            return ignoreEntityTypes != null && ignoreEntityTypes.Contains(entity);
        }

        /// <summary>
        /// 实体是否应该忽略
        /// </summary>
        /// <param name="ignoreEntity"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        protected virtual bool EntityShouldIgnore(string[]? ignoreEntity, string entity)
        {
            return ignoreEntity != null && ignoreEntity.Contains(entity, StringComparer.CurrentCultureIgnoreCase);
        }
    }
}