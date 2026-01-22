using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Rong.Volo.Abp.CodeGenerator.Vue.Enums;

namespace Rong.Volo.Abp.CodeGenerator.Vue.Attributes
{
    /// <summary>
    /// vue 组件数据特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class VueComponentAttribute : Attribute//VueFieldSeqAttribute
    {
        /// <summary>
        /// 组件
        /// </summary>
        public string Component { get; set; }

        public VueComponentAttribute(string component)// : base(fieldSeq)
        {
            Component = component;
        }
    }
}
