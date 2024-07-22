using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Rong.CodeGenerator.Enums
{
    /// <summary>
    /// 字典种类枚举
    /// <para>本系统所有字典种类，都必须在此处添加</para>
    /// </summary>
    [Description("字典种类枚举")]
    public enum DictionaryTypeEnum
    {
        /// <summary>
        /// 学历
        /// </summary>
        [Display(Name = "学历")]
        Education,

        /// <summary>
        /// 婚姻状态
        /// </summary>
        [Display(Name = "婚姻状态")]
        MaritalStatus,

        /// <summary>
        /// 民族
        /// </summary>
        [Display(Name = "民族")]
        Nation,
    }
}
