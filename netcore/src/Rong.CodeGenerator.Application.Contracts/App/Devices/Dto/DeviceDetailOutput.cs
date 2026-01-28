using Rong.Volo.Abp.CodeGenerator.Vue.Attributes;
using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Domain.Entities;

namespace Rong.CodeGenerator.App.Devices.Dto
{
    /// <summary>
    /// 设备管理 - 详情输出
    /// </summary>
    public class DeviceDetailOutput : DevicePageOutput, IHasConcurrencyStamp
    {
        public string ConcurrencyStamp { get; set; }

        /// <summary>
        /// 设备供应商Id
        /// </summary>
        [Display(Name = "设备供应商Id")]
        public Guid? DeviceSupplierId { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [Display(Name = "备注")]
        public string? Remark { get; set; }


    }
}
