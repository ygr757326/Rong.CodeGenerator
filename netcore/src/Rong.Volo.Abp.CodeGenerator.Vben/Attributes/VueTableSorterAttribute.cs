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
    public class VueTableSorterAttribute : VueFieldSeqAttribute
    {
        /// <summary>
        /// 是否排序
        /// </summary>
        public bool Sorter { get; set; }

        public VueTableSorterAttribute(bool sorter = false, short fieldSeq = short.MaxValue) : base(fieldSeq)
        {
            Sorter = sorter;
        }
    }
}
