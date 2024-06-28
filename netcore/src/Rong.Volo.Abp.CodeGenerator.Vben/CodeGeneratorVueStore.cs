using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
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

        protected CodeGeneratorVueModelStore CodeGeneratorModelStore;
        protected ITemplateDefinitionManager TemplateDefinitionManager;
        protected ITemplateRenderer TemplateRenderer;

        public CodeGeneratorVueStore(
            IOptions<CodeGeneratorVueOptions> options,
            CodeGeneratorVueModelStore codeGeneratorModelStore,
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
        /// <param name="apiRootPath">api根路径</param>
        /// <param name="entities">实体集合</param>
        /// <param name="saveRootPath">要保存到的根目录</param>
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
    }

}