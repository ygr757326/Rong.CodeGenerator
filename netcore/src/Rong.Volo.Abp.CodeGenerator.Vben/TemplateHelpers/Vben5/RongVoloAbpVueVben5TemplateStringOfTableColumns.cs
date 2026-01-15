using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Options;
using Rong.Volo.Abp.CodeGenerator.Vue.Models;
using System.Text;
using Rong.Volo.Abp.CodeGenerator.Vue.Enums;
using Volo.Abp.DependencyInjection;

namespace Rong.Volo.Abp.CodeGenerator.Vue.TemplateHelpers.Vben5
{
    /// <summary>
    /// vben模板 -  表头 columns
    /// </summary>
    public class RongVoloAbpVueVben5TemplateStringOfTableColumns : RongVoloAbpVueTemplateBase, ISingletonDependency
    {

        public RongVoloAbpVueVben5TemplateStringOfTableColumns(IOptions<RongVoloAbpCodeGeneratorVueOptions> options) : base(options)
        {
        }

        /// <summary>
        /// 默认模板
        /// </summary>
        /// <returns></returns>
        public virtual string? DefaultTemplate(TemplateVueEntityPropertyData item, int space = 6)
        {
            StringBuilder b = new StringBuilder();

            b.Space(space).AppendLine("{");

            b.Space(space + 2).AppendLine($"title: '{item.DisplayName}',");
            b.Space(space + 2).AppendLine($"field: {FormatPropertyCaseForfield(item.PropertyCase)},");

            if (item.TableSorter)
            {
                b.Space(space + 2).AppendLine($"sortable: true,");
            }

            b.Space(space).AppendLine("},");

            return b.ToString();
        }

        /// <summary>
        /// 日期模板
        /// </summary>
        /// <returns></returns>
        public virtual string? DateTimeTemplate(TemplateVueEntityPropertyData item, int space = 6)
        {
            StringBuilder b = new StringBuilder();

            b.Space(space).AppendLine("{");

            b.Space(space + 2).AppendLine($"title: '{item.DisplayName}',");
            b.Space(space + 2).AppendLine($"field: {FormatPropertyCaseForfield(item.PropertyCase)},");
            b.Space(space + 2).AppendLine($"formatter: ({{ cellValue }}) => {{ ");
            b.Space(space + 4).AppendLine($"return formatToDate(cellValue, '{item.DateFormat}');");
            b.Space(space + 2).AppendLine($"}},");

            if (item.TableSorter)
            {
                b.Space(space + 2).AppendLine($"sortable: true,");
            }

            b.Space(space).AppendLine("},");

            return b.ToString();
        }


        /// <summary>
        /// 枚举模板
        /// </summary>
        /// <returns></returns>
        public virtual string? EnumTemplate(TemplateVueEntityPropertyData item, int space = 6)
        {
            StringBuilder b = new StringBuilder();

            b.Space(space).AppendLine("{");

            b.Space(space + 2).AppendLine($"title: '{item.DisplayName}',");
            b.Space(space + 2).AppendLine($"field: {FormatPropertyCaseForfield(item.PropertyCase)},");

            if (item.IsSlot)
            {
                b.Space(space + 2).AppendLine($"slots: {{ default: '{FormatPropertyCaseForSlot(item.PropertyCase)}' }},");
            }
            else
            {
                b.Space(space + 2).AppendLine($"formatter: ({{ cellValue }}) => {{ ");

                if (item.IsEnumMultiple)
                {
                    b.Space(space + 4).AppendLine($"return (cellValue || []).map((a) => enumStore?.findName('{item.PropertyType.Name}', a)).join(',');");
                }
                else
                {
                    b.Space(space + 4).AppendLine($"return enumStore?.findName('{item.PropertyType.Name}', cellValue);");
                }

                b.Space(space + 2).AppendLine($"}},");
            }
            if (item.TableSorter)
            {
                b.Space(space + 2).AppendLine($"sortable: true,");
            }

            b.Space(space).AppendLine("},");

            return b.ToString();
        }

        /// <summary>
        /// 枚举插槽
        /// </summary>
        /// <returns></returns>
        public virtual string? EnumSlots(TemplateVueEntityPropertyData item, int space = 6)
        {
            StringBuilder b = new StringBuilder();

            string filed = FormatPropertyCaseForSlot(item.PropertyCase);

            b.Space(space).AppendLine($"<template #{filed}=\"{{ row }}\">");

            if (item.IsDictionaryMultiple)
            {
                b.Space(space + 2).AppendLine($"<a-tag color=\"\"> v-for=\"(item, i) in row.{filed} || []\" :key=\"i\">");
                b.Space(space + 2).AppendLine($" {{{{ enumStore?.findName('{item.PropertyType.Name}', item) }}}}");
                b.Space(space + 2).AppendLine($"</a-tag>");
            }
            else
            {
                b.Space(space + 2).AppendLine($"<a-tag color=\"\">");
                b.Space(space + 2).AppendLine($" {{{{ enumStore?.findName('{item.PropertyType.Name}', row.{filed}) }}}}");
                b.Space(space + 2).AppendLine($"</a-tag>");
            }

            b.Space(space).AppendLine($"</template>");

            return b.ToString();
        }

        /// <summary>
        /// 字典模板
        /// </summary>
        /// <returns></returns>
        public virtual string? DictionaryTemplate(TemplateVueEntityPropertyData item, int space = 6)
        {
            StringBuilder b = new StringBuilder();

            b.Space(space).AppendLine("{");

            b.Space(space + 2).AppendLine($"title: '{item.DisplayName}',");
            b.Space(space + 2).AppendLine($"field: {FormatPropertyCaseForfield(item.PropertyCase)},");

            if (item.IsSlot)
            {
                b.Space(space + 2).AppendLine($"slots: {{ default: '{FormatPropertyCaseForSlot(item.PropertyCase)}' }},");
            }
            else
            {
                b.Space(space + 2).AppendLine($"formatter: ({{ cellValue }}) => {{ ");

                if (item.IsDictionaryMultiple)
                {
                    b.Space(space + 4).AppendLine($"return (cellValue || []).map((a) => dictStore?.findName('{item.DictionaryCode}', a)).join(',');");
                }
                else
                {
                    b.Space(space + 4).AppendLine($"return dictStore?.findName('{item.DictionaryCode}', cellValue);");
                }

                b.Space(space + 2).AppendLine($"}},");
            }

            if (item.TableSorter)
            {
                b.Space(space + 2).AppendLine($"sortable: true,");
            }

            b.Space(space).AppendLine("},");

            return b.ToString();
        }

        /// <summary>
        /// 字典插槽
        /// </summary>
        /// <returns></returns>
        public virtual string? DictionarySlot(TemplateVueEntityPropertyData item, int space = 6)
        {
            StringBuilder b = new StringBuilder();

            string filed = FormatPropertyCaseForSlot(item.PropertyCase);
            b.Space(space).AppendLine($"<template #{filed}=\"{{ row }}\">");//value, record

            if (item.IsDictionaryMultiple)
            {
                b.Space(space + 2).AppendLine($"<a-tag color=\"\"> v-for=\"(item, i) in row.{filed} || []\" :key=\"i\">");
                b.Space(space + 2).AppendLine($" {{{{ dictStore?.findName('{item.DictionaryCode}', item) }}}}");
                b.Space(space + 2).AppendLine($"</a-tag>");
            }
            else
            {
                b.Space(space + 2).AppendLine($"<a-tag color=\"\">");
                b.Space(space + 2).AppendLine($" {{{{ dictStore?.findName('{item.DictionaryCode}', row.{filed}) }}}}");
                b.Space(space + 2).AppendLine($"</a-tag>");
            }

            b.Space(space).AppendLine($"</template>");

            return b.ToString();
        }

        /// <summary>
        /// bool模板
        /// </summary>
        /// <returns></returns>
        public virtual string? BoolTemplate(TemplateVueEntityPropertyData item, int space = 6)
        {
            StringBuilder b = new StringBuilder();

            b.Space(space).AppendLine("{");

            b.Space(space + 2).AppendLine($"title: '{item.DisplayName}',");
            b.Space(space + 2).AppendLine($"field: {FormatPropertyCaseForfield(item.PropertyCase)},");

            if (item.IsSlot)
            {
                b.Space(space + 2).AppendLine($"slots: {{ default: '{FormatPropertyCaseForSlot(item.PropertyCase)}' }},");
            }
            else
            {
                b.Space(space + 2).AppendLine($"formatter: ({{ cellValue }}) => {{ ");
                b.Space(space + 4).AppendLine($"return cellValue ? '是' : '否';");
                b.Space(space + 2).AppendLine($"}},");
            }

            if (item.TableSorter)
            {
                b.Space(space + 2).AppendLine($"sortable: true,");
            }

            b.Space(space).AppendLine("},");

            return b.ToString();
        }

        /// <summary>
        /// bool插槽
        /// </summary>
        /// <returns></returns>
        public virtual string? BoolSlot(TemplateVueEntityPropertyData item, int space = 6)
        {
            StringBuilder b = new StringBuilder();
            string filed = FormatPropertyCaseForSlot(item.PropertyCase);

            b.Space(space).AppendLine($"<template #{filed}=\"{{ row }}\">");
            b.Space(space + 2).AppendLine($"<a-tag :color=\"row.{filed} ? 'green' : 'red'\">");
            b.Space(space + 2).AppendLine($" {{{{ row.{filed} ? '是' : '否' }}}}");
            b.Space(space + 2).AppendLine($"</a-tag>");
            b.Space(space).AppendLine($"</template>");

            return b.ToString();
        }

        /// <summary>
        /// 图片预览模板
        /// </summary>
        /// <returns></returns>
        public virtual string? ImagePreviewTemplate(TemplateVueEntityPropertyData item, int space = 6)
        {
            StringBuilder b = new StringBuilder();

            b.Space(space).AppendLine("{");

            b.Space(space + 2).AppendLine($"title: '{item.DisplayName}',");
            b.Space(space + 2).AppendLine($"field: {FormatPropertyCaseForfield(item.PropertyCase)},");

            if (item.IsSlot)
            {
                b.Space(space + 2).AppendLine($"slots: {{ default: '{FormatPropertyCaseForSlot(item.PropertyCase)}' }},");
            }

            b.Space(space).AppendLine("},");

            return b.ToString();
        }

        /// <summary>
        /// 图片预览插槽
        /// </summary>
        /// <returns></returns>
        public virtual string? ImagePreviewSlot(TemplateVueEntityPropertyData item, int space = 6)
        {
            var componentName = Options.ImagePreviewComponent ?? GetMapComponent("BaseUpload");

            string filed = FormatPropertyCaseForSlot(item.PropertyCase);

            StringBuilder b = new StringBuilder();

            b.Space(space).AppendLine($"<template #{filed}=\"{{ row }}\">");

            b.Space(space + 2).Append($"<{componentName} :width=\"50\" :height=\"50\"");

            if (item.MultipleFile)
            {
                b.Append($" v-for=\"(item, i) in row.{filed}\" :{Options.ImagePreviewComponentProp ?? "src"}=\"item\" :key=\"i\" ");
            }
            else
            {
                b.Append($" :{Options.ImagePreviewComponentProp ?? "src"}=\"row.{filed}\" ");
            }

            b.AppendLine($"></{componentName}>");


            b.Space(space).AppendLine($"</template>");

            return b.ToString();
        }


        /// <summary>
        /// 文件预览模板
        /// </summary>
        /// <returns></returns>
        public virtual string? FilePreviewTemplate(TemplateVueEntityPropertyData item, int space = 6)
        {
            StringBuilder b = new StringBuilder();

            b.Space(space).AppendLine("{");

            b.Space(space + 2).AppendLine($"title: '{item.DisplayName}',");
            b.Space(space + 2).AppendLine($"field: {FormatPropertyCaseForfield(item.PropertyCase)},");

            if (item.IsSlot)
            {
                b.Space(space + 2).AppendLine($"slots: {{ default: '{FormatPropertyCaseForSlot(item.PropertyCase)}' }},");
            }

            b.Space(space).AppendLine("},");

            return b.ToString();
        }

        /// <summary>
        /// 文件预览插槽
        /// </summary>
        /// <returns></returns>
        public virtual string? FilePreviewSlot(TemplateVueEntityPropertyData item, int space = 6)
        {
            var componentName = Options.FilePreviewComponent ?? GetMapComponent("BaseUpload");

            string filed = FormatPropertyCaseForSlot(item.PropertyCase);

            StringBuilder b = new StringBuilder();

            b.Space(space).AppendLine($"<template #{filed}=\"{{ row }}\">");

            b.Space(space + 2).Append($"<{componentName}");
            if (item.MultipleFile)
            {
                b.Append($" :{Options.FilePreviewComponentProp ?? "v-model"}=\"row.{filed}\"  listType=\"text\" :disabled=\"true\" ");
            }
            else
            {
                b.Append($" :{Options.FilePreviewComponentProp ?? "v-model"}=\"[ row.{filed} ]\"  listType=\"text\" :disabled=\"true\" ");
            }

            b.AppendLine($"></{componentName}>");


            b.Space(space).AppendLine($"</template>");

            return b.ToString();
        }

        /// <summary>
        /// 编辑器插槽
        /// </summary>
        /// <returns></returns>
        public virtual string? EditorSlot(TemplateVueEntityPropertyData item, int space = 6)
        {
            string filed = FormatPropertyCaseForSlot(item.PropertyCase);

            StringBuilder b = new StringBuilder();

            b.Space(space).AppendLine($"<template #{FormatPropertyCaseForSlot(item.PropertyCase)}=\"{{ row }}\">");

            b.Space(space + 2).Append($"<p v-html=\"row.{filed}\" />");

            b.Space(space).AppendLine($"</template>");

            return b.ToString();
        }



        /// <summary>
        /// 格式化属性名称 - 插槽
        /// </summary>
        /// <param name="propertyCase"></param>
        /// <returns></returns>
        protected virtual string FormatPropertyCaseForSlot(string propertyCase)
        {
            return propertyCase.Replace(".", "_")
                .Replace("[", "")
                .Replace("]", "")
                .Replace(" ", "")
                .Replace("'", "")
                .Replace(",", "_");

        }

        /// <summary>
        /// 格式化属性名称 - field
        /// </summary>
        /// <param name="propertyCase"></param>
        /// <returns></returns>
        protected virtual string FormatPropertyCaseForfield(string propertyCase)
        {
            if (Options.AntTabledDataIndexMode.Equals(AntTabledDataIndexModeEnum.Dotted))
            {
                return $"'{propertyCase}'";
            }

            propertyCase = propertyCase ?? string.Empty;
            var p = propertyCase.Split('.');
            var data = p.Select(a => $"'{a}'").JoinAsString(", ");
            return $"[{data}]";
        }
    }
}