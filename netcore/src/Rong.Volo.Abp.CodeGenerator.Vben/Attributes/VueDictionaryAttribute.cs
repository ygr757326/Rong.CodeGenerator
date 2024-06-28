using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Rong.Volo.Abp.CodeGenerator.Vue.Enums;

namespace Rong.Volo.Abp.CodeGenerator.Vue.Attributes
{
    /// <summary>
    /// vue 字典特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class VueDictionaryAttribute : VueTableSorterAttribute
    {
        /// <summary>
        /// 选择模式
        /// </summary>
        public VueSelectModeEnum SelectMode { get; set; }

        /// <summary>
        /// 是否插槽
        /// </summary>
        public bool  Slot { get; set; }

        /// <summary>
        /// 字典编码
        /// </summary>
        public string Code { get; set; }

        public VueDictionaryAttribute(string code, VueSelectModeEnum selectMode = VueSelectModeEnum.Select, bool slot=false, bool sorter = true) : base(sorter)
        {
            Code = code;
            SelectMode = selectMode;
            Slot = slot;
        }
    }
}
