using System;
using System.ComponentModel.DataAnnotations;
using Rong.Volo.Abp.CodeGenerator.Vue.Enums;

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
        public string PropertyCase { get; internal set; }

        /// <summary>
        /// 是否必须
        /// </summary>
        public bool IsRequired { get; set; }

        /// <summary>
        /// 属性值类型
        /// </summary>
        public Type PropertyType { get; set; }

        /// <summary>
        /// 是否是字典
        /// </summary>
        public bool IsDictionary { get; set; }

        /// <summary>
        /// 是否是Slot
        /// </summary>
        public bool IsSlot { get; set; }

        /// <summary>
        /// 字典编码
        /// </summary>
        public string? DictionaryCode { get; set; }

        /// <summary>
        /// 是否是枚举
        /// </summary>
        public bool IsEnum { get; set; }

        /// <summary>
        /// 选择模式
        /// </summary>
        public VueSelectModeEnum? SelectMode { get; set; }

        /// <summary>
        /// 表格是否排序
        /// </summary>
        public bool TableSorter { get; set; }

        /// <summary>
        /// 是否是文件
        /// </summary>
        public bool IsFile { get; set; }

        /// <summary>
        /// 是否是多文件
        /// </summary>
        public bool MultipleFile { get; set; }

    }
}
