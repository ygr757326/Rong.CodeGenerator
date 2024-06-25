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
    public class VueDictionaryAttribute : VueSorterAttribute
    {
        /// <summary>
        /// 选择模式
        /// </summary>
        public VueSelectModeEnum SelectMode { get; set; }

        /// <summary>
        /// 字典编码
        /// </summary>
        public string Code { get; set; }

        public VueDictionaryAttribute(string code, VueSelectModeEnum selectMode = VueSelectModeEnum.Select, bool sorter = true) : base(sorter)
        {
            Code = code;
            SelectMode = selectMode;
        }
    }
}
