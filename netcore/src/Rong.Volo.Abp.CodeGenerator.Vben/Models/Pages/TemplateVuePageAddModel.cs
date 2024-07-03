using System.Collections.Generic;

namespace Rong.Volo.Abp.CodeGenerator.Vue.Models.Pages
{
    /// <summary>
    /// 新增页模型
    /// </summary>
    public class TemplateVuePageAddModel : TemplateVueEntityModelBase
    {
        /// <summary>
        /// 表单模板
        /// </summary>
        public List<TemplateVueEntityPropertyData>? Form { get; set; } = new List<TemplateVueEntityPropertyData>();

        /// <summary>
        /// 表单字符串模板
        /// </summary>
        public string? FormTemplate { get; set; }
    }
}
