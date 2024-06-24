using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Reflection;
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
        /// <returns></returns>
        public async Task StartAsync(List<TemplateVueModel> entities, string? nameSpace, string projectRootPath)
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
                item.NameSpace ??= nameSpace;

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
        private async Task GenerateForEntityAsync(TemplateVueModel entity, string projectRootPath)
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

                var model = GetModel(entity.NameSpace, entity.Entity, template);
                if (model is TemplateVueModel m)
                {
                    m.NameSpace = entity.NameSpace;
                    m.Entity = entity.Entity;
                    m.EntityName = entity.EntityName;
                }

                //保存
                await SaveAsync(model, template, name, path);
            }

        }

        private object GetModel(string nameSpace, string entity, string template)
        {
            var types = GetTypes(nameSpace);

            switch (template)
            {
                case CodeGeneratorVbenTemplateNames.Vben_index:
                    {
                        var search = types.FirstOrDefault(a => a.Name == $"{entity}PageSearchInput");
                        var page = types.FirstOrDefault(a => a.Name == $"{entity}PageOutput");

                        TemplateVueIndexModel data = new TemplateVueIndexModel
                        {
                            Table = GetPropertyInfo(page),
                            Search = GetPropertyInfo(search, true)
                        };
                        return data;
                    }
                case CodeGeneratorVbenTemplateNames.Vben_add:
                    {
                        var create = types.FirstOrDefault(a => a.Name == $"{entity}CreateInput");

                        TemplateVueAddModel data = new TemplateVueAddModel
                        {
                            Form = GetPropertyInfo(create),
                        };
                        return data;
                    }
                case CodeGeneratorVbenTemplateNames.Vben_modify:
                    {
                        var update = types.FirstOrDefault(a => a.Name == $"{entity}UpdateInput");

                        TemplateVueModifyModel data = new TemplateVueModifyModel
                        {
                            Form = GetPropertyInfo(update),
                        };
                        return data;
                    }
                case CodeGeneratorVbenTemplateNames.Vben_detail:
                    {
                        var detail = types.FirstOrDefault(a => a.Name == $"{entity}DetailOutput");

                        TemplateVueDetailModel data = new TemplateVueDetailModel
                        {
                            View = GetPropertyInfo(detail),
                        };
                        return data;
                    }

                case CodeGeneratorVbenTemplateNames.Vben_api:
                    {
                        TemplateVueApiModel data = new TemplateVueApiModel()
                        {

                        };
                        return data;
                    }
                case CodeGeneratorVbenTemplateNames.Vben_detailDrawer:
                    {
                        return null;
                    }
                default:
                    throw new UserFriendlyException($"模板【{template}】输出未实现");
            }
        }


        private List<TemplateVueModelData>? GetPropertyInfo(Type? type, bool? isCanWrite = null)
        {
            if (type == null)
            {
                return null;
            }

            List<TemplateVueModelData> data = new List<TemplateVueModelData>();

            var properties = type.GetProperties().WhereIf(isCanWrite != null, a => a.CanWrite == isCanWrite);

            foreach (PropertyInfo propertyInfo in properties)
            {
                var info = new TemplateVueModelData()
                {
                    Property = propertyInfo.Name,
                    PropertyType = propertyInfo.PropertyType,
                    DisplayName = propertyInfo.GetCustomAttribute<DisplayAttribute>()?.Name,
                    IsRequired = propertyInfo.IsDefined(typeof(RequiredAttribute), true),
                };

                data.Add(info);
            }

            return data;
        }



        private static IEnumerable<Type> GetTypes(string nameSpace)
        {
            return AppDomain.CurrentDomain.GetAssemblies()
                .Where(x => x.FullName.StartsWith(nameSpace))
                .SelectMany(a => a.GetTypes());
        }

        /// <summary>
        /// 保存文件
        /// </summary>
        /// <returns></returns>
        private async Task SaveAsync(object model, string template, string name, string path)
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