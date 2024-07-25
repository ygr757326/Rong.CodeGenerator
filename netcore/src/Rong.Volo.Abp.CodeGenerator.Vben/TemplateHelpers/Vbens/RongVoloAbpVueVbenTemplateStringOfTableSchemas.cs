using Microsoft.Extensions.Options;
using Rong.Volo.Abp.CodeGenerator.Vue.Models;
using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using Volo.Abp.DependencyInjection;

namespace Rong.Volo.Abp.CodeGenerator.Vue.TemplateHelpers.Vbens
{
    /// <summary>
    /// vben模板 -  table查询表单
    /// </summary>
    public class RongVoloAbpVueVbenTemplateStringOfTableSchemas : RongVoloAbpVueTemplateBase, ISingletonDependency
    {

        public RongVoloAbpVueVbenTemplateStringOfTableSchemas(IOptions<RongVoloAbpCodeGeneratorVueOptions> options) : base(options)
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

            b.Space(space + 2).AppendLine($"component: '{Options.EnumSelectComponent ?? GetMapComponent("Select")}',");

            if (Options.EnumSelectComponentProp != null)
            {
                b.Space(space + 2).AppendLine($"componentProps: {{");
                b.Space(space + 4).AppendLine($"{Options.EnumSelectComponentProp}: '{item.PropertyType.Name}',");
                b.Space(space + 4).AppendLine($"showSearch: true,");
                b.Space(space + 4).AppendLine($"allowClear: true,");
                b.Space(space + 2).AppendLine($"}},");
            }
            else
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
            b.Space(space + 2).AppendLine($"component: '{Options.DictionarySelectComponent ?? GetMapComponent("Select")}',");

            if (Options.DictionarySelectComponentProp != null)
            {
                b.Space(space + 2).AppendLine($"componentProps: {{");
                b.Space(space + 4).AppendLine($"{Options.DictionarySelectComponentProp}: '{item.DictionaryCode}',");
                b.Space(space + 4).AppendLine($"showSearch: true,");
                b.Space(space + 4).AppendLine($"allowClear: true,");
                b.Space(space + 2).AppendLine($"}},");
            }
            else
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
            b.Space(space + 2).AppendLine($"component: '{GetMapComponent("Select")}',");
            b.Space(space + 2).AppendLine($"componentProps: {{");
            b.Space(space + 4).AppendLine($"options: [");
            b.Space(space + 6).AppendLine($"{{label: '是', value: true }},");
            b.Space(space + 6).AppendLine($"{{label: '否', value: false }},");
            b.Space(space + 4).AppendLine($"],");
            b.Space(space + 2).AppendLine($"}},");


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
        /// 使用组件模板
        /// </summary>
        /// <returns></returns>
        public virtual string? ComponentTemplate(TemplateVueEntityPropertyData item, int space = 6)
        {
            StringBuilder b = new StringBuilder();

            b.Space(space).AppendLine("{");

            b.Space(space + 2).AppendLine($"label: '{item.DisplayName}',");
            b.Space(space + 2).AppendLine($"field: '{item.PropertyCase}',");
            b.Space(space + 2).AppendLine($"component: '{GetMapComponent(item.Component)}',");

            if (item.IsRequired)
            {
                b.Space(space + 2).AppendLine($"required: true,");
            }

            b.Space(space).AppendLine("},");

            return b.ToString();
        }
    }
}