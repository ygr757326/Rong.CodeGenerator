using System;
using System.Globalization;

namespace Rong.Volo.Abp.CodeGenerator.Vue.Attributes
{
    /// <summary>
    /// vue 编辑器特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class VueEditorAttribute : VueTableSorterAttribute
    {
        public VueEditorAttribute(bool sorter = false, short fieldSeq = short.MaxValue) : base(sorter, fieldSeq)
        {
        }
    }
}
