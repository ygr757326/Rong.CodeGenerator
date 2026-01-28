using Rong.Volo.Abp.CodeGenerator.Vue.Attributes;
using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Application.Dtos;

namespace Rong.CodeGenerator.App.Devices.Dto
{
    /// <summary>
    /// 设备管理 - 基本信息输出
    /// </summary>
    public class DeviceBaseOutput : EntityDto<Guid>
    {
        /// <summary>
        /// 设备名称
        /// </summary>
        [Display(Name = "设备名称")]
        public string? Name { get; set; }

        /// <summary>
        /// 设备序列号/设备标识（SN）
        /// <para>唯一：所有业务类型下都唯一</para>
        /// </summary>
        [Display(Name = "设备序列号1")]
        [Required(ErrorMessage = "{0}不能为空")]
        public string SerialNo { get; set; }

        /// <summary>
        /// 设备政策
        /// </summary>
        [Display(Name = "设备政策")]
        [Required(ErrorMessage = "{0}不能为空")]
        public Guid DevicePolicyId { get; set; }
    }
}
