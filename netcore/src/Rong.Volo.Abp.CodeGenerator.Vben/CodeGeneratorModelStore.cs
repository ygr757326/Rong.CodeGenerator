using Rong.Volo.Abp.CodeGenerator.Vue.Attributes;
using Rong.Volo.Abp.CodeGenerator.Vue.Models;
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
    /// 代码生成器帮助器
    /// </summary>
    public class CodeGeneratorModelStore : ISingletonDependency
    {
        private readonly CodeGeneratorVueVbenHelper _codeGeneratorVueVbenHelper;
        public CodeGeneratorModelStore(CodeGeneratorVueVbenHelper codeGeneratorVueVbenHelper)
        {
            _codeGeneratorVueVbenHelper = codeGeneratorVueVbenHelper;
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
            var types = GetTypes(model.NameSpace);
            object data;
            switch (template)
            {
                case CodeGeneratorVbenTemplateNames.Vben_index:
                    {

                        var page = types.FirstOrDefault(a => a.Name == $"{model.Entity}PageOutput");
                        var tableData = GetPropertyInfo(page, ignoreProperties: new[] { "id", "concurrencyStamp" })?.Where(a =>
                            !a.Property.Equals("concurrencyStamp", StringComparison.CurrentCultureIgnoreCase)).ToList();

                        var search = types.FirstOrDefault(a => a.Name == $"{model.Entity}PageSearchInput");
                        var searchData = GetPropertyInfo(search, true, new[] { "Sorting", "SkipCount", "MaxResultCount" });

                        data = new TemplateVueIndexModel
                        {
                            Table = tableData,
                            TableString = _codeGeneratorVueVbenHelper.GetVueTable(tableData, 4),
                            TableSlotsString = _codeGeneratorVueVbenHelper.GetVueTableSlots(tableData, 8),
                            Search = searchData,
                            SearchString = _codeGeneratorVueVbenHelper.GetVueForm(searchData, 8),
                        };

                        break;
                    }
                case CodeGeneratorVbenTemplateNames.Vben_add:
                    {
                        var create = types.FirstOrDefault(a => a.Name == $"{model.Entity}CreateInput");

                        var formData = GetPropertyInfo(create);
                        data = new TemplateVueAddModel
                        {
                            Form = formData,
                            FormString = _codeGeneratorVueVbenHelper.GetVueForm(formData, 4),
                        };
                        break;
                    }
                case CodeGeneratorVbenTemplateNames.Vben_modify:
                    {
                        var update = types.FirstOrDefault(a => a.Name == $"{model.Entity}UpdateInput");

                        var formData = GetPropertyInfo(update);
                        data = new TemplateVueModifyModel
                        {
                            Form = formData,
                            FormString = _codeGeneratorVueVbenHelper.GetVueForm(formData, 4),
                        };
                        break;
                    }
                case CodeGeneratorVbenTemplateNames.Vben_detail:
                    {
                        var detail = types.FirstOrDefault(a => a.Name == $"{model.Entity}DetailOutput");
                        var viewData = GetPropertyInfo(detail, ignoreProperties: new[] { "id", "concurrencyStamp" });
                        data = new TemplateVueDetailModel
                        {
                            View = viewData,
                            ViewString = _codeGeneratorVueVbenHelper.GetVueDetail(viewData),
                        };
                        break;
                    }

                case CodeGeneratorVbenTemplateNames.Vben_api:
                    {
                        data = new TemplateVueApiModel()
                        {

                        };
                        break;
                    }
                case CodeGeneratorVbenTemplateNames.Vben_detailDrawer:
                    {
                        data = new TemplateVueDetailDrawerModel()
                        {

                        };
                        break;
                    }
                default:
                    throw new UserFriendlyException($"模板【{template}】输出未实现");
            }

            if (data is TemplateVueModel m)
            {
                m.NameSpace = model.NameSpace;
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

                info.IsSlot = dictAttr?.Slot == true || enumAttr?.Slot == true;

                data.Add(info);
            }

            return data;
        }

        /// <summary>
        /// 获取程序集
        /// </summary>
        /// <param name="nameSpace"></param>
        /// <returns></returns>
        protected virtual IEnumerable<Type> GetTypes(string nameSpace)
        {
            return AppDomain.CurrentDomain.GetAssemblies()
                .Where(x => x.FullName.StartsWith(nameSpace))
                .SelectMany(a => a.GetTypes());
        }
    }

}