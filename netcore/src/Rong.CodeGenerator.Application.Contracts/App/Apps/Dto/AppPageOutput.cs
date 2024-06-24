using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Domain.Entities;

namespace Rong.CodeGenerator.App.Apps.Dto
{
    /// <summary>
    /// 分页输出
    /// </summary>
    public class AppPageOutput : AppBaseOutput, IHasConcurrencyStamp
    {
        /// <summary>
        /// 并发标记
        /// </summary>
        [Display(Name = "并发标记")]
        public string? ConcurrencyStamp { get; set; }


        public string Test => "0";
        private string Test1 => "01";
    }
}
