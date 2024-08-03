using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Options;
using Rong.Volo.Abp.CodeGenerator.Vue.Models;
using System.Text;
using Rong.Volo.Abp.CodeGenerator.Vue.Enums;
using Volo.Abp.DependencyInjection;

namespace Rong.Volo.Abp.CodeGenerator.Vue.TemplateHelpers.Vbens
{
    /// <summary>
    /// vben模板 -  表头 columns
    /// </summary>
    public class RongVoloAbpVueVbenTemplateStringOfTableColumns : RongVoloAbpVueTemplateBase, ISingletonDependency
    {

        public RongVoloAbpVueVbenTemplateStringOfTableColumns(IOptions<RongVoloAbpCodeGeneratorVueOptions> options) : base(options)
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
            b.Space(space + 2).AppendLine($"dataIndex: {FormatPropertyCaseForDataIndex(item.PropertyCase)},");

            if (item.TableSorter)
            {
                b.Space(space + 2).AppendLine($"sorter: true,");
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
            b.Space(space + 2).AppendLine($"dataIndex: {FormatPropertyCaseForDataIndex(item.PropertyCase)},");
            b.Space(space + 2).AppendLine($"customRender: ({{ value }}) => {{ ");
            b.Space(space + 4).AppendLine($"return formatToDate(value);");
            b.Space(space + 2).AppendLine($"}},");

            if (item.TableSorter)
            {
                b.Space(space + 2).AppendLine($"sorter: true,");
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
            b.Space(space + 2).AppendLine($"dataIndex: {FormatPropertyCaseForDataIndex(item.PropertyCase)},");

            if (item.IsSlot)
            {
                b.Space(space + 2).AppendLine($"slots: {{ customRender: '{FormatPropertyCaseForSlot(item.PropertyCase)}' }},");
            }
            else
            {
                b.Space(space + 2).AppendLine($"customRender: ({{ value }}) => {{ ");

                if (item.IsEnumMultiple)
                {
                    b.Space(space + 4).AppendLine($"return (value || []).map((a) => enumStore?.findName('{item.PropertyType.Name}', a)).join(',');");
                }
                else
                {
                    b.Space(space + 4).AppendLine($"return enumStore?.findName('{item.PropertyType.Name}', value);");
                }

                b.Space(space + 2).AppendLine($"}},");
            }
            if (item.TableSorter)
            {
                b.Space(space + 2).AppendLine($"sorter: true,");
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

            b.Space(space).AppendLine($"<template #{FormatPropertyCaseForSlot(item.PropertyCase)}=\"{{ value }}\">");//value, record

            if (item.IsDictionaryMultiple)
            {
                b.Space(space + 2).AppendLine($"<Tag color=\"\"> v-for=\"(item, i) in value || []\" :key=\"i\">");
                b.Space(space + 2).AppendLine($" {{{{ enumStore?.findName('{item.PropertyType.Name}', item) }}}}");
                b.Space(space + 2).AppendLine($"</Tag>");
            }
            else
            {
                b.Space(space + 2).AppendLine($"<Tag color=\"\">");
                b.Space(space + 2).AppendLine($" {{{{ enumStore?.findName('{item.PropertyType.Name}', value) }}}}");
                b.Space(space + 2).AppendLine($"</Tag>");
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
            b.Space(space + 2).AppendLine($"dataIndex: {FormatPropertyCaseForDataIndex(item.PropertyCase)},");

            if (item.IsSlot)
            {
                b.Space(space + 2).AppendLine($"slots: {{ customRender: '{FormatPropertyCaseForSlot(item.PropertyCase)}' }},");
            }
            else
            {
                b.Space(space + 2).AppendLine($"customRender: ({{ value }}) => {{ ");

                if (item.IsDictionaryMultiple)
                {
                    b.Space(space + 4).AppendLine($"return (value || []).map((a) => dictStore?.findName('{item.DictionaryCode}', a)).join(',');");
                }
                else
                {
                    b.Space(space + 4).AppendLine($"return dictStore?.findName('{item.DictionaryCode}', value);");
                }

                b.Space(space + 2).AppendLine($"}},");
            }

            if (item.TableSorter)
            {
                b.Space(space + 2).AppendLine($"sorter: true,");
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

            b.Space(space).AppendLine($"<template #{FormatPropertyCaseForSlot(item.PropertyCase)}=\"{{ value }}\">");//value, record

            if (item.IsDictionaryMultiple)
            {
                b.Space(space + 2).AppendLine($"<Tag color=\"\"> v-for=\"(item, i) in value || []\" :key=\"i\">");
                b.Space(space + 2).AppendLine($" {{{{ dictStore?.findName('{item.DictionaryCode}', item) }}}}");
                b.Space(space + 2).AppendLine($"</Tag>");
            }
            else
            {
                b.Space(space + 2).AppendLine($"<Tag color=\"\">");
                b.Space(space + 2).AppendLine($" {{{{ dictStore?.findName('{item.DictionaryCode}', value) }}}}");
                b.Space(space + 2).AppendLine($"</Tag>");
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
            b.Space(space + 2).AppendLine($"dataIndex: {FormatPropertyCaseForDataIndex(item.PropertyCase)},");

            if (item.IsSlot)
            {
                b.Space(space + 2).AppendLine($"slots: {{ customRender: '{FormatPropertyCaseForSlot(item.PropertyCase)}' }},");
            }
            else
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

            return b.ToString();
        }

        /// <summary>
        /// bool插槽
        /// </summary>
        /// <returns></returns>
        public virtual string? BoolSlot(TemplateVueEntityPropertyData item, int space = 6)
        {
            StringBuilder b = new StringBuilder();

            b.Space(space).AppendLine($"<template #{FormatPropertyCaseForSlot(item.PropertyCase)}=\"{{ value }}\">");//value, record
            b.Space(space + 2).AppendLine($"<Tag :color=\"value ? 'green' : 'red'\">");
            b.Space(space + 2).AppendLine($" {{{{ value ? '是' : '否' }}}}");
            b.Space(space + 2).AppendLine($"</Tag>");
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
            b.Space(space + 2).AppendLine($"dataIndex: {FormatPropertyCaseForDataIndex(item.PropertyCase)},");

            if (item.IsSlot)
            {
                b.Space(space + 2).AppendLine($"slots: {{ customRender: '{FormatPropertyCaseForSlot(item.PropertyCase)}' }},");
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

            StringBuilder b = new StringBuilder();

            b.Space(space).AppendLine($"<template #{FormatPropertyCaseForSlot(item.PropertyCase)}=\"{{ value }}\">");//value, record

            b.Space(space + 2).Append($"<{componentName} :width=\"50\" :height=\"50\"");

            if (item.MultipleFile)
            {
                b.Append($" v-for=\"(item, i) in value\" :{Options.ImagePreviewComponentProp ?? "src"}=\"item\" :key=\"i\" ");
            }
            else
            {
                b.Append($" :{Options.ImagePreviewComponentProp ?? "src"}=\"value\" ");
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
            b.Space(space + 2).AppendLine($"dataIndex: {FormatPropertyCaseForDataIndex(item.PropertyCase)},");

            if (item.IsSlot)
            {
                b.Space(space + 2).AppendLine($"slots: {{ customRender: '{FormatPropertyCaseForSlot(item.PropertyCase)}' }},");
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

            StringBuilder b = new StringBuilder();

            b.Space(space).AppendLine($"<template #{FormatPropertyCaseForSlot(item.PropertyCase)}=\"{{ value }}\">");//value, record

            b.Space(space + 2).Append($"<{componentName}");

            b.Append($" :{Options.FilePreviewComponentProp ?? "v-model"}=\"value\"  listType=\"text\" :disabled=\"true\" ");

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
            StringBuilder b = new StringBuilder();

            b.Space(space).AppendLine($"<template #{FormatPropertyCaseForSlot(item.PropertyCase)}=\"{{ value }}\">");//value, record

            b.Space(space + 2).Append($"<p v-html=\"value\" />");

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
        /// 格式化属性名称 - dataIndex
        /// </summary>
        /// <param name="propertyCase"></param>
        /// <returns></returns>
        protected virtual string FormatPropertyCaseForDataIndex(string propertyCase)
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