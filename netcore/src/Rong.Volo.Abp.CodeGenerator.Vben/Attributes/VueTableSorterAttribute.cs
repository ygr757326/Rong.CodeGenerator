using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Rong.Volo.Abp.CodeGenerator.Vue.Enums;

namespace Rong.Volo.Abp.CodeGenerator.Vue.Attributes
{
    /// <summary>
    /// vue table 排序特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class VueTableSorterAttribute : Attribute
    {
        /// <summary>
        /// 是否排序
        /// </summary>
        public bool Sorter { get; set; }

        /// <summary>
        /// 字段在table中的左右顺序
        /// <para>越小越在左</para>
        /// </summary>
        public short FieldSeq { get; set; }

        public VueTableSorterAttribute(bool sorter = false, short fieldSeq = short.MaxValue)
        {
            Sorter = sorter;
            FieldSeq = fieldSeq;
        }
    }
}
