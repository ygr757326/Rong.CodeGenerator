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
    public class VueEnumAttribute : VueSorterAttribute
    {
        /// <summary>
        /// 选择模式
        /// </summary>
        public VueSelectModeEnum SelectMode { get; set; }

        /// <summary>
        /// 枚举类型
        /// </summary>
        public Type EnumType { get; set; }

        public VueEnumAttribute(Type enumType, VueSelectModeEnum selectMode = VueSelectModeEnum.Select, bool sorter = true) : base(sorter)
        {
            EnumType = enumType;
            SelectMode = selectMode;
        }
    }
}
