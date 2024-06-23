using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rong.Volo.Abp.CodeGenerator.Vue.Models;
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
        private readonly ITemplateDefinitionManager _templateDefinitionManager;
        private readonly ITemplateRenderer _templateRenderer;

        public CodeGeneratorVueStore(ITemplateDefinitionManager templateDefinitionManager,
            ITemplateRenderer templateRenderer)
        {
            _templateRenderer = templateRenderer;
            _templateDefinitionManager = templateDefinitionManager;
        }

        /// <summary>
        /// 开始代码生成
        /// </summary>
        /// <param name="entities">实体集合</param>
        /// <param name="applicationAsController">统一是否应用层作为控制器，如果 <see cref="TemplateVueAddModel.ApplicationAsController"/> 为空则使用 <paramref name="applicationAsController"/>，否则使用 <see cref="TemplateVueAddModel.ApplicationAsController"/>，如果 <see cref="TemplateVueAddModel.ApplicationAsController"/> 也为空，则默认true（true：不生成HttpApi,默认不生成）</param>
        /// <param name="nameSpace">统一命名空间，如果 <see cref="TemplateVueAddModel.NameSpace"/> 为空则使用 <paramref name="nameSpace"/>，否则使用 <see cref="TemplateVueAddModel.NameSpace"/></param>
        /// <returns></returns>
        public async Task StartAsync(List<TemplateVueAddModel> entities, string projectRootPath)
        {
            Check.NotNull(entities, nameof(entities));
            Check.NotNullOrWhiteSpace(projectRootPath, nameof(projectRootPath));

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
                tasks.Add(Task.Run(async () => { await GenerateForEntityAsync(item, projectRootPath); }));
            }

            //等待任务执行完毕
            await Task.WhenAll(tasks);
        }

        /// <summary>
        /// 生成实体相关类
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        private async Task GenerateForEntityAsync(TemplateVueAddModel entity, string projectRootPath)
        {
            string[] templates = ReflectionHelper.GetPublicConstantsRecursively(typeof(CodeGeneratorVbenTemplateNames));

            foreach (var template in templates)
            {
                //模板不存在
                var temp = await _templateDefinitionManager.GetAsync(template);
                if (temp == null)
                {
                    continue;
                }

                string name = temp.Properties["name"].ToString().Replace("xxx", entity.Entity);
                string path = temp.Properties["path"].ToString().Replace("xxx", entity.Entity)
                    .Replace("$rootPath", projectRootPath?.TrimEnd('\\', '/'));

                //保存
                await SaveAsync(entity, template, name, path);
            }

        }

        /// <summary>
        /// 保存文件
        /// </summary>
        /// <returns></returns>
        private async Task SaveAsync(TemplateVueAddModel model, string template, string name, string path)
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