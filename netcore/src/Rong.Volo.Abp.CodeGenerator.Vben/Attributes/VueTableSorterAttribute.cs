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
    public class VueTableSorterAttribute : Attribute
    {
        /// <summary>
        /// 是否排序
        /// </summary>
        public bool Sorter { get; set; }

        public VueTableSorterAttribute(bool sorter=false)
        {
            Sorter = sorter;
        }
    }
}
