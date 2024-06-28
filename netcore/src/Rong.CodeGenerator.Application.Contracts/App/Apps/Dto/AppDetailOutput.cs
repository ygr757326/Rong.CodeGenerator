using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using Rong.Volo.Abp.CodeGenerator.Vue.Attributes;

namespace Rong.CodeGenerator.App.Apps.Dto
{
    /// <summary>
    ///  详情输出
    /// </summary>
    public class AppDetailOutput : AppPageOutput
    {
        /// <summary>
        /// 文章 
        /// </summary>
        [Display(Name = "文章")]
        [Required(ErrorMessage = "{0}不能为空")]
        [VueEditor()]
        public string Edit { get; set; }
    }
}
