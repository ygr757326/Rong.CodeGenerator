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
        /// 头像 
        /// </summary>
        [Display(Name = "头像")]
        [Required(ErrorMessage = "{0}不能为空")]
        [VueFile()]
        public string Logo { get; set; }

        /// <summary>
        /// 头像1
        /// </summary>
        [Display(Name = "头像1")]
        [Required(ErrorMessage = "{0}不能为空")]
        [VueFile(true)]
        public string[] Logos { get; set; }

        /// <summary>
        /// 文章 
        /// </summary>
        [Display(Name = "文章")]
        [Required(ErrorMessage = "{0}不能为空")]
        [VueEditor()]
        public string Edit { get; set; }

        /// <summary>
        /// 文章 1
        /// </summary>
        [Display(Name = "文章1")]
        [Required(ErrorMessage = "{0}不能为空")]
        [VueEditor()]
        public string Edit1 { get; set; }
    }
}
