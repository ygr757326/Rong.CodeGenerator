using System;
using System.ComponentModel.DataAnnotations;

namespace Rong.Volo.Abp.CodeGenerator.Vue.Models
{
    /// <summary>
    /// 模型数据
    /// </summary>
    public class TemplateVueModelData
    {
        /// <summary>
        /// 显示名称
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// 原属性
        /// </summary>
        public string Property { get; set; }

        /// <summary>
        /// 属性小写
        /// </summary>
        public string PropertyCase => Property.IsNullOrWhiteSpace() ? null : $"{char.ToLowerInvariant(Property[0]) + Property.Substring(1)}";

        /// <summary>
        /// 是否必须
        /// </summary>
        public bool IsRequired { get; set; }

        /// <summary>
        /// 属性值类型
        /// </summary>
        public Type? PropertyType { get; set; }
    }
}
