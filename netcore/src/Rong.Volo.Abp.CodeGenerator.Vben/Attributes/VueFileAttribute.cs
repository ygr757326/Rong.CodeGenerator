using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Rong.Volo.Abp.CodeGenerator.Vue.Enums;

namespace Rong.Volo.Abp.CodeGenerator.Vue.Attributes
{
    /// <summary>
    /// vue 文件枚举特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class VueFileAttribute : VueTableSorterAttribute
    {
        /// <summary>
        /// 是否多文件
        /// </summary>
        public bool Multiple { get; set; }

        /// <summary>
        /// 文件类型
        /// </summary>
        public VueFileTypeEnum FileType { get; set; }

        public VueFileAttribute(bool multiple = false, VueFileTypeEnum fileType = VueFileTypeEnum.Image, bool sorter = false, short fieldSeq = short.MaxValue) : base(sorter, fieldSeq)
        {
            Multiple = multiple;
            FileType = fileType;
        }
    }
}
