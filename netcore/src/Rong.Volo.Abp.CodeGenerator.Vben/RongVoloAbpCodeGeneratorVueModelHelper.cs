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
using Volo.Abp.Reflection;

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
                        var isAssignable = model.EntityDtoType.BaseOutType?.IsAssignableFrom(model.EntityDtoType.PageType) == true;
                        var baseColumns = isAssignable ? GetPropertyInfo(model.EntityDtoType.PageType.ba, ignoreProperties: new[] { "id" }).ToList() : new List<TemplateVueEntityPropertyData>();

                        var pageColumns = GetPropertyInfo(model.EntityDtoType.PageType, ignoreProperties: new[] { "id", "concurrencyStamp" })
                            .Where(a => !a.Property.Equals("concurrencyStamp", StringComparison.CurrentCultureIgnoreCase)).ToList();

                        var columns = baseColumns.Concat(pageColumns.Where(a => baseColumns.All(b => b.PropertyInfo.Name != a.PropertyInfo.Name))).ToList();

                        var search = GetPropertyInfo(model.EntityDtoType.SearchType, true, new[] { "Sorting", "SkipCount", "MaxResultCount" });

                        data = new TemplateVuePageIndexModel
                        {
                            TableColumns = columns,
                            TableColumnsTemplate = _vueTemplate.GetTableColumnsTemplate(columns, 4),
                            TableColumnsSlotsTemplate = _vueTemplate.GetTableColumnsSlotsTemplate(columns, 8),
                            TableSchemas = search,
                            TableSchemasTemplate = _vueTemplate.GetTableSchemasTemplate(search, 4),
                        };

                        break;
                    }
                case RongVoloAbpVueVbenTemplateNames.Vben_add:
                    {
                        var isAssignable = model.EntityDtoType.CreateOrUpdateBaseType?.IsAssignableFrom(model.EntityDtoType.CreateType) == true;
                        var baseForm = isAssignable ? GetPropertyInfo(model.EntityDtoType.CreateOrUpdateBaseType) : new List<TemplateVueEntityPropertyData>();
                        var createForm = GetPropertyInfo(model.EntityDtoType.CreateType);

                        var formData = createForm.Concat(baseForm.Where(a => createForm.All(b => b.PropertyInfo.Name != a.PropertyInfo.Name))).ToList();

                        data = new TemplateVuePageAddModel
                        {
                            Form = formData,
                            FormTemplate = _vueTemplate.GetFormTemplate(formData, 4),
                        };
                        break;
                    }
                case RongVoloAbpVueVbenTemplateNames.Vben_modify:
                    {
                        var isAssignable = model.EntityDtoType.CreateOrUpdateBaseType?.IsAssignableFrom(model.EntityDtoType.UpdateType) == true;
                        var baseForm = isAssignable ? GetPropertyInfo(model.EntityDtoType.CreateOrUpdateBaseType) : new List<TemplateVueEntityPropertyData>();
                        var updateForm = GetPropertyInfo(model.EntityDtoType.UpdateType);

                        var formData = updateForm.Concat(baseForm.Where(a => updateForm.All(b => b.PropertyInfo.Name != a.PropertyInfo.Name))).ToList();

                        data = new TemplateVuePageModifyModel
                        {
                            Form = formData,
                            FormTemplate = _vueTemplate.GetFormTemplate(formData, 4),
                        };
                        break;
                    }
                case RongVoloAbpVueVbenTemplateNames.Vben_detail:
                    {
                        var isAssignable1 = model.EntityDtoType.PageType?.IsAssignableFrom(model.EntityDtoType.DetailType) == true;
                        var pageData = isAssignable1 ? GetPropertyInfo(model.EntityDtoType.PageType, ignoreProperties: new[] { "id", "concurrencyStamp" })
                            .Where(a => !a.Property.Equals("concurrencyStamp", StringComparison.CurrentCultureIgnoreCase)).ToList() : new List<TemplateVueEntityPropertyData>();


                        var isAssignable = model.EntityDtoType.BaseOutType?.IsAssignableFrom(model.EntityDtoType.PageType) == true;
                        var baseData = isAssignable1 && isAssignable ? GetPropertyInfo(model.EntityDtoType.BaseOutType, ignoreProperties: new[] { "id" }).ToList() : new List<TemplateVueEntityPropertyData>();


                        var detailData = GetPropertyInfo(model.EntityDtoType.DetailType, ignoreProperties: new[] { "id", "concurrencyStamp" });

                        var pageData1 = baseData.Concat(pageData.Where(a => baseData.All(b => b.PropertyInfo.Name != a.PropertyInfo.Name))).ToList();

                        var viewData = pageData1.Concat(detailData.Where(a => pageData1.All(b => b.PropertyInfo.Name != a.PropertyInfo.Name))).ToList();

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
                case RongVoloAbpVueVbenTemplateNames.Vben_myComponentSetting:
                    {
                        data = null;
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
        protected virtual void GetPropertyInfo(List<TemplateVueEntityPropertyData> data, Type? type, bool? isCanWrite = null, string[]? ignoreProperties = null, bool baseTypeIsMain = true)
        {
            if (type == null)
            {
                return;
            }

            List<TemplateVueEntityPropertyData> list = new List<TemplateVueEntityPropertyData>();

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
                    PropertyType = propertyInfo.PropertyType.GetFirstGenericArgumentIfNullable(),
                    DisplayName = propertyInfo.GetCustomAttribute<DisplayAttribute>()?.Name ?? propertyInfo.Name,
                    IsRequired = propertyInfo.IsDefined(typeof(RequiredAttribute), true) ||
                                 propertyInfo.PropertyType != typeof(string) && !TypeHelper.IsNullable(propertyInfo.PropertyType),
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

                list.Add(info);
            }

            if (baseTypeIsMain)
            {
                data.Add(list);
            }
            else
            {
                data.Add(list);
            }


            if (type.BaseType != null)
            {
                GetPropertyInfo(data, type.BaseType, isCanWrite, ignoreProperties, baseTypeIsMain);
            }
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

            info.IsEnum = attr != null || propertyInfo.PropertyType.GetFirstGenericArgumentIfNullable().IsEnum;
            if (!info.IsEnum)
            {
                return;
            }

            if (attr != null)
            {
                info.PropertyType = attr.EnumType;
            }

            info.SelectMode = attr?.SelectMode ?? VueSelectModeEnum.Select;
            info.TableSorter = attr?.Sorter ?? true;
            info.IsSlot = attr?.Slot ?? true;
        }
        /// <summary>
        /// bool模型
        /// </summary>
        protected virtual void HandleBoolModel(TemplateVueEntityPropertyData info, PropertyInfo propertyInfo)
        {
            var typeCode = propertyInfo.PropertyType.GetMyTypeCode();
            var attr = propertyInfo.GetCustomAttribute<VueBoolAttribute>();
            if (attr == null || typeCode != TypeCode.Boolean)
            {
                return;
            }
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
            info.Property = attr.PointSplicingName ?? propertyInfo.Name;
            info.PropertyCase = info.Property.Split(".").Select(a => a.ToCamelCase()).JoinAsString(".");
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