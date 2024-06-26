using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Rong.Volo.Abp.CodeGenerator.Vue.Attributes;
using Rong.Volo.Abp.CodeGenerator.Vue.Enums;
using Volo.Abp.Application.Dtos;

namespace Rong.CodeGenerator.App.Apps.Dto
{
    /// <summary>
    /// 基本信息输出
    /// </summary>
    public class AppBaseOutput : EntityDto<Guid>
    {
        /// <summary>
        /// 头像 
        /// </summary>
        [Display(Name = "头像")]
        [Required(ErrorMessage = "{0}不能为空")]
        [VueFile()]
        public string Logo { get; set; }

        /// <summary>
        /// 头 1
        /// </summary>
        [Display(Name = "头像1")]
        [Required(ErrorMessage = "{0}不能为空")]
        [VueFile(true)]
        public string[] Logos { get; set; }

        /// <summary>
        /// 客户端 
        /// </summary>
        [Display(Name = "客户端")]
        [Required(ErrorMessage = "{0}不能为空")]
        public string Client { get; set; }

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
        /// App版本数字号
        /// </summary>
        [Display(Name = "App版本数字号")]
        [Required(ErrorMessage = "{0}不能为空")]
        [Range(0, 999999, ErrorMessage = "{0}值区间为{1}~{2}")]
        public int? VersionNumber1 { get; set; }

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

    }
}
