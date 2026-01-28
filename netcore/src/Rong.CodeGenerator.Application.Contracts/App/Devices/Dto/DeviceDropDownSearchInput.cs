using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Application.Dtos;

namespace Rong.CodeGenerator.App.Devices.Dto
{
    /// <summary>
    /// 设备管理 - 下拉查询输入
    /// </summary>
    public class DeviceDropDownSearchInput : PagedAndSortedResultRequestDto
    {        
        /// <summary>
        /// 关键字
        /// </summary>
        [Display(Name = "关键字")]
        public string? Keywords { get; set; }


        /// <summary>
        /// 验证、归一化输入值
        /// </summary>
        /// <returns></returns>
        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (string.IsNullOrWhiteSpace(Sorting))
            {
                Sorting = "SerialNo ASC";
            }

            return base.Validate(validationContext);
        }
    }
}
