using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Rong.Volo.Abp.CodeGenerator.Vue.Enums;

namespace Rong.Volo.Abp.CodeGenerator.Vue.Attributes
{
    /// <summary>
    /// vue table 字段顺序特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class VueFieldSeqAttribute : Attribute
    {
        /// <summary>
        /// 字段在table中的左右顺序
        /// <para>越小越在左</para>
        /// </summary>
        public short FieldSeq { get; set; }

        public VueFieldSeqAttribute(short fieldSeq)
        {
            FieldSeq = fieldSeq;
        }
    }
}
