using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Rong.Volo.Abp.CodeGenerator.Vue.Enums;

namespace Rong.Volo.Abp.CodeGenerator.Vue.Attributes
{
    /// <summary>
    /// vue 忽略特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class VueIgnoreAttribute : Attribute
    {
        /// <summary>
        /// 是否忽略,true 则不生成
        /// </summary>
        public bool IsIgnore { get; set; }

        public VueIgnoreAttribute(bool isIgnore = true)
        {
            IsIgnore = isIgnore;
        }
    }
}
