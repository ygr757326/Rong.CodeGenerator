using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Rong.Volo.Abp.CodeGenerator.Vue.Enums;

namespace Rong.Volo.Abp.CodeGenerator.Vue.Attributes
{
    /// <summary>
    /// vue 枚举特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class VueEnumAttribute : VueTableSorterAttribute
    {
        /// <summary>
        /// 选择模式
        /// </summary>
        public VueSelectModeEnum SelectMode { get; set; }

        /// <summary>
        /// 是否插槽
        /// </summary>
        public bool Slot { get; set; }

        /// <summary>
        /// 枚举类型
        /// </summary>
        public Type EnumType { get; set; }

        public VueEnumAttribute(Type enumType, VueSelectModeEnum selectMode = VueSelectModeEnum.Select, bool slot = false, bool sorter = true) : base(sorter)
        {
            EnumType = enumType;
            SelectMode = selectMode;
            Slot = slot;
        }
    }
}
