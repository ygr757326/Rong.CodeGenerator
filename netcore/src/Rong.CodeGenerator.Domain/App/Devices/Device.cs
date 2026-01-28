using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Volo.Abp.Domain.Entities.Auditing;

namespace Rong.CodeGenerator.App.Devices
{
    /// <summary>
    /// 设备
    /// </summary>
    [Table("Device")]
    [Display(Name = "设备")]
    public class Device : FullAuditedAggregateRoot<int>
    {
        /// <summary>
        /// 构造
        /// </summary>
        public Device()
        {
        }

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="id"></param>
        public Device(int id) : base(id)
        {
        }

    }
}
