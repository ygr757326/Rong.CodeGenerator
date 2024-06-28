using Microsoft.Extensions.Options;
using Rong.Volo.Abp.CodeGenerator.Vue.Models;
using System.Text;
using Volo.Abp.DependencyInjection;

namespace Rong.Volo.Abp.CodeGenerator.Vue.TemplateHelpers.Vbens
{
    /// <summary>
    /// vben模板 -  表头 columns
    /// </summary>
    public class CodeGeneratorVueVbenTemplateStringOfTableColumns : CodeGeneratorVueTemplateBase, ISingletonDependency
    {

        public CodeGeneratorVueVbenTemplateStringOfTableColumns(IOptions<CodeGeneratorVueOptions> options) : base(options)
        {
        }

        /// <summary>
        /// 默认模板
        /// </summary>
        /// <returns></returns>
        public virtual string? DefaultTemplate(TemplateVueModelData item, int space = 6)
        {
            StringBuilder b = new StringBuilder();

            b.Space(space).AppendLine("{");

            b.Space(space + 2).AppendLine($"title: '{item.DisplayName}',");
            b.Space(space + 2).AppendLine($"dataIndex: '{item.PropertyCase}',");

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
        public virtual string? DateTimeTemplate(TemplateVueModelData item, int space = 6)
        {
            StringBuilder b = new StringBuilder();

            b.Space(space).AppendLine("{");

            b.Space(space + 2).AppendLine($"title: '{item.DisplayName}',");
            b.Space(space + 2).AppendLine($"dataIndex: '{item.PropertyCase}',");
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
        public virtual string? EnumTemplate(TemplateVueModelData item, int space = 6)
        {
            StringBuilder b = new StringBuilder();

            b.Space(space).AppendLine("{");

            b.Space(space + 2).AppendLine($"title: '{item.DisplayName}',");
            b.Space(space + 2).AppendLine($"dataIndex: '{item.PropertyCase}',");

            if (item.IsSlot)
            {
                b.Space(space + 2).AppendLine($"slots: {{ customRender: '{item.PropertyCase}' }},");
            }
            else
            {
                b.Space(space + 2).AppendLine($"customRender: ({{ value }}) => {{ ");
                b.Space(space + 4).AppendLine($"return enumStore?.findName('{item.PropertyType.Name}', value);");
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
        public virtual string? EnumSlots(TemplateVueModelData item, int space = 6)
        {
            StringBuilder b = new StringBuilder();

            b.Space(space).AppendLine($"<template #{item.PropertyCase}=\"{{ value }}\">");//value, record
            b.Space(space + 2).AppendLine($"<Tag color=\"\">");
            b.Space(space + 2).AppendLine($" {{{{ enumStore?.findName('{item.PropertyType.Name}', value) }}}}");
            b.Space(space + 2).AppendLine($"</Tag>");
            b.Space(space).AppendLine($"</template>");

            return b.ToString();
        }

        /// <summary>
        /// 字典模板
        /// </summary>
        /// <returns></returns>
        public virtual string? DictionaryTemplate(TemplateVueModelData item, int space = 6)
        {
            StringBuilder b = new StringBuilder();

            b.Space(space).AppendLine("{");

            b.Space(space + 2).AppendLine($"title: '{item.DisplayName}',");
            b.Space(space + 2).AppendLine($"dataIndex: '{item.PropertyCase}',");

            if (item.IsSlot)
            {
                b.Space(space + 2).AppendLine($"slots: {{ customRender: '{item.PropertyCase}' }},");
            }
            else
            {
                b.Space(space + 2).AppendLine($"customRender: ({{ value }}) => {{ ");
                b.Space(space + 4).AppendLine($"return dictStore?.findName('{item.DictionaryCode}', value);");
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
        public virtual string? DictionarySlot(TemplateVueModelData item, int space = 6)
        {
            StringBuilder b = new StringBuilder();

            b.Space(space).AppendLine($"<template #{item.PropertyCase}=\"{{ value }}\">");//value, record
            b.Space(space + 2).AppendLine($"<Tag color=\"\">");
            b.Space(space + 2).AppendLine($" {{{{ dictStore?.findName('{item.DictionaryCode}', value) }}}}");
            b.Space(space + 2).AppendLine($"</Tag>");
            b.Space(space).AppendLine($"</template>");

            return b.ToString();
        }

        /// <summary>
        /// bool模板
        /// </summary>
        /// <returns></returns>
        public virtual string? BoolTemplate(TemplateVueModelData item, int space = 6)
        {
            StringBuilder b = new StringBuilder();

            b.Space(space).AppendLine("{");

            b.Space(space + 2).AppendLine($"title: '{item.DisplayName}',");
            b.Space(space + 2).AppendLine($"dataIndex: '{item.PropertyCase}',");

            if (item.IsSlot)
            {
                b.Space(space + 2).AppendLine($"slots: {{ customRender: '{item.PropertyCase}' }},");
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
        public virtual string? BoolSlot(TemplateVueModelData item, int space = 6)
        {
            StringBuilder b = new StringBuilder();

            b.Space(space).AppendLine($"<template #{item.PropertyCase}=\"{{ value }}\">");//value, record
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
        public virtual string? ImagePreviewTemplate(TemplateVueModelData item, int space = 6)
        {
            StringBuilder b = new StringBuilder();

            b.Space(space).AppendLine("{");

            b.Space(space + 2).AppendLine($"title: '{item.DisplayName}',");
            b.Space(space + 2).AppendLine($"dataIndex: '{item.PropertyCase}',");

            if (item.IsSlot)
            {
                b.Space(space + 2).AppendLine($"slots: {{ customRender: '{item.PropertyCase}' }},");
            }

            b.Space(space).AppendLine("},");

            return b.ToString();
        }

        /// <summary>
        /// 图片预览插槽
        /// </summary>
        /// <returns></returns>
        public virtual string? ImagePreviewSlot(TemplateVueModelData item, int space = 6)
        {
            var componentName = Options.ImagePreviewComponent ?? GetMapComponent("BaseUpload");

            StringBuilder b = new StringBuilder();

            b.Space(space).AppendLine($"<template #{item.PropertyCase}=\"{{ value }}\">");//value, record

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
        /// 文件预览插槽
        /// </summary>
        /// <returns></returns>
        public virtual string? FilePreviewSlot(TemplateVueModelData item, int space = 6)
        {
            var componentName = Options.FilePreviewComponent ?? GetMapComponent("BaseUpload");

            StringBuilder b = new StringBuilder();

            b.Space(space).AppendLine($"<template #{item.PropertyCase}=\"{{ value }}\">");//value, record

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
        public virtual string? EditorSlot(TemplateVueModelData item, int space = 6)
        {
            StringBuilder b = new StringBuilder();

            b.Space(space).AppendLine($"<template #{item.PropertyCase}=\"{{ value }}\">");//value, record

            b.Space(space + 2).Append($"<p v-html=\"value\" />");

            b.Space(space).AppendLine($"</template>");

            return b.ToString();
        }
        
    }
}