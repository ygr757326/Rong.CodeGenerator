using System.Collections.Generic;

namespace Rong.Volo.Abp.CodeGenerator.Vue.Models.Pages
{
    /// <summary>
    /// 修改页模型
    /// </summary>
    public class TemplateVuePageModifyModel : TemplateVueEntityModelBase
    {
        /// <summary>
        /// 编辑表单模板
        /// </summary>
        public List<TemplateVueEntityPropertyData>? Form { get; set; } = new List<TemplateVueEntityPropertyData>();

        /// <summary>
        /// 编辑表单字符串模板
        /// </summary>
        public string? FormTemplate { get; set; }
    }
}
