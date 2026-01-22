using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Rong.Volo.Abp.CodeGenerator.Vue.Enums;

namespace Rong.Volo.Abp.CodeGenerator.Vue.Attributes
{
    /// <summary>
    /// vue 日期枚举特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class VueDateAttribute : VueTableSorterAttribute
    {
        /// <summary>
        /// 日期类型
        /// </summary>
        public VueDateTypeEnum DateType { get; set; }

        public VueDateAttribute(VueDateTypeEnum dateType = VueDateTypeEnum.Date, bool sorter = true, short fieldSeq = short.MaxValue) : base(sorter, fieldSeq)
        {
            DateType = dateType;
        }
    }
}
