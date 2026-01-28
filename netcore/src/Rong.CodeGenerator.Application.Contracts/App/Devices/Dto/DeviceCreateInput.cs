using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Rong.CodeGenerator.App.Devices.Dto
{
    /// <summary>
    /// 设备管理 - 创建输入
    /// </summary>
    public class DeviceCreateInput : DeviceCreateOrUpdateInputBase
    {
        /// <summary>
        /// 设备序列号/设备标识（SN）
        /// <para>唯一标识</para>
        /// </summary>
        [Display(Name = "设备序列号")]
        [Required(ErrorMessage = "{0}不能为空")]
        public string SerialNo { get; set; }

        /// <summary>
        /// 验证、归一化输入值
        /// </summary>
        /// <returns></returns>
        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            yield break;
        }
    }
}
