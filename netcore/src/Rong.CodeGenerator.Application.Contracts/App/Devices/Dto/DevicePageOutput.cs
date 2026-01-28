using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Rong.CodeGenerator.App.Devices.Dto
{
    /// <summary>
    /// 设备管理 - 分页输出
    /// </summary>
    public class DevicePageOutput : DeviceBaseOutput
    {

        /// <summary>
        /// 是否启用押金
        /// </summary>
        [Display(Name = "是否启用押金")]
        public bool IsEnableDeposit { get; set; }

        /// <summary>
        /// 押金金额
        /// <para>码牌才会有押金</para>
        /// </summary>
        [Display(Name = "押金金额")]
        [Column(TypeName = "decimal(18,2)")]
        [Range(typeof(decimal), "0", "999999999", ErrorMessage = "{0}值区间为{1}~{2}")]
        public decimal DepositAmount { get; set; }

        /// <summary>
        /// 是否已支付押金
        /// </summary>
        [Display(Name = "是否已支付押金")]
        public bool IsPaidDeposit { get; set; }

        /// <summary>
        /// 支付时间
        /// </summary>
        [Display(Name = "支付时间")]
        public DateTime? PaymentDate { get; set; }

        /// <summary>
        /// 是否已绑定
        /// </summary>
        [Display(Name = "是否已绑定")]
        public bool IsBind => PaymentDate != null;

        /// <summary>
        /// 是否已激活
        /// </summary>
        [Display(Name = "是否已激活")]
        public bool IsActivated => PaymentDate != null;
    }
}
