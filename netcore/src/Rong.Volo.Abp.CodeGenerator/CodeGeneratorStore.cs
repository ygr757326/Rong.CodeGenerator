using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Reflection;
using Volo.Abp.TextTemplating;

namespace Rong.Volo.Abp.CodeGenerator
{
    /// <summary>
    /// 代码生成器帮助器
    /// </summary>
    public class CodeGeneratorStore : ITransientDependency
    {
        private readonly ITemplateDefinitionManager _templateDefinitionManager;
        private readonly ITemplateRenderer _templateRenderer;

        public CodeGeneratorStore(ITemplateDefinitionManager templateDefinitionManager,
            ITemplateRenderer templateRenderer)
        {
            _templateRenderer = templateRenderer;
            _templateDefinitionManager = templateDefinitionManager;
        }

        /// <summary>
        /// 开始代码生成
        /// </summary>
        /// <param name="entities">实体集合</param>
        /// <param name="applicationAsController">统一是否应用层作为控制器，如果 <see cref="TemplateModel.ApplicationAsController"/> 为空则使用 <paramref name="applicationAsController"/>，否则使用 <see cref="TemplateModel.ApplicationAsController"/>，如果 <see cref="TemplateModel.ApplicationAsController"/> 也为空，则默认true（true：不生成HttpApi,默认不生成）</param>
        /// <param name="nameSpace">统一命名空间，如果 <see cref="TemplateModel.NameSpace"/> 为空则使用 <paramref name="nameSpace"/>，否则使用 <see cref="TemplateModel.NameSpace"/></param>
        /// <param name="project">项目，若为空，则取 <paramref name="nameSpace"/>.Split('.')[1] </param>
        /// <returns></returns>
        public async Task StartAsync(List<TemplateModel> entities, bool? applicationAsController = null, string? nameSpace = null, string? project = null)
        {
            Check.NotNull(entities, nameof(entities));

            var entitys = entities.GroupBy(a => a.Entity)
                .Where(a => a.Count() > 1)
                .Select(a => a.Key)
                .ToList();
            if (entitys.Any())
            {
                throw new UserFriendlyException($"以下实体名称重复：{entitys.JoinAsString(",")}");
            }

            var tasks = new List<Task>();

            foreach (var item in entities)
            {
                item.EntityCase = item.Entity.ToCamelCase();
                item.NameSpace ??= nameSpace;
                item.Project ??= project;
                item.ApplicationAsController ??= applicationAsController ?? true;

                item.SetProject();

                tasks.Add(Task.Run(async () => { await GenerateForEntityAsync(item); }));
            }

            tasks.Add(Task.Run(async () => { await GenerateForBaseAsync(nameSpace, project); }));

            //等待任务执行完毕
            await Task.WhenAll(tasks);
        }

        /// <summary>
        /// 生成实体相关类
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        private async Task GenerateForEntityAsync(TemplateModel entity)
        {
            string[] templates = ReflectionHelper.GetPublicConstantsRecursively(typeof(CodeGeneratorTemplateNames))
                .Except(new[]
                {
                    CodeGeneratorTemplateNames.Domain_DomainServiceBase,
                    CodeGeneratorTemplateNames.HttpApi_ControllerBase
                }).ToArray();

            foreach (var template in templates)
            {
                //模板不存在
                var temp = await _templateDefinitionManager.GetAsync(template);
                if (temp == null)
                {
                    continue;
                }

                string name = temp.Properties["name"].ToString().Replace("xxx", entity.Entity)
                    .Replace("$project", entity.Project);
                string path = temp.Properties["path"].ToString().Replace("xxx", entity.Entity)
                    .Replace("$namespace", entity.NameSpace);

                //未启用
                if (entity.ApplicationAsController == true &&
                    template == CodeGeneratorTemplateNames.HttpApi_xxxController)
                {
                    continue;
                }

                //保存
                await SaveAsync(entity, template, name, path);
            }

        }

        /// <summary>
        /// 生成基类
        /// </summary>
        /// <returns></returns>
        private async Task GenerateForBaseAsync(string? nameSpace, string? project)
        {
            var model = new TemplateModel
            {
                NameSpace = nameSpace,
                Project = project
            };

            model.SetProject();

            string[] templates = new[]
            {
                CodeGeneratorTemplateNames.Domain_DomainServiceBase,
                CodeGeneratorTemplateNames.HttpApi_ControllerBase,
                CodeGeneratorTemplateNames.ApplicationContractsPermissions_PermissionAttribute,
                CodeGeneratorTemplateNames.ApplicationContractsPermissions_PermissionConsts,
                CodeGeneratorTemplateNames.ApplicationContractsPermissions_PermissionExtensions,
                CodeGeneratorTemplateNames.ApplicationContractsPermissions_PermissionMultiTenancySideAttribute,
            };

            foreach (var template in templates)
            {
                //模板不存在
                var temp = await _templateDefinitionManager.GetAsync(template);
                if (temp == null)
                {
                    continue;
                }

                string name = temp.Properties["name"].ToString().Replace("$project", model.Project);
                string path = temp.Properties["path"].ToString().Replace("$namespace", model.NameSpace);

                //保存
                await SaveAsync(model, template, name, path);
            }

        }

        /// <summary>
        /// 保存文件
        /// </summary>
        /// <returns></returns>
        private async Task SaveAsync(TemplateModel model, string template, string name, string path)
        {
            //模板不存在
            var temp = await _templateDefinitionManager.GetAsync(template);
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
            var result = await _templateRenderer.RenderAsync(
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
        private async Task SaveToFileAsync(string renderResult, string fileName)
        {
            byte[] buffer = renderResult.GetBytes(Encoding.UTF8);
            using FileStream fs = new FileStream(fileName, FileMode.Create, FileAccess.ReadWrite);
            await fs.WriteAsync(buffer, 0, buffer.Length);
        }
    }

}