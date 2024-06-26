using System.Collections.Generic;

namespace Rong.Volo.Abp.CodeGenerator.Vue.Models
{
    /// <summary>
    /// 修改页模型
    /// </summary>
    public class TemplateVueModifyModel : TemplateVueModel
    {
        /// <summary>
        /// 编辑表单模板
        /// </summary>
        public List<TemplateVueModelData>? Form { get; set; } = new List<TemplateVueModelData>();

        /// <summary>
        /// 编辑表单字符串模板
        /// </summary>
        public string? FormTemplate { get; set; }
    }
}
