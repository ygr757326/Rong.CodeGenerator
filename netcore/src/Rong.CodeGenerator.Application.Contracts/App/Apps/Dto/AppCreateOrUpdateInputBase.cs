using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Rong.CodeGenerator.App.Apps.Dto
{
    /// <summary>
    /// 修改/创建输入基类
    /// <para>该类提供需要同时进行 修改和创建 的字段</para>
    /// </summary>
    public abstract class AppCreateOrUpdateInputBase : IValidatableObject
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
        /// <para>年月日时分秒</para>
        /// </summary>
        [Display(Name = "指定生效时间")]
        public DateTime? EffectTime { get; set; }

        /// <summary>
        /// 是否强制性更新
        /// </summary>
        public bool IsForceUpdate { get; set; }

        /// <summary>
        /// 验证、归一化输入值
        /// </summary>
        /// <returns></returns>
        public virtual IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (!IsNowEffect && !EffectTime.HasValue)
            {
                yield return new ValidationResult("生效时间不能为空");
            }
            yield break;
        }

    }
}
