using Rong.Volo.Abp.CodeGenerator.Vue.Enums;
using System;

namespace Rong.Volo.Abp.CodeGenerator.Vue.Attributes
{
    /// <summary>
    /// vue bool特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class VueBoolAttribute : VueTableSorterAttribute
    {
        /// <summary>
        /// 选择模式
        /// </summary>
        public VueSelectModeEnum SelectMode { get; set; }

        /// <summary>
        /// 是否插槽
        /// </summary>
        public bool Slot { get; set; }

        public VueBoolAttribute(VueSelectModeEnum selectMode = VueSelectModeEnum.Switch, bool slot = true, bool sorter = true) : base(sorter)
        {
            SelectMode = selectMode;
            Slot = slot;
        }
    }
}
