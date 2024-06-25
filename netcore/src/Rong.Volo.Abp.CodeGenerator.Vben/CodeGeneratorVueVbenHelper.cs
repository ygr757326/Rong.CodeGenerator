using Rong.Volo.Abp.CodeGenerator.Vue.Enums;
using Rong.Volo.Abp.CodeGenerator.Vue.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using Volo.Abp.DependencyInjection;

namespace Rong.Volo.Abp.CodeGenerator.Vue
{
    /// <summary>
    /// vben代码生成器帮助器
    /// </summary>
    public class CodeGeneratorVueVbenHelper : ISingletonDependency
    {
        /// <summary>
        /// 获取vue详情
        /// </summary>
        /// <returns></returns>
        public string? GetVueDetail(List<TemplateVueModelData> models, int space = 8)
        {
            if (models == null)
            {
                return null;
            }

            StringBuilder b = new StringBuilder();
            b.AppendLine();

            foreach (var item in models)
            {
                var typeCode = item.PropertyType.GetMyTypeCode();

                b.Space(space).Append($"<a-descriptions-item label=\"{@item.DisplayName}\">");
                b.Append($" {{{{ ");

                if (typeCode == TypeCode.DateTime)
                {
                    b.Append($"formatToDate(detailData?.{@item.PropertyCase})");
                }
                else if (item.IsEnum)
                {
                    b.Append($"enumStore?.findName('{item.PropertyType.Name}',detailData?.{@item.PropertyCase})");
                }
                else if (item.IsDictionary)
                {
                    b.Append($"dictStore?.findName('{item.DictionaryCode}',detailData?.{@item.PropertyCase})");
                }
                else if (new[] { TypeCode.Boolean }.Contains(typeCode))
                {
                    b.Append($"detailData?.{@item.PropertyCase} ? '是' : '否'");
                }
                else
                {
                    b.Append($"detailData?.{@item.PropertyCase}");
                }
                b.Append($" }}}} ");
                b.AppendLine("</a-descriptions-item>");
            }

            b.AppendLine();
            return b.ToString();
        }

        /// <summary>
        /// 获取vue表头
        /// </summary>
        /// <returns></returns>
        public string? GetVueTable(List<TemplateVueModelData> models, int space = 6)
        {
            if (models == null)
            {
                return null;
            }

            StringBuilder b = new StringBuilder();
            foreach (var item in models)
            {
                var typeCode = item.PropertyType.GetMyTypeCode();

                b.Space(space).AppendLine("{");
                b.Space(space + 2).AppendLine($"title: '{@item.DisplayName}',");
                b.Space(space + 2).AppendLine($"dataIndex: '{@item.PropertyCase}',");

                if (typeCode == TypeCode.DateTime || item.PropertyType == typeof(DateTime?))
                {
                    b.Space(space + 2).AppendLine($"customRender: ({{ value }}) => {{ ");
                    b.Space(space + 4).AppendLine($"return formatToDate(value);");
                    b.Space(space + 2).AppendLine($"}},");
                }
                else if (item.IsEnum)
                {
                    b.Space(space + 2).AppendLine($"customRender: ({{ value }}) => {{ ");
                    b.Space(space + 4).AppendLine($"return enumStore?.findName('{item.PropertyType.Name}',value);");
                    b.Space(space + 2).AppendLine($"}},");
                }
                else if (item.IsDictionary)
                {
                    b.Space(space + 2).AppendLine($"customRender: ({{ value }}) => {{ ");
                    b.Space(space + 4).AppendLine($"return dictStore?.findName('{item.DictionaryCode}',value);");
                    b.Space(space + 2).AppendLine($"}},");
                }
                else if (new[] { TypeCode.Boolean }.Contains(typeCode))
                {
                    b.Space(space + 2).AppendLine($"customRender: ({{ value }}) => {{ ");
                    b.Space(space + 4).AppendLine($"return value ? '是' : '否';");
                    b.Space(space + 2).AppendLine($"}},");
                }

                if (item.TableSorter)
                {
                    b.Space(space + 2).AppendLine($"sorter: true,");
                }

                b.Space(space).AppendLine("},");
            }
            return b.ToString();
        }

        /// <summary>
        /// 获取表单
        /// </summary>
        /// <returns></returns>
        public string? GetVueForm(List<TemplateVueModelData> models, int space = 6)
        {
            if (models == null)
            {
                return null;
            }

            StringBuilder b = new StringBuilder();
            foreach (var item in models)
            {
                b.Space(space).AppendLine("{");
                b.Space(space + 2).AppendLine($"label: '{@item.DisplayName}',");
                b.Space(space + 2).AppendLine($"field: '{@item.PropertyCase}',");

                var typeCode = item.PropertyType.GetMyTypeCode();

                if (typeCode == TypeCode.DateTime)
                {
                    b.Space(space + 2).AppendLine($"component: 'DatePicker',");
                    b.Space(space + 2).AppendLine($"componentProps: {{");
                    b.Space(space + 4).AppendLine($"valueFormat: 'YYYY-MM-DD'");
                    b.Space(space + 4).AppendLine($"}},");
                }
                else if (item.IsEnum)
                {
                    if (item.SelectMode == VueSelectModeEnum.Radio)
                    {
                        b.Space(space + 2).AppendLine($"component: 'RadioGroup',");
                    }
                    else
                    {
                        b.Space(space + 2).AppendLine($"component: 'Select',");
                    }

                    b.Space(space + 2).AppendLine($"componentProps: {{");
                    b.Space(space + 4).AppendLine($"options: enumStore?.findCodeSelect('{item.PropertyType.Name}'),");
                    b.Space(space + 4).AppendLine($"showSearch: true,");
                    b.Space(space + 4).AppendLine($"optionFilterProp: 'label'");
                    b.Space(space + 2).AppendLine($"}},");
                }
                else if (new[] { TypeCode.Byte, TypeCode.Int16, TypeCode.Int32, TypeCode.Int64, TypeCode.UInt16, TypeCode.UInt32, TypeCode.UInt64 }.Contains(typeCode))
                {
                    b.Space(space + 2).AppendLine($"component: 'InputNumber',");
                    b.Space(space + 2).AppendLine($"componentProps: {{ precision: 0 }},");
                }
                else if (new[] { TypeCode.Double }.Contains(typeCode))
                {
                    b.Space(space + 2).AppendLine($"component: 'InputNumber',");
                    b.Space(space + 2).AppendLine($"componentProps: {{ precision: 2 }},");
                }
                else if (new[] { TypeCode.Single }.Contains(typeCode))
                {
                    b.Space(space + 2).AppendLine($"component: 'InputNumber',");
                    b.Space(space + 2).AppendLine($"componentProps: {{ precision: 1 }},");
                }
                else if (new[] { TypeCode.Decimal }.Contains(typeCode))
                {
                    int length = 2;
                    var column = item.PropertyType.GetCustomAttribute<ColumnAttribute>();

                    if (column?.TypeName != null)
                    {
                        string pattern = @"\((.*?)\)";
                        var match = Regex.Match(column.TypeName, pattern);
                        if (match.Success)
                        {
                            var de = match.Groups[1].Value.Split(",");
                            if (de.Length == 2)
                            {
                                int.TryParse(de[1], out length);
                            }
                        }
                    }
                    b.Space(space + 2).AppendLine($"component: 'InputNumber',");
                    b.Space(space + 2).AppendLine($"componentProps: {{ precision: {length} }},");
                }
                else if (new[] { TypeCode.Boolean }.Contains(typeCode))
                {
                    var defaultValue = item.PropertyType.GetCustomAttribute<DefaultValueAttribute>();
                    b.Space(space + 2).AppendLine($"component: 'Switch',");
                    if (defaultValue?.Value != null)
                    {
                        b.Space(space + 2).AppendLine($"defaultValue: '{defaultValue.Value.ToString()?.ToCamelCase()}',");
                    }
                }
                else if (item.IsDictionary)
                {
                    if (item.SelectMode == VueSelectModeEnum.Radio)
                    {
                        b.Space(space + 2).AppendLine($"component: 'RadioGroup',");
                    }
                    else
                    {
                        b.Space(space + 2).AppendLine($"component: 'Select',");
                    }

                    b.Space(space + 2).AppendLine($"componentProps: {{");
                    b.Space(space + 4).AppendLine($"options: dictStore?.findCodeSelect('{item.DictionaryCode}'),");
                    b.Space(space + 4).AppendLine($"showSearch: true,");
                    b.Space(space + 4).AppendLine($"optionFilterProp: 'label'");
                    b.Space(space + 2).AppendLine($"}},");
                }
                else if (item.IsFile)
                {
                    b.Space(space + 2).AppendLine($"component: 'ImageUpload',");
                }
                else
                {
                    b.Space(space + 2).AppendLine($"component: 'Input',");
                }

                if (item.IsRequired)
                {
                    b.Space(space + 2).AppendLine($"required: true,");
                }

                if (item.Property.Equals("id", StringComparison.CurrentCultureIgnoreCase) ||
                    item.Property.Equals("concurrencyStamp", StringComparison.CurrentCultureIgnoreCase))
                {
                    b.Space(space + 2).AppendLine($"show: false,");
                }

                b.Space(space).AppendLine("},");
            }
            return b.ToString();
        }

    }

}