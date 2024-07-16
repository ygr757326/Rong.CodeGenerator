using Rong.Volo.Abp.CodeGenerator.Vue.Attributes;
using Rong.Volo.Abp.CodeGenerator.Vue.Models;
using Rong.Volo.Abp.CodeGenerator.Vue.TemplateHelpers.Vbens;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using Rong.Volo.Abp.CodeGenerator.Vue.Enums;
using Volo.Abp;
using Volo.Abp.DependencyInjection;
using Microsoft.Extensions.Options;
using Rong.Volo.Abp.CodeGenerator.Vue.Models.Pages;

namespace Rong.Volo.Abp.CodeGenerator.Vue
{
    /// <summary>
    /// Vben代码生成器帮助器
    /// </summary>
    public class RongVoloAbpCodeGeneratorVueModelHelper : ISingletonDependency
    {
        private readonly RongVoloAbpVueVbenTemplate _vueTemplate;
        public RongVoloAbpCodeGeneratorVueModelHelper(RongVoloAbpVueVbenTemplate vueTemplate)
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
                case RongVoloAbpVueVbenTemplateNames.Vben_index:
                    {

                        var tableData = GetPropertyInfo(model.EntityDtoType.PageType, ignoreProperties: new[] { "id", "concurrencyStamp" })?.Where(a =>
                            !a.Property.Equals("concurrencyStamp", StringComparison.CurrentCultureIgnoreCase)).ToList();

                        var searchData = GetPropertyInfo(model.EntityDtoType.SearchType, true, new[] { "Sorting", "SkipCount", "MaxResultCount" });

                        data = new TemplateVuePageIndexModel
                        {
                            TableColumns = tableData,
                            TableColumnsTemplate = _vueTemplate.GetTableColumnsTemplate(tableData, 4),
                            TableColumnsSlotsTemplate = _vueTemplate.GetTableColumnsSlotsTemplate(tableData, 8),
                            TableSchemas = searchData,
                            TableSchemasTemplate = _vueTemplate.GetTableSchemasTemplate(searchData, 8),
                        };

                        break;
                    }
                case RongVoloAbpVueVbenTemplateNames.Vben_add:
                    {
                        var formData = GetPropertyInfo(model.EntityDtoType.CreateType);
                        data = new TemplateVuePageAddModel
                        {
                            Form = formData,
                            FormTemplate = _vueTemplate.GetFormTemplate(formData, 4),
                        };
                        break;
                    }
                case RongVoloAbpVueVbenTemplateNames.Vben_modify:
                    {
                        var formData = GetPropertyInfo(model.EntityDtoType.UpdateType);
                        data = new TemplateVuePageModifyModel
                        {
                            Form = formData,
                            FormTemplate = _vueTemplate.GetFormTemplate(formData, 4),
                        };
                        break;
                    }
                case RongVoloAbpVueVbenTemplateNames.Vben_detail:
                    {
                        var viewData = GetPropertyInfo(model.EntityDtoType.DetailType, ignoreProperties: new[] { "id", "concurrencyStamp" });
                        data = new TemplateVuePageDetailModel
                        {
                            Detail = viewData,
                            DetailTemplate = _vueTemplate.GetDetailTemplate(viewData, 8),
                        };
                        break;
                    }

                case RongVoloAbpVueVbenTemplateNames.Vben_api:
                    {
                        data = new TemplateVuePageApiModel();
                        break;
                    }
                case RongVoloAbpVueVbenTemplateNames.Vben_detailDrawer:
                    {
                        data = new TemplateVuePageDetailDrawerModel();
                        break;
                    }
                case RongVoloAbpVueVbenTemplateNames.Vben_router:
                    {
                        data = new TemplateVuePageRouterModel();
                        break;
                    }
                default:
                    throw new UserFriendlyException($"模板【{template}】输出未实现");
            }

            if (data is TemplateVueEntityModelBase m)
            {
                m.EntityInfo = model;
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
        protected virtual List<TemplateVueEntityPropertyData>? GetPropertyInfo(Type? type, bool? isCanWrite = null, string[]? ignoreProperties = null)
        {
            if (type == null)
            {
                return new List<TemplateVueEntityPropertyData>();
            }

            List<TemplateVueEntityPropertyData> data = new List<TemplateVueEntityPropertyData>();

            var properties = type.GetProperties()
                .WhereIf(ignoreProperties != null, a => !ignoreProperties.Contains(a.Name, StringComparer.CurrentCultureIgnoreCase))
                .WhereIf(isCanWrite != null, a => a.CanWrite == isCanWrite);

            foreach (PropertyInfo propertyInfo in properties)
            {
                var typeCode = propertyInfo.PropertyType.GetMyTypeCode();

                var info = new TemplateVueEntityPropertyData()
                {
                    PropertyInfo = propertyInfo,
                    Property = propertyInfo.Name,
                    PropertyCase = propertyInfo.Name.ToCamelCase(),
                    PropertyType = propertyInfo.PropertyType,
                    DisplayName = propertyInfo.GetCustomAttribute<DisplayAttribute>()?.Name ?? propertyInfo.Name,
                    IsRequired = propertyInfo.IsDefined(typeof(RequiredAttribute), true) ||
                                 !propertyInfo.PropertyType.IsNullableValueType(),
                };

                HandleEditorModel(info, propertyInfo);
                HandleTextareaModel(info, propertyInfo);
                HandleDictionaryModel(info, propertyInfo);
                HandleEnumModel(info, propertyInfo);
                HandleBoolModel(info, propertyInfo);
                HandleFileModel(info, propertyInfo);
                HandleValueNameModel(info, propertyInfo);
                HandleComponentModel(info, propertyInfo);

                var sorterAttr = propertyInfo.GetCustomAttribute<VueTableSorterAttribute>();
                if (sorterAttr != null)
                {
                    info.TableSorter = sorterAttr.Sorter;
                }

                data.Add(info);
            }

            return data;
        }

        /// <summary>
        /// 字典模型
        /// </summary>
        protected virtual void HandleDictionaryModel(TemplateVueEntityPropertyData info, PropertyInfo propertyInfo)
        {
            var attr = propertyInfo.GetCustomAttribute<VueDictionaryAttribute>();
            if (attr == null)
            {
                return;
            }

            info.IsDictionary = true;
            info.DictionaryCode = attr.Code;
            info.SelectMode = attr.SelectMode;
            info.TableSorter = attr.Sorter;
            info.IsSlot = attr.Slot;
        }

        /// <summary>
        /// 枚举模型
        /// </summary>
        protected virtual void HandleEnumModel(TemplateVueEntityPropertyData info, PropertyInfo propertyInfo)
        {
            var attr = propertyInfo.GetCustomAttribute<VueEnumAttribute>();

            info.IsEnum = attr != null || propertyInfo.PropertyType.IsEnum;
            info.SelectMode = attr?.SelectMode ?? VueSelectModeEnum.Select;

            if (attr != null)
            {
                info.PropertyType = attr.EnumType;
            }

            info.TableSorter = attr?.Sorter ?? true;
            info.IsSlot = attr?.Slot ?? true;
        }
        /// <summary>
        /// bool模型
        /// </summary>
        protected virtual void HandleBoolModel(TemplateVueEntityPropertyData info, PropertyInfo propertyInfo)
        {
            var attr = propertyInfo.GetCustomAttribute<VueBoolAttribute>();
            info.SelectMode = attr?.SelectMode ?? VueSelectModeEnum.Switch;
            info.TableSorter = attr?.Sorter ?? true;
            info.IsSlot = attr?.Slot ?? true;
        }

        /// <summary>
        /// 文件模型
        /// </summary>
        protected virtual void HandleFileModel(TemplateVueEntityPropertyData info, PropertyInfo propertyInfo)
        {
            var attr = propertyInfo.GetCustomAttribute<VueFileAttribute>();
            if (attr == null)
            {
                return;
            }
            info.IsFile = attr.FileType.Equals(VueFileTypeEnum.File);
            info.IsImage = attr.FileType.Equals(VueFileTypeEnum.Image);
            info.MultipleFile = attr.Multiple;
        }

        /// <summary>
        /// 内容输入模型
        /// </summary>
        protected virtual void HandleTextareaModel(TemplateVueEntityPropertyData info, PropertyInfo propertyInfo)
        {
            var attr = propertyInfo.GetCustomAttribute<VueTextareaAttribute>();
            if (attr == null)
            {
                return;
            }
            info.IsTextarea = true;
        }

        /// <summary>
        /// 编辑器模型
        /// </summary>
        protected virtual void HandleEditorModel(TemplateVueEntityPropertyData info, PropertyInfo propertyInfo)
        {
            var attr = propertyInfo.GetCustomAttribute<VueEditorAttribute>();
            if (attr == null)
            {
                return;
            }
            info.IsEditor = true;
        }

        /// <summary>
        /// 值名称数据模型
        /// </summary>
        protected virtual void HandleValueNameModel(TemplateVueEntityPropertyData info, PropertyInfo propertyInfo)
        {
            var attr = propertyInfo.GetCustomAttribute<VueValueNameAttribute>();
            if (attr == null || string.IsNullOrWhiteSpace(attr.PointSplicingName))
            {
                return;
            }
            info.Property = attr.PointSplicingName;
            info.PropertyCase = attr.PointSplicingName.Split(".").Select(a => a.ToCamelCase()).JoinAsString(".");
        }

        /// <summary>
        /// 自定义组件模型
        /// </summary>
        protected virtual void HandleComponentModel(TemplateVueEntityPropertyData info, PropertyInfo propertyInfo)
        {
            var attr = propertyInfo.GetCustomAttribute<VueComponentAttribute>();
            if (attr == null)
            {
                return;
            }

            info.IsComponent = true;
            info.Component = string.IsNullOrWhiteSpace(attr.Component) ? "Input" : attr.Component;
        }
    }

}