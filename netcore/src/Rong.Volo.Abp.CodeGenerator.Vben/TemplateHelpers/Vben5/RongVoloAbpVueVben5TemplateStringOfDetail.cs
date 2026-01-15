using System;
using System.Collections.Generic;
using Microsoft.Extensions.Options;
using Rong.Volo.Abp.CodeGenerator.Vue.Models;
using System.Linq;
using System.Text;
using Volo.Abp.DependencyInjection;

namespace Rong.Volo.Abp.CodeGenerator.Vue.TemplateHelpers.Vben5
{
    /// <summary>
    /// vben模板 - 详情页
    /// </summary>
    public class RongVoloAbpVueVben5TemplateStringOfDetail : RongVoloAbpVueTemplateBase, ISingletonDependency
    {

        public RongVoloAbpVueVben5TemplateStringOfDetail(IOptions<RongVoloAbpCodeGeneratorVueOptions> options) : base(options)
        {
        }

        /// <summary>
        /// 默认模板
        /// </summary>
        /// <returns></returns>
        public virtual string? DefaultTemplate(TemplateVueEntityPropertyData item, int space = 8)
        {
            StringBuilder b = new StringBuilder();
            b.Space(space).AppendLine($"<{GetMapComponent("a-descriptions-item")} label=\"{item.DisplayName}\">");
            b.Space(space + 2).AppendLine($" {{{{ detailData?.{FormatPropertyCase(item.PropertyCase)}  }}}} ");
            b.Space(space).AppendLine($"</{GetMapComponent("a-descriptions-item")}>");

            return b.ToString();
        }

        /// <summary>
        /// 日期模板
        /// </summary>
        /// <returns></returns>
        public virtual string? DateTimeTemplate(TemplateVueEntityPropertyData item, int space = 8)
        {
            StringBuilder b = new StringBuilder();
            b.Space(space).AppendLine($"<{GetMapComponent("a-descriptions-item")} label=\"{item.DisplayName}\">");
            b.Space(space + 2).AppendLine($" {{{{ formatToDate(detailData?.{FormatPropertyCase(item.PropertyCase)}, '{item.DateFormat}')  }}}} ");
            b.Space(space).AppendLine($"</{GetMapComponent("a-descriptions-item")}>");

            return b.ToString();
        }

        /// <summary>
        /// 枚举模板
        /// </summary>
        /// <returns></returns>
        public virtual string? EnumTemplate(TemplateVueEntityPropertyData item, int space = 8)
        {
            StringBuilder b = new StringBuilder();
            b.Space(space).AppendLine($"<{GetMapComponent("a-descriptions-item")} label=\"{item.DisplayName}\">");


            if (item.IsEnumMultiple)
            {
                b.Space(space + 2).AppendLine($"<a-tag color=\"\" v-for=\"(item, i) in detailData?.{FormatPropertyCase(item.PropertyCase)}|| []\" :key=\"i\">");
                b.Space(space + 2).AppendLine($" {{{{ enumStore?.findName('{item.PropertyType.Name}', item) }}}} ");
                b.Space(space + 2).AppendLine($"</a-tag>");

            }
            else
            {
                if (item.IsSlot)
                {
                    b.Space(space + 2).AppendLine($"<a-tag color=\"\">");
                    b.Space(space + 2)
                        .AppendLine(
                            $" {{{{ enumStore?.findName('{item.PropertyType.Name}', detailData?.{FormatPropertyCase(item.PropertyCase)}) }}}} ");
                    b.Space(space + 2).AppendLine($"</a-tag>");
                }
                else
                {
                    b.Space(space + 2)
                        .AppendLine(
                            $" {{{{ enumStore?.findName('{item.PropertyType.Name}', detailData?.{FormatPropertyCase(item.PropertyCase)})  }}}} ");
                }
            }

            b.Space(space).AppendLine($"</{GetMapComponent("a-descriptions-item")}>");

            return b.ToString();
        }

        /// <summary>
        /// 字典模板
        /// </summary>
        /// <returns></returns>
        public virtual string? DictionaryTemplate(TemplateVueEntityPropertyData item, int space = 8)
        {
            StringBuilder b = new StringBuilder();
            b.Space(space).AppendLine($"<{GetMapComponent("a-descriptions-item")} label=\"{item.DisplayName}\">");


            if (item.IsDictionaryMultiple)
            {
                b.Space(space + 2).AppendLine($"<a-tag color=\"\" v-for=\"(item, i) in detailData?.{FormatPropertyCase(item.PropertyCase)}|| []\" :key=\"i\">");
                b.Space(space + 2).AppendLine($" {{{{ dictStore?.findName('{item.DictionaryCode}', item) }}}} ");
                b.Space(space + 2).AppendLine($"</a-tag>");

            }
            else
            {
                if (item.IsSlot)
                {
                    b.Space(space + 2).AppendLine($"<a-tag color=\"\">");
                    b.Space(space + 2).AppendLine($" {{{{ dictStore?.findName('{item.DictionaryCode}', detailData?.{FormatPropertyCase(item.PropertyCase)}) }}}} ");
                    b.Space(space + 2).AppendLine($"</a-tag>");

                }
                else
                {
                    b.Space(space + 2).AppendLine($" {{{{ dictStore?.findName('{item.DictionaryCode}', detailData?.{FormatPropertyCase(item.PropertyCase)})  }}}} ");
                }
            }


            b.Space(space).AppendLine($"</{GetMapComponent("a-descriptions-item")}>");

            return b.ToString();
        }

        /// <summary>
        /// bool模板
        /// </summary>
        /// <returns></returns>
        public virtual string? BoolTemplate(TemplateVueEntityPropertyData item, int space = 8)
        {
            StringBuilder b = new StringBuilder();
            b.Space(space).AppendLine($"<{GetMapComponent("a-descriptions-item")} label=\"{item.DisplayName}\">");

            b.Space(space + 2).AppendLine($"<a-tag :color=\"detailData?.{FormatPropertyCase(item.PropertyCase)} ? 'green' : 'red'\">");
            b.Space(space + 2).AppendLine($" {{{{ detailData?.{FormatPropertyCase(item.PropertyCase)} ? '是' : '否' }}}} ");
            b.Space(space + 2).AppendLine($"</a-tag>");

            b.Space(space).AppendLine($"</{GetMapComponent("a-descriptions-item")}>");

            return b.ToString();
        }

        /// <summary>
        /// 图片预览模板
        /// </summary>
        /// <returns></returns>
        public virtual string? ImagePreviewTemplate(TemplateVueEntityPropertyData item, int space = 8)
        {
            var componentName = Options.ImagePreviewComponent ?? GetMapComponent("ImageUpload");

            StringBuilder b = new StringBuilder();
            b.Space(space).AppendLine($"<{GetMapComponent("a-descriptions-item")} label=\"{item.DisplayName}\">");

            b.Space(space + 2).Append($"<{componentName} {(Options.ImagePreviewComponent != null ? ":width=\"100\" :height=\"100\"" : ":disabled=\"true\"")} ");

            if (item.MultipleFile)
            {
                b.Append($" v-for=\"(item, i) in detailData?.{FormatPropertyCase(item.PropertyCase)}|| []\" :key=\"i\" :{Options.ImagePreviewComponentProp ?? "v-model"}=\"item\" ");
            }
            else
            {
                b.Append($" :{Options.ImagePreviewComponentProp ?? "v-model"}=\"detailData?.{FormatPropertyCase(item.PropertyCase)}\" ");
            }

            b.AppendLine($"></{componentName}>");

            b.Space(space).AppendLine($"</{GetMapComponent("a-descriptions-item")}>");

            return b.ToString();
        }

        /// <summary>
        /// 文件预览模板
        /// </summary>
        /// <returns></returns>
        public virtual string? FilePreviewTemplate(TemplateVueEntityPropertyData item, int space = 8)
        {
            var componentName = Options.FilePreviewComponent ?? GetMapComponent("BaseUpload");

            StringBuilder b = new StringBuilder();
            b.Space(space).AppendLine($"<{GetMapComponent("a-descriptions-item")} label=\"{item.DisplayName}\">");

            b.Space(space + 2).Append($"<{componentName} ");
            if (item.MultipleFile)
            {
                b.Append(
                    $" :{Options.FilePreviewComponentProp ?? "v-model"}=\"detailData?.{FormatPropertyCase(item.PropertyCase)}\" listType=\"text\" :disabled=\"true\" ");
            }
            else
            {
                b.Append(
                    $" :{Options.FilePreviewComponentProp ?? "v-model"}=\"[ detailData?.{FormatPropertyCase(item.PropertyCase)} ]\" listType=\"text\" :disabled=\"true\" ");

            }

            b.AppendLine($"></{componentName}>");

            b.Space(space).AppendLine($"</{GetMapComponent("a-descriptions-item")}>");

            return b.ToString();
        }


        /// <summary>
        /// 编辑器模板
        /// </summary>
        /// <returns></returns>
        public virtual string? EditorTemplate(TemplateVueEntityPropertyData item, int space = 8)
        {
            StringBuilder b = new StringBuilder();
            b.Space(space).AppendLine($"<{GetMapComponent("a-descriptions-item")} label=\"{item.DisplayName}\">");

            b.Space(space + 2).AppendLine($"<p v-html=\"detailData?.{FormatPropertyCase(item.PropertyCase)}\"></p>");

            b.Space(space).AppendLine($"</{GetMapComponent("a-descriptions-item")}>");

            return b.ToString();
        }

        /// <summary>
        /// 格式化属性名称
        /// </summary>
        /// <param name="propertyCase"></param>
        /// <returns></returns>
        protected virtual string FormatPropertyCase(string propertyCase)
        {
            return propertyCase.Replace(".", "?.");
        }
    }
}