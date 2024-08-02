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
using System.Collections.Concurrent;

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

                        var columns = GetPropertyInfosOfSort(model.EntityDtoType.PageType, ignoreProperties: new[] { "id", "concurrencyStamp" })
                            .Where(a => !a.Property.Equals("concurrencyStamp", StringComparison.CurrentCultureIgnoreCase)).ToList();

                        var search = GetPropertyInfosOfSort(model.EntityDtoType.SearchType, true, new[] { "Sorting", "SkipCount", "MaxResultCount", "DefaultMaxResultCount", "MaxMaxResultCount" }).ToList();

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
                        var formData = GetPropertyInfosOfSort(model.EntityDtoType.CreateType, baseTypeIsMain: false).ToList();

                        data = new TemplateVuePageAddModel
                        {
                            Form = formData,
                            FormTemplate = _vueTemplate.GetFormTemplate(formData, 4),
                        };
                        break;
                    }
                case RongVoloAbpVueVbenTemplateNames.Vben_modify:
                    {
                        var formData = GetPropertyInfosOfSort(model.EntityDtoType.UpdateType, baseTypeIsMain: false).ToList();
                        data = new TemplateVuePageModifyModel
                        {
                            Form = formData,
                            FormTemplate = _vueTemplate.GetFormTemplate(formData, 4),
                        };
                        break;
                    }
                case RongVoloAbpVueVbenTemplateNames.Vben_detail:
                    {
                        var detailData = GetPropertyInfosOfSort(model.EntityDtoType.DetailType, ignoreProperties: new[] { "id", "concurrencyStamp" }).ToList();
                        data = new TemplateVuePageDetailModel
                        {
                            Detail = detailData,
                            DetailTemplate = _vueTemplate.GetDetailTemplate(detailData, 8),
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
        /// 获取排序后的属性值
        /// </summary>
        /// <param name="type"></param>
        /// <param name="isCanWrite"></param>
        /// <param name="ignoreProperties">忽略的属性</param>
        /// <returns></returns>
        protected virtual List<TemplateVueEntityPropertyData> GetPropertyInfosOfSort(Type? type, bool? isCanWrite = null, string[]? ignoreProperties = null, bool baseTypeIsMain = true, List<TemplateVueEntityPropertyData>? data = null)
        {
            if (type == null)
            {
                return data ?? new List<TemplateVueEntityPropertyData>();
            }

            var list = GetPropertyInfos(type, isCanWrite, ignoreProperties);

            data ??= list.ToList();

            //基类信息为主
            if (baseTypeIsMain)
            {
                var basePropertyInfo = data.Where(a => list.Any(x => x.PropertyInfo.Name == a.PropertyInfo.Name));
                var exceptPropertyInfo = data.Except(basePropertyInfo);
                data = basePropertyInfo.Concat(exceptPropertyInfo).ToList();
            }

            if (type.BaseType != null)
            {
                data = GetPropertyInfosOfSort(type.BaseType, isCanWrite, ignoreProperties, baseTypeIsMain, data);
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
        protected virtual IEnumerable<TemplateVueEntityPropertyData> GetPropertyInfos(Type? type, bool? isCanWrite = null, string[]? ignoreProperties = null)
        {
            if (type == null)
            {
                yield break;
            }

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

                yield return info;
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
            info.IsDictionaryMultiple = attr.Multiple;
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
            info.IsEnumMultiple = attr?.Multiple??false;
        }
        /// <summary>
        /// bool模型
        /// </summary>
        protected virtual void HandleBoolModel(TemplateVueEntityPropertyData info, PropertyInfo propertyInfo)
        {
            var typeCode = propertyInfo.PropertyType.GetMyTypeCode();
            var attr = propertyInfo.GetCustomAttribute<VueBoolAttribute>();
            if (attr == null && typeCode != TypeCode.Boolean)
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
            info.IsSlot = true;
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
            info.IsSlot = false;
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
            info.IsSlot = true;
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
            info.IsSlot = false;
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
            info.IsSlot = false;
        }
    }

}