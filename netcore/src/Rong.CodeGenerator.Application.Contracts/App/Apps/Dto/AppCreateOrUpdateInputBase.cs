using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Rong.Volo.Abp.CodeGenerator.Vue.Attributes;
using Rong.Volo.Abp.CodeGenerator.Vue.Enums;

namespace Rong.CodeGenerator.App.Apps.Dto
{
    /// <summary>
    /// 修改/创建输入基类
    /// <para>该类提供需要同时进行 修改和创建 的字段</para>
    /// </summary>
    public abstract class AppCreateOrUpdateInputBase : IValidatableObject
    {
        /// <summary>
        /// 文章 
        /// </summary>
        [Display(Name = "文章")]
        [Required(ErrorMessage = "{0}不能为空")]
        [VueEditor()]
        public string Edit { get; set; }

        /// <summary>
        /// 头像 
        /// </summary>
        [Display(Name = "头像")]
        [Required(ErrorMessage = "{0}不能为空")]
        [VueFile()]
        public string Logo { get; set; }

        /// <summary>
        /// 文件
        /// </summary>
        [Display(Name = "文件")]
        [Required(ErrorMessage = "{0}不能为空")]
        [VueFile(true,VueFileTypeEnum.File)]
        public string[] Files { get; set; }

        /// <summary>
        /// 客户端 
        /// </summary>
        [Display(Name = "客户端2")]
        [Required(ErrorMessage = "{0}不能为空")]
        [VueDictionary("MyDictCode", VueSelectModeEnum.Radio)]
        public string ClientDict { get; set; }

        /// <summary>
        /// 客户端3
        /// </summary>
        [Display(Name = "客户端3")]
        [Required(ErrorMessage = "{0}不能为空")]
        [VueDictionary("MyDictCode", VueSelectModeEnum.Select, slot: true)]
        public string ClientDict3 { get; set; }

        /// <summary>
        /// App版本数字号
        /// </summary>
        [Display(Name = "App版本数字号")]
        [Required(ErrorMessage = "{0}不能为空")]
        [Range(0, 999999, ErrorMessage = "{0}值区间为{1}~{2}")]
        public int VersionNumber { get; set; }

        /// <summary>
        /// 是否马上生效
        /// </summary>
        [Display(Name = "是否马上生效")]
        [Required(ErrorMessage = "{0}不能为空")]
        public bool IsNowEffect { get; set; }

        /// <summary>
        /// 开始日期
        /// </summary>
        [Display(Name = "开始日期")]
        public DateTime StartDate { get; set; }

        /// <summary>
        /// 结束日期
        /// </summary>
        [Display(Name = "结束日期")]
        public DateTime? EffectTime { get; set; }

        /// <summary>
        /// 是否强制性更新
        /// </summary>
        [Display(Name = "是否强制性更新")]
        public bool IsForceUpdate { get; set; }

        /// <summary>
        /// 金额
        /// </summary>
        [Display(Name = "金额")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }

        /// <summary>
        /// 枚举
        /// </summary>
        [Display(Name = "枚举")]
        public TestEnum MyEnum { get; set; }

        /// <summary>
        /// 枚举1
        /// </summary>
        [Display(Name = "枚举1")]
        [VueEnum(typeof(TestEnum), VueSelectModeEnum.Select)]
        public TestEnum MyEnum1 { get; set; }


        /// <summary>
        /// 枚举2
        /// </summary>
        [Display(Name = "枚举2")]
        [VueEnum(typeof(TestEnum), VueSelectModeEnum.Radio, slot: true)]
        public TestEnum MyEnum2 { get; set; }


        /// <summary>
        /// 验证、归一化输入值
        /// </summary>
        /// <returns></returns>
        public virtual IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            yield break;
        }

    }
}
