using Microsoft.Extensions.Options;
using Rong.Volo.Abp.CodeGenerator.Vue.Models;
using System.Text;
using Volo.Abp.DependencyInjection;

namespace Rong.Volo.Abp.CodeGenerator.Vue.TemplateHelpers.Vbens
{
    /// <summary>
    /// vben模板 - 详情页
    /// </summary>
    public class CodeGeneratorVueVbenTemplateStringOfDetail : CodeGeneratorVueTemplateBase, ISingletonDependency
    {

        public CodeGeneratorVueVbenTemplateStringOfDetail(IOptions<CodeGeneratorVueOptions> options) : base(options)
        {
        }

        /// <summary>
        /// 默认模板
        /// </summary>
        /// <returns></returns>
        public virtual string? DefaultTemplate(TemplateVueModelData item, int space = 8)
        {
            StringBuilder b = new StringBuilder();
            b.Space(space).AppendLine($"<{GetMapComponent("a-descriptions-item")} label=\"{item.DisplayName}\">");
            b.Space(space + 2).AppendLine($" {{{{ detailData?.{item.PropertyCase}  }}}} ");
            b.Space(space).AppendLine($"</{GetMapComponent("a-descriptions-item")}>");

            return b.ToString();
        }

        /// <summary>
        /// 日期模板
        /// </summary>
        /// <returns></returns>
        public virtual string? DateTimeTemplate(TemplateVueModelData item, int space = 8)
        {
            StringBuilder b = new StringBuilder();
            b.Space(space).AppendLine($"<{GetMapComponent("a-descriptions-item")} label=\"{item.DisplayName}\">");
            b.Space(space + 2).AppendLine($" {{{{ formatToDate(detailData?.{item.PropertyCase})  }}}} ");
            b.Space(space).AppendLine($"</{GetMapComponent("a-descriptions-item")}>");

            return b.ToString();
        }

        /// <summary>
        /// 枚举模板
        /// </summary>
        /// <returns></returns>
        public virtual string? EnumTemplate(TemplateVueModelData item, int space = 8)
        {
            StringBuilder b = new StringBuilder();
            b.Space(space).AppendLine($"<{GetMapComponent("a-descriptions-item")} label=\"{item.DisplayName}\">");

            if (item.IsSlot)
            {
                b.Space(space + 2).AppendLine($"<Tag color=\"\">");
                b.Space(space + 2).AppendLine($" {{{{ enumStore?.findName('{item.PropertyType.Name}', detailData?.{item.PropertyCase}) }}}} ");
                b.Space(space + 2).AppendLine($"</Tag>");
            }
            else
            {
                b.Space(space + 2).AppendLine($" {{{{ enumStore?.findName('{item.PropertyType.Name}', detailData?.{item.PropertyCase})  }}}} ");
            }

            b.Space(space).AppendLine($"</{GetMapComponent("a-descriptions-item")}>");

            return b.ToString();
        }

        /// <summary>
        /// 字典模板
        /// </summary>
        /// <returns></returns>
        public virtual string? DictionaryTemplate(TemplateVueModelData item, int space = 8)
        {
            StringBuilder b = new StringBuilder();
            b.Space(space).AppendLine($"<{GetMapComponent("a-descriptions-item")} label=\"{item.DisplayName}\">");

            if (item.IsSlot)
            {
                b.Space(space + 2).AppendLine($"<Tag color=\"\">");
                b.Space(space + 2).AppendLine($" {{{{ dictStore?.findName('{item.DictionaryCode}', detailData?.{item.PropertyCase}) }}}} ");
                b.Space(space + 2).AppendLine($"</Tag>");
            }
            else
            {
                b.Space(space + 2).AppendLine($" {{{{ dictStore?.findName('{item.DictionaryCode}', detailData?.{item.PropertyCase})  }}}} ");
            }

            b.Space(space).AppendLine($"</{GetMapComponent("a-descriptions-item")}>");

            return b.ToString();
        }

        /// <summary>
        /// bool模板
        /// </summary>
        /// <returns></returns>
        public virtual string? BoolTemplate(TemplateVueModelData item, int space = 8)
        {
            StringBuilder b = new StringBuilder();
            b.Space(space).AppendLine($"<{GetMapComponent("a-descriptions-item")} label=\"{item.DisplayName}\">");

            b.Space(space + 2).AppendLine($"<Tag :color=\"detailData?.{item.PropertyCase} ? 'green' : 'red'\">");
            b.Space(space + 2).AppendLine($" {{{{ detailData?.{item.PropertyCase} ? '是' : '否' }}}} ");
            b.Space(space + 2).AppendLine($"</Tag>");

            b.Space(space).AppendLine($"</{GetMapComponent("a-descriptions-item")}>");

            return b.ToString();
        }

        /// <summary>
        /// 图片预览模板
        /// </summary>
        /// <returns></returns>
        public virtual string? ImagePreviewTemplate(TemplateVueModelData item, int space = 8)
        {
            StringBuilder b = new StringBuilder();
            b.Space(space).AppendLine($"<{GetMapComponent("a-descriptions-item")} label=\"{item.DisplayName}\">");

            b.Space(space + 2).Append($"<{GetMapComponent("ImagePreview")} :width=\"100\" :height=\"100\"");

            if (item.MultipleFile)
            {
                b.Append($" v-for=\"(item, i) in detailData?.{item.PropertyCase}|| []\" :key=\"i\" :src=\"item\" ");
            }
            else
            {
                b.Append($" :src=\"detailData?.{item.PropertyCase}\" ");
            }

            b.AppendLine($"></{GetMapComponent("ImagePreview")}>");

            b.Space(space).AppendLine($"</{GetMapComponent("a-descriptions-item")}>");

            return b.ToString();
        }

    }
}