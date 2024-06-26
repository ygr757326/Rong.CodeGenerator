using Rong.Volo.Abp.CodeGenerator.Vue.Attributes;
using Rong.Volo.Abp.CodeGenerator.Vue.Models;
using Rong.Volo.Abp.CodeGenerator.Vue.TemplateHelpers.Vbens;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using Volo.Abp;
using Volo.Abp.DependencyInjection;

namespace Rong.Volo.Abp.CodeGenerator.Vue
{
    /// <summary>
    /// Vben代码生成器帮助器
    /// </summary>
    public class CodeGeneratorVueModelStore : ISingletonDependency
    {
        private readonly CodeGeneratorVueVbenTemplate _vueTemplate;
        public CodeGeneratorVueModelStore(CodeGeneratorVueVbenTemplate vueTemplate)
        {
            _vueTemplate = vueTemplate;
        }

        /// <summary>
        /// 获取模型值
        /// </summary>
        /// <param name="model"></param>
        /// <param name="template"></param>
        /// <returns></returns>
        /// <exception cref="UserFriendlyException"></exception>
        public virtual object? GetModel(TemplateVueModel model, string template)
        {
            object data;
            switch (template)
            {
                case CodeGeneratorVueVbenTemplateNames.Vben_index:
                    {

                        var tableData = GetPropertyInfo(model.EntityType.PageType, ignoreProperties: new[] { "id", "concurrencyStamp" })?.Where(a =>
                            !a.Property.Equals("concurrencyStamp", StringComparison.CurrentCultureIgnoreCase)).ToList();

                        var searchData = GetPropertyInfo(model.EntityType.SearchType, true, new[] { "Sorting", "SkipCount", "MaxResultCount" });

                        data = new TemplateVueIndexModel
                        {
                            TableColumns = tableData,
                            TableColumnsTemplate = _vueTemplate.GetTableColumnsTemplate(tableData, 4),
                            TableColumnsSlotsTemplate = _vueTemplate.GetTableColumnsSlotsTemplate(tableData, 8),
                            TableSchemas = searchData,
                            TableSchemasTemplate = _vueTemplate.GetTableSchemasTemplate(searchData, 8),
                        };

                        break;
                    }
                case CodeGeneratorVueVbenTemplateNames.Vben_add:
                    {
                        var formData = GetPropertyInfo(model.EntityType.CreateType);
                        data = new TemplateVueAddModel
                        {
                            Form = formData,
                            FormTemplate = _vueTemplate.GetFormTemplate(formData, 4),
                        };
                        break;
                    }
                case CodeGeneratorVueVbenTemplateNames.Vben_modify:
                    {
                        var formData = GetPropertyInfo(model.EntityType.UpdateType);
                        data = new TemplateVueModifyModel
                        {
                            Form = formData,
                            FormTemplate = _vueTemplate.GetFormTemplate(formData, 4),
                        };
                        break;
                    }
                case CodeGeneratorVueVbenTemplateNames.Vben_detail:
                    {
                        var viewData = GetPropertyInfo(model.EntityType.DetailType, ignoreProperties: new[] { "id", "concurrencyStamp" });
                        data = new TemplateVueDetailModel
                        {
                            Detail = viewData,
                            DetailTemplate = _vueTemplate.GetDetailTemplate(viewData, 8),
                        };
                        break;
                    }

                case CodeGeneratorVueVbenTemplateNames.Vben_api:
                    {
                        data = new TemplateVueApiModel()
                        {

                        };
                        break;
                    }
                case CodeGeneratorVueVbenTemplateNames.Vben_detailDrawer:
                    {
                        data = new TemplateVueDetailDrawerModel()
                        {

                        };
                        break;
                    }
                case CodeGeneratorVueVbenTemplateNames.Vben_router:
                {
                    data = new TemplateVueRouterModel()
                    {

                    };
                    break;
                }
                default:
                    throw new UserFriendlyException($"模板【{template}】输出未实现");
            }

            if (data is TemplateVueModel m)
            {
                m.ServiceName = model.ServiceName;
                m.EntityCase = model.EntityCase;
                m.Entity = model.Entity;
                m.EntityName = model.EntityName;
                m.PermissionGroup = model.PermissionGroup;
            }

            return data;
        }

        /// <summary>
        /// 获取属性值
        /// </summary>
        /// <param name="type"></param>
        /// <param name="isCanWrite"></param>
        /// <param name="ignoreProperties">忽略的属性</param>
        /// <returns></returns>
        protected virtual List<TemplateVueModelData>? GetPropertyInfo(Type? type, bool? isCanWrite = null, string[]? ignoreProperties = null)
        {
            if (type == null)
            {
                return new List<TemplateVueModelData>();
            }

            List<TemplateVueModelData> data = new List<TemplateVueModelData>();

            var properties = type.GetProperties()
                .WhereIf(ignoreProperties != null, a => !ignoreProperties.Contains(a.Name, StringComparer.CurrentCultureIgnoreCase))
                .WhereIf(isCanWrite != null, a => a.CanWrite == isCanWrite);

            foreach (PropertyInfo propertyInfo in properties)
            {
                var typeCode = propertyInfo.PropertyType.GetMyTypeCode();

                var dictAttr = propertyInfo.GetCustomAttribute<VueDictionaryAttribute>();
                var enumAttr = propertyInfo.GetCustomAttribute<VueEnumAttribute>();
                var fileAttr = propertyInfo.GetCustomAttribute<VueFileAttribute>();
                var sorterAttr = propertyInfo.GetCustomAttribute<VueSorterAttribute>();

                var info = new TemplateVueModelData()
                {
                    Property = propertyInfo.Name,
                    PropertyCase = propertyInfo.Name.ToCamelCase(),
                    PropertyType = propertyInfo.PropertyType,
                    DisplayName = propertyInfo.GetCustomAttribute<DisplayAttribute>()?.Name ?? propertyInfo.Name,
                    IsRequired = propertyInfo.IsDefined(typeof(RequiredAttribute), true) ||
                                 !propertyInfo.PropertyType.IsNullableValueType(),
                };
                info.IsDictionary = dictAttr != null;
                info.DictionaryCode = dictAttr?.Code;

                info.IsEnum = enumAttr != null || propertyInfo.PropertyType.IsEnum;

                if (enumAttr != null)
                {
                    info.PropertyType = enumAttr.EnumType;
                }

                info.SelectMode = enumAttr?.SelectMode ?? dictAttr?.SelectMode;

                info.TableSorter = info.IsEnum || info.IsDictionary || dictAttr?.Sorter == true || enumAttr?.Sorter == true || sorterAttr != null;

                info.IsFile = fileAttr != null;
                info.MultipleFile = fileAttr?.Multiple ?? false;

                info.IsSlot = typeCode == TypeCode.Boolean || dictAttr?.Slot == true || enumAttr?.Slot == true;

                data.Add(info);
            }

            return data;
        }
    }

}