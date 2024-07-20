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
    public class RongVoloAbpCodeGeneratorStoreBase : ITransientDependency
    {
        protected ITemplateDefinitionManager TemplateDefinitionManager;
        protected ITemplateRenderer TemplateRenderer;

        protected RongVoloAbpCodeGeneratorStoreBase(ITemplateDefinitionManager templateDefinitionManager,
            ITemplateRenderer templateRenderer)
        {
            TemplateRenderer = templateRenderer;
            TemplateDefinitionManager = templateDefinitionManager;
        }

        /// <summary>
        /// 保存文件
        /// </summary>
        /// <returns></returns>
        protected virtual async Task SaveAsync(TemplateModel model, string template, string name, string path)
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