using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Rong.CodeGenerator.App.Devices.Dto
{
    /// <summary>
    /// 设备管理 - 修改/创建输入基类
    /// <para>该类提供需要同时进行 修改和创建 的字段</para>
    /// </summary>
    public abstract class DeviceCreateOrUpdateInputBase : IValidatableObject
    {
        /// <summary>
        /// 机具网络
        /// </summary>
        [Display(Name = "机具网络")]
        public string? Network { get; set; }

        /// <summary>
        /// 设备名称
        /// </summary>
        [Display(Name = "设备名称")]
        public string? Name { get; set; }

        /// <summary>
        /// 是否启用押金
        /// </summary>
        [Display(Name = "是否启用押金")]
        public bool IsEnableDeposit { get; set; }

        /// <summary>
        /// 押金金额
        /// </summary>
        [Display(Name = "押金金额")]
        [Column(TypeName = "decimal(18,2)")]
        [Range(typeof(decimal), "0", "999999999", ErrorMessage = "{0}值区间为{1}~{2}")]
        public decimal DepositAmount { get; set; }

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
