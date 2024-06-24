using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Application.Dtos;

namespace Rong.CodeGenerator.App.Apps.Dto
{
    /// <summary>
    /// 分页查询输入
    /// </summary>
    public class AppPageSearchInput : PagedAndSortedResultRequestDto
    {
        /// <summary>
        /// 版本号
        /// </summary>
        [Display(Name = "版本号")]
        public string? Version1 { get; }


        /// <summary>
        /// 版本号
        /// </summary>
        [Display(Name = "版本号")]
        public string? Version { get; set; }

        /// <summary>
        /// App版本数字号
        /// </summary>
        [Display(Name = "App版本数字号")]
        [Range(0, 999999, ErrorMessage = "{0}值区间为{1}~{2}")]
        public int? VersionNumber { get; set; }

        /// <summary>
        /// 是否马上生效
        /// </summary>
        [Display(Name = "是否马上生效")]
        public bool? IsNowEffect { get; set; }

        /// <summary>
        /// 指定生效时间
        /// </summary>
        [Display(Name = "指定生效时间")]
        public DateTime? EffectTimeStart { get; set; }

        /// <summary>
        /// 验证、归一化输入值
        /// </summary>
        /// <returns></returns>
        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            return base.Validate(validationContext);
        }

    }
}
