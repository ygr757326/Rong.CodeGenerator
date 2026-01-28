using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Application.Dtos;

namespace Rong.CodeGenerator.App.Devices.Dto
{
    /// <summary>
    /// 设备管理 - 分页查询输入
    /// </summary>
    public class DevicePageSearchInput : PagedAndSortedResultRequestDto
    {
        /// <summary>
        /// 设备政策
        /// </summary>
        [Display(Name = "设备政策")]
        public Guid? DevicePolicyId { get; set; }


        /// <summary>
        /// 设备序列号/设备标识（SN）
        /// <para>唯一标识</para>
        /// </summary>
        [Display(Name = "设备序列号")]

        public string? SerialNo { get; set; }


        /// <summary>
        /// 商户号
        /// </summary>
        [Display(Name = "商户号")]
        public string? MerchantNo { get; set; }

        /// <summary>
        /// 商户名称
        /// </summary>
        [Display(Name = "商户名称")]
        public string? MerchantName { get; set; }

        /// <summary>
        /// 是否启用押金
        /// </summary>
        [Display(Name = "是否启用押金")]
        public bool? IsEnableDeposit { get; set; }

        /// <summary>
        /// 是否已支付押金
        /// </summary>
        [Display(Name = "是否已支付押金")]
        public bool? IsPaymentDeposit { get; set; }

        /// <summary>
        /// 是否已支付押金
        /// </summary>
        [Display(Name = "是否已支付押金")]
        public bool? IsPaidDeposit { get; set; }

        /// <summary>
        /// 验证、归一化输入值
        /// </summary>
        /// <returns></returns>
        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
           
            SerialNo = SerialNo?.Trim();
            MerchantNo = MerchantNo?.Trim();
            MerchantName = MerchantName?.Trim();
            return base.Validate(validationContext);
        }

    }
}
