using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace Rong.CodeGenerator.App.Devices.Dto
{
    /// <summary>
    /// 设备管理 - 修改输入
    /// </summary>
    public class DeviceUpdateInput : DeviceCreateOrUpdateInputBase, IEntityDto<Guid>, IHasConcurrencyStamp
    {
        /// <summary>
        /// 并发标记
        /// </summary>
        [Display(Name = "并发标记")]
        public string? ConcurrencyStamp { get; set; }

        /// <summary>
        /// id
        /// </summary>
        [Display(Name = "Id")]
        [Required(ErrorMessage = "{0}不能为空")]
        public virtual Guid Id { get; set; }

        /// <summary>
        /// 设备政策
        /// </summary>
        [Display(Name = "设备政策")]
        [Required(ErrorMessage = "{0}不能为空")]
        public Guid DevicePolicyId { get; set; }

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
