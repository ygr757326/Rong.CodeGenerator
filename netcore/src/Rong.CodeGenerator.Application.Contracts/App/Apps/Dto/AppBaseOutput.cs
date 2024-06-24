using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Volo.Abp.Application.Dtos;

namespace Rong.CodeGenerator.App.Apps.Dto
{
    /// <summary>
    /// 基本信息输出
    /// </summary>
    public class AppBaseOutput : EntityDto<Guid>
    {
        /// <summary>
        /// 客户端 
        /// </summary>
        [Display(Name = "客户端")]
        [Required(ErrorMessage = "{0}不能为空")]
        public string Client { get; set; }

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
        /// 指定生效时间
        /// </summary>
        [Display(Name = "指定生效时间")]
        public DateTime? EffectTime { get; set; }

        /// <summary>
        /// 是否强制性更新
        /// </summary>
        public bool IsForceUpdate { get; set; }

        /// <summary>
        /// 金额
        /// </summary>
        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }

    }
}
