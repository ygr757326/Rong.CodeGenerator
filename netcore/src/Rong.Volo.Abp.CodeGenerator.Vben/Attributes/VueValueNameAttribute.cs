using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Rong.Volo.Abp.CodeGenerator.Vue.Enums;

namespace Rong.Volo.Abp.CodeGenerator.Vue.Attributes
{
    /// <summary>
    /// vue 值名称数据特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class VueValueNameAttribute : VueTableSorterAttribute
    {
        /// <summary>
        /// 点拼接名称
        /// </summary>
        public string PointSplicingName { get; set; }

        public VueValueNameAttribute(string pointSplicingName, bool sorter = false) : base(sorter)
        {
            PointSplicingName = pointSplicingName;
        }
    }
}
