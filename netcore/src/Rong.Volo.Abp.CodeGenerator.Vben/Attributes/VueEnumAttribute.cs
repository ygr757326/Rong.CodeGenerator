using Rong.Volo.Abp.CodeGenerator.Vue.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Text;

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

        /// <summary>
        /// 是否多选
        /// </summary>
        public bool Multiple { get; set; }

        public VueEnumAttribute(Type enumType, VueSelectModeEnum selectMode = VueSelectModeEnum.Select, bool slot = false, bool multiple = false, bool sorter = true, short fieldSeq = short.MaxValue) : base(sorter, fieldSeq)
        {
            EnumType = enumType;
            SelectMode = selectMode;
            Slot = slot;
            Multiple = multiple;
        }
    }
}
