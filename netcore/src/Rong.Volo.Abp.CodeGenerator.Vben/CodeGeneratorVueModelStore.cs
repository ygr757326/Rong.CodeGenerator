﻿using Rong.Volo.Abp.CodeGenerator.Vue.Attributes;
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
                        data = new TemplateVueApiModel();
                        break;
                    }
                case CodeGeneratorVueVbenTemplateNames.Vben_detailDrawer:
                    {
                        data = new TemplateVueDetailDrawerModel();
                        break;
                    }
                case CodeGeneratorVueVbenTemplateNames.Vben_router:
                    {
                        data = new TemplateVueRouterModel();
                        break;
                    }
                default:
                    throw new UserFriendlyException($"模板【{template}】输出未实现");
            }

            if (data is TemplateVueModel m)
            {
                m.Options = model.Options;
                m.ApiRootPath = model.ApiRootPath;
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

                var info = new TemplateVueModelData()
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
        protected virtual void HandleDictionaryModel(TemplateVueModelData info, PropertyInfo propertyInfo)
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
        protected virtual void HandleEnumModel(TemplateVueModelData info, PropertyInfo propertyInfo)
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
        protected virtual void HandleBoolModel(TemplateVueModelData info, PropertyInfo propertyInfo)
        {
            var attr = propertyInfo.GetCustomAttribute<VueBoolAttribute>();
            info.SelectMode = attr?.SelectMode ?? VueSelectModeEnum.Switch;
            info.TableSorter = attr?.Sorter ?? true;
            info.IsSlot = attr?.Slot ?? true;
        }

        /// <summary>
        /// 文件模型
        /// </summary>
        protected virtual void HandleFileModel(TemplateVueModelData info, PropertyInfo propertyInfo)
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
        protected virtual void HandleTextareaModel(TemplateVueModelData info, PropertyInfo propertyInfo)
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
        protected virtual void HandleEditorModel(TemplateVueModelData info, PropertyInfo propertyInfo)
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
        protected virtual void HandleValueNameModel(TemplateVueModelData info, PropertyInfo propertyInfo)
        {
            var attr = propertyInfo.GetCustomAttribute<VueValueNameAttribute>();
            if (attr == null || string.IsNullOrWhiteSpace(attr.PointSplicingName))
            {
                return;
            }
            info.Property = attr.PointSplicingName;
            info.PropertyCase = attr.PointSplicingName.Split(".").Select(a => a.ToCamelCase()).JoinAsString(".");
        }
    }

}