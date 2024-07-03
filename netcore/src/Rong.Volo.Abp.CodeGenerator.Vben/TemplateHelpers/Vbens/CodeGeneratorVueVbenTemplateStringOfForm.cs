using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.CodeAnalysis;
using Microsoft.Extensions.Options;
using Rong.Volo.Abp.CodeGenerator.Vue.Enums;
using Rong.Volo.Abp.CodeGenerator.Vue.Models;
using Volo.Abp.DependencyInjection;

namespace Rong.Volo.Abp.CodeGenerator.Vue.TemplateHelpers.Vbens
{
    /// <summary>
    /// vben模板 -  新增编辑表单
    /// </summary>
    public class CodeGeneratorVueVbenTemplateStringOfForm : CodeGeneratorVueTemplateBase, ISingletonDependency
    {

        public CodeGeneratorVueVbenTemplateStringOfForm(IOptions<CodeGeneratorVueOptions> options) : base(options)
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

            b.Space(space + 2).AppendLine($"label: '{item.DisplayName}',");
            b.Space(space + 2).AppendLine($"field: '{item.PropertyCase}',");
            b.Space(space + 2).AppendLine($"component: '{GetMapComponent("Input")}',");
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

            b.Space(space + 2).AppendLine($"label: '{item.DisplayName}',");
            b.Space(space + 2).AppendLine($"field: '{item.PropertyCase}',");
            b.Space(space + 2).AppendLine($"component: '{GetMapComponent("DatePicker")}',");
            b.Space(space + 2).AppendLine($"componentProps: {{");
            b.Space(space + 4).AppendLine($"valueFormat: 'YYYY-MM-DD',");//YYYY-MM-DD HH:mm:ss
            b.Space(space + 4).AppendLine($"allowClear: true,");
            b.Space(space + 2).AppendLine($"}},");

            if (item.IsRequired)
            {
                b.Space(space + 2).AppendLine($"required: true,");
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

            b.Space(space + 2).AppendLine($"label: '{item.DisplayName}',");
            b.Space(space + 2).AppendLine($"field: '{item.PropertyCase}',");

            bool defaultComponentProps = true;
            if (item.SelectMode == VueSelectModeEnum.Radio)
            {
                b.Space(space + 2).AppendLine($"component: '{Options.EnumRadioComponent ?? GetMapComponent("RadioGroup")}',");

                if (Options.EnumRadioComponentProp != null)
                {
                    defaultComponentProps = false;

                    b.Space(space + 2).AppendLine($"componentProps: {{");
                    b.Space(space + 4).AppendLine($"{Options.EnumRadioComponentProp}: '{item.PropertyType.Name}',");
                    b.Space(space + 4).AppendLine($"showSearch: true,");
                    b.Space(space + 4).AppendLine($"allowClear: true,");
                    b.Space(space + 2).AppendLine($"}},");
                }
            }
            else
            {
                b.Space(space + 2).AppendLine($"component: '{Options.EnumSelectComponent ?? GetMapComponent("Select")}',");

                if (Options.EnumSelectComponentProp != null)
                {
                    defaultComponentProps = false;

                    b.Space(space + 2).AppendLine($"componentProps: {{");
                    b.Space(space + 4).AppendLine($"{Options.EnumSelectComponentProp}: '{item.PropertyType.Name}',");
                    b.Space(space + 4).AppendLine($"showSearch: true,");
                    b.Space(space + 4).AppendLine($"allowClear: true,");
                    b.Space(space + 2).AppendLine($"}},");
                }
            }

            if (defaultComponentProps)
            {
                b.Space(space + 2).AppendLine($"componentProps: {{");
                b.Space(space + 4).AppendLine($"options: enumStore?.findCodeSelect('{item.PropertyType.Name}'),");
                b.Space(space + 4).AppendLine($"showSearch: true,");
                b.Space(space + 4).AppendLine($"allowClear: true,");
                b.Space(space + 4).AppendLine($"optionFilterProp: 'label',");
                b.Space(space + 2).AppendLine($"}},");
            }

            if (item.IsRequired)
            {
                b.Space(space + 2).AppendLine($"required: true,");
            }

            b.Space(space).AppendLine("},");

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

            b.Space(space + 2).AppendLine($"label: '{item.DisplayName}',");
            b.Space(space + 2).AppendLine($"field: '{item.PropertyCase}',");

            bool defaultComponentProps = true;
            if (item.SelectMode == VueSelectModeEnum.Radio)
            {
                b.Space(space + 2).AppendLine($"component: '{Options.DictionaryRadioComponent ?? GetMapComponent("RadioGroup")}',");

                if (Options.DictionaryRadioComponentProp != null)
                {
                    defaultComponentProps = false;

                    b.Space(space + 2).AppendLine($"componentProps: {{");
                    b.Space(space + 4).AppendLine($"{Options.DictionaryRadioComponentProp}: '{item.DictionaryCode}',");
                    b.Space(space + 4).AppendLine($"showSearch: true,");
                    b.Space(space + 4).AppendLine($"allowClear: true,");
                    b.Space(space + 2).AppendLine($"}},");
                }
            }
            else
            {
                b.Space(space + 2).AppendLine($"component: '{Options.DictionarySelectComponent ?? GetMapComponent("Select")}',");

                if (Options.DictionarySelectComponentProp != null)
                {
                    defaultComponentProps = false;

                    b.Space(space + 2).AppendLine($"componentProps: {{");
                    b.Space(space + 4).AppendLine($"{Options.DictionarySelectComponentProp}: '{item.DictionaryCode}',");
                    b.Space(space + 4).AppendLine($"showSearch: true,");
                    b.Space(space + 4).AppendLine($"allowClear: true,");
                    b.Space(space + 2).AppendLine($"}},");
                }
            }

            if (defaultComponentProps)
            {
                b.Space(space + 2).AppendLine($"componentProps: {{");
                b.Space(space + 4).AppendLine($"options: dictStore?.findCodeSelect('{item.DictionaryCode}'),");
                b.Space(space + 4).AppendLine($"showSearch: true,");
                b.Space(space + 4).AppendLine($"allowClear: true,");
                b.Space(space + 4).AppendLine($"optionFilterProp: 'label',");
                b.Space(space + 2).AppendLine($"}},");
            }

            if (item.IsRequired)
            {
                b.Space(space + 2).AppendLine($"required: true,");
            }

            b.Space(space).AppendLine("},");

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

            b.Space(space + 2).AppendLine($"label: '{item.DisplayName}',");
            b.Space(space + 2).AppendLine($"field: '{item.PropertyCase}',");

            if (item.SelectMode == VueSelectModeEnum.Radio)
            {
                b.Space(space + 2).AppendLine($"component:'{Options.BoolRadioComponent ?? GetMapComponent("Switch")}',");
            }
            else if (item.SelectMode == VueSelectModeEnum.Select)
            {
                b.Space(space + 2).AppendLine($"component:'{Options.BoolSelectComponent ?? GetMapComponent("Switch")}',");
            }
            else
            {
                b.Space(space + 2).AppendLine($"component:'{GetMapComponent("Switch")}',");
            }

            var defaultValue = item.PropertyInfo.GetCustomAttribute<DefaultValueAttribute>();
            if (defaultValue?.Value != null)
            {
                b.Space(space + 2).AppendLine($"defaultValue: '{defaultValue.Value.ToString()?.ToCamelCase()}',");
            }

            if (item.IsRequired)
            {
                b.Space(space + 2).AppendLine($"required: true,");
            }

            b.Space(space).AppendLine("},");

            return b.ToString();
        }

        /// <summary>
        /// 数值模板
        /// </summary>
        /// <returns></returns>
        public virtual string? NumberTemplate(TemplateVueEntityPropertyData item, int space = 6)
        {
            var typeCode = item.PropertyType.GetMyTypeCode();

            StringBuilder b = new StringBuilder();

            b.Space(space).AppendLine("{");

            b.Space(space + 2).AppendLine($"label: '{item.DisplayName}',");
            b.Space(space + 2).AppendLine($"field: '{item.PropertyCase}',");

            if (new[] { TypeCode.Double }.Contains(typeCode))
            {
                b.Space(space + 2).AppendLine($"component: '{GetMapComponent("InputNumber")}',");
                b.Space(space + 2).AppendLine($"componentProps: {{ precision: 2 }},");
            }
            else if (new[] { TypeCode.Single }.Contains(typeCode))
            {
                b.Space(space + 2).AppendLine($"component: '{GetMapComponent("InputNumber")}',");
                b.Space(space + 2).AppendLine($"componentProps: {{ precision: 1 }},");
            }
            else if (new[] { TypeCode.Decimal }.Contains(typeCode))
            {
                int length = item.PropertyInfo.GetDecimalPlaceFromColumnAttribute();
                b.Space(space + 2).AppendLine($"component:'{GetMapComponent("InputNumber")}',");
                b.Space(space + 2).AppendLine($"componentProps: {{ precision: {length} }},");
            }
            else
            {
                b.Space(space + 2).AppendLine($"component: 'InputNumber',");
                b.Space(space + 2).AppendLine($"componentProps: {{ precision: 0 }},");
            }

            if (item.IsRequired)
            {
                b.Space(space + 2).AppendLine($"required: true,");
            }

            b.Space(space).AppendLine("},");

            return b.ToString();
        }

        /// <summary>
        /// 文件上传模板
        /// </summary>
        /// <returns></returns>
        public virtual string? FileUploadTemplate(TemplateVueEntityPropertyData item, int space = 6)
        {
            StringBuilder b = new StringBuilder();

            b.Space(space).AppendLine("{");

            b.Space(space + 2).AppendLine($"label: '{item.DisplayName}',");
            b.Space(space + 2).AppendLine($"field: '{item.PropertyCase}',");
            b.Space(space + 2).AppendLine($"component: '{Options.FileUploadComponent ?? GetMapComponent("BaseUpload")}',");
            b.Space(space + 2).AppendLine($"componentProps: {{");
            b.Space(space + 4).AppendLine($"multiple: {item.MultipleFile.ToString().ToCamelCase()},");
            b.Space(space + 4).AppendLine($"listType: 'text',");
            b.Space(space + 2).AppendLine($"}},");

            if (item.IsRequired)
            {
                b.Space(space + 2).AppendLine($"required: true,");
            }

            b.Space(space).AppendLine("},");

            return b.ToString();
        }

        /// <summary>
        /// 图片上传模板
        /// </summary>
        /// <returns></returns>
        public virtual string? ImageUploadTemplate(TemplateVueEntityPropertyData item, int space = 6)
        {
            StringBuilder b = new StringBuilder();

            b.Space(space).AppendLine("{");

            b.Space(space + 2).AppendLine($"label: '{item.DisplayName}',");
            b.Space(space + 2).AppendLine($"field: '{item.PropertyCase}',");
            b.Space(space + 2).AppendLine($"component: '{Options.ImageUploadComponent ?? GetMapComponent("ImageUpload")}',");
            b.Space(space + 2).AppendLine($"componentProps: {{");
            b.Space(space + 4).AppendLine($"multiple: {item.MultipleFile.ToString().ToCamelCase()},");
            b.Space(space + 4).AppendLine($"listType: 'picture-card',");
            b.Space(space + 2).AppendLine($"}},");

            if (item.IsRequired)
            {
                b.Space(space + 2).AppendLine($"required: true,");
            }

            b.Space(space).AppendLine("},");

            return b.ToString();
        }

        /// <summary>
        /// 内容输入框模板
        /// </summary>
        /// <returns></returns>
        public virtual string? TextareaTemplate(TemplateVueEntityPropertyData item, int space = 6)
        {
            StringBuilder b = new StringBuilder();

            b.Space(space).AppendLine("{");

            b.Space(space + 2).AppendLine($"label: '{item.DisplayName}',");
            b.Space(space + 2).AppendLine($"field: '{item.PropertyCase}',");
            b.Space(space + 2).AppendLine($"component: '{GetMapComponent("InputTextArea")}',");
            //b.Space(space + 2).AppendLine($"componentProps: {{");
            //b.Space(space + 4).AppendLine($"multiple: {item.MultipleFile.ToString().ToCamelCase()},");
            //b.Space(space + 4).AppendLine($"listType: 'picture-card',");
            //b.Space(space + 2).AppendLine($"}},");
            if (item.IsRequired)
            {
                b.Space(space + 2).AppendLine($"required: true,");
            }

            b.Space(space).AppendLine("},");

            return b.ToString();
        }

        /// <summary>
        /// 编辑器模板
        /// </summary>
        /// <returns></returns>
        public virtual string? EditorTemplate(TemplateVueEntityPropertyData item, int space = 6)
        {
            StringBuilder b = new StringBuilder();

            b.Space(space).AppendLine("{");

            b.Space(space + 2).AppendLine($"label: '{item.DisplayName}',");
            b.Space(space + 2).AppendLine($"field: '{item.PropertyCase}',");
            b.Space(space + 2).AppendLine($"component: '{Options.EditorComponent ?? GetMapComponent("Tinymce")}',");

            if (item.IsRequired)
            {
                b.Space(space + 2).AppendLine($"required: true,");
            }

            b.Space(space).AppendLine("},");

            return b.ToString();
        }
    }
}