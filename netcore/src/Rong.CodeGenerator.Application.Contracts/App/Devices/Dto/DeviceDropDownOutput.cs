using System;
using Volo.Abp;

namespace Rong.CodeGenerator.App.Devices.Dto
{
    /// <summary>
    /// 设备管理 - 下拉输出
    /// </summary>
    public class DeviceDropDownOutput : NameValue<Guid>
    {
        public Guid Id { get; set; }
        public string SerialNo { get; set; }

        public new Guid Value => Id;

        public new string Name => SerialNo;

        /// <summary>
        /// 构造
        /// </summary>
        public DeviceDropDownOutput()
        {
        }

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="value">值</param>
        public DeviceDropDownOutput(string name, Guid value) : base(name, value)
        {

        }
    }
}
