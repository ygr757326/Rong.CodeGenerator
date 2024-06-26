using System.ComponentModel.DataAnnotations.Schema;
using Volo.Abp.Domain.Entities.Auditing;

namespace Rong.CodeGenerator.App.Apps
{
    /// <summary>
    /// App应用
    /// </summary>
    [Table("App应用")]
    public class App : FullAuditedAggregateRoot<int>
    {
        /// <summary>
        /// 构造
        /// </summary>
        public App()
        {
        }

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="id"></param>
        public App(int id) : base(id)
        {
        }

    }
}
