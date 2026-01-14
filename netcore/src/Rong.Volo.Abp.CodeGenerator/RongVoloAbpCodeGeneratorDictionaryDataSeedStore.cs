using Rong.Volo.Abp.CodeGenerator.Models;
using Rong.Volo.Abp.CodeGenerator.TemplateDefinitionProviders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.TextTemplating;

namespace Rong.Volo.Abp.CodeGenerator
{
    /// <summary>
    /// 代码生成器帮助器
    /// </summary>
    public class RongVoloAbpCodeGeneratorDictionaryDataSeedStore : RongVoloAbpCodeGeneratorStoreBase
    {
        public RongVoloAbpCodeGeneratorDictionaryDataSeedStore(ITemplateDefinitionManager templateDefinitionManager,
            ITemplateRenderer templateRenderer) : base(templateDefinitionManager, templateRenderer)
        {
        }

        /// <summary>
        /// 开始代码生成
        /// <para>*字典显示名称请在定义的字段上添加 DisplayAttribute 特性，并设置 Name。或 重写 <see cref="RongVoloAbpCodeGeneratorDictionaryDataSeedStore.GetDisplayName"/> 方法</para>
        /// </summary>
        /// <param name="type">字典枚举类型</param>
        /// <param name="nameSpace">统一命名空间</param>
        /// <param name="nameSpaceLayerName">命名空间分层名称，默认 EntityFrameworkCore</param>
        /// <param name="saveFolderPath">要保存到的文件夹，默认 EntityFrameworkCore/Seeds</param>
        /// <returns></returns>
        public async Task StartAsync(Type type, string nameSpace, string nameSpaceLayerName = "EntityFrameworkCore", string saveFolderPath = "EntityFrameworkCore/Seeds")
        {
            Check.NotNull(type, nameof(type));
            Check.NotNullOrWhiteSpace(nameSpace, nameof(nameSpace));

            var fieldInfos = type.GetFields(BindingFlags.Public | BindingFlags.Static).ToList();
            if (!fieldInfos.Any())
            {
                return;
            }

            var tasks = new List<Task>();

            foreach (var fieldInfo in fieldInfos)
            {
                var item = new DictionaryTemplateModel
                {
                    Type = type,
                    Name = GetDisplayName(fieldInfo),
                    Code = fieldInfo.Name,
                    NameSpace = nameSpace,
                    NameSpaceLayerName = nameSpaceLayerName,
                    SaveFolderPath = saveFolderPath
                };

                tasks.Add(Task.Run(async () => { await GenerateAsync(item); }));
            }

            var dictBase = new DictionaryTemplateModel
            {
                Type = type,
                NameSpace = nameSpace,
                NameSpaceLayerName = nameSpaceLayerName,
                SaveFolderPath = saveFolderPath
            };
            tasks.Add(Task.Run(async () => { await GenerateBaseAsync(dictBase); }));

            //等待任务执行完毕
            await Task.WhenAll(tasks);
        }

        /// <summary>
        /// 生成实体相关类
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        private async Task GenerateAsync(DictionaryTemplateModel entity)
        {
            string template = RongVoloAbpCodeGeneratorDictionaryDataSeedTemplateDefinitionProvider.TemplateName;

            //模板不存在
            var temp = await TemplateDefinitionManager.GetOrNullAsync(template);
            if (temp == null)
            {
                return;
            }

            string name = temp.Properties["name"].ToString().Replace("xxx", entity.Code);
            string path = temp.Properties["path"].ToString().Replace("xxx", entity.Code)
                .Replace("$namespace", entity.NameSpace)
                .Replace("$nameSpaceLayerName", entity.NameSpaceLayerName)
                .Replace("$saveFolderPath", entity.SaveFolderPath);
            //保存
            await SaveAsync(entity, template, name, path);
        }

        /// <summary>
        /// 生成实体相关类
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        private async Task GenerateBaseAsync(DictionaryTemplateModel entity)
        {
            string template = RongVoloAbpCodeGeneratorDictionaryDataSeedTemplateDefinitionProvider.DictionaryDataSeedBase;

            //模板不存在
            var temp = await TemplateDefinitionManager.GetOrNullAsync(template);
            if (temp == null)
            {
                return;
            }

            string name = temp.Properties["name"].ToString();
            string path = temp.Properties["path"].ToString()
                .Replace("$namespace", entity.NameSpace)
                .Replace("$nameSpaceLayerName", entity.NameSpaceLayerName)
                .Replace("$saveFolderPath", entity.SaveFolderPath);
            //保存
            await SaveAsync(entity, template, name, path);
        }

        /// <summary>
        /// 获取字典显示名称
        ///  <para>若无 Display.Name，则返回 field.Name</para>
        /// </summary>
        /// <returns></returns>
        protected virtual string? GetDisplayName(FieldInfo field)
        {
            if (field == null)
            {
                return null;
            }
            var displayName = field.GetCustomAttribute<DisplayAttribute>()?.Name;
            if (string.IsNullOrWhiteSpace(displayName))
            {
                displayName = field.Name;
            }
            return displayName;
        }
    }
}