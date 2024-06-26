using System.Collections.Generic;

namespace Rong.Volo.Abp.CodeGenerator.Vue.Models
{
    /// <summary>
    /// 新增页模型
    /// </summary>
    public class TemplateVueAddModel: TemplateVueModel
    {
        /// <summary>
        /// 表单模板
        /// </summary>
        public List<TemplateVueModelData>? Form { get; set; } = new List<TemplateVueModelData>();

        /// <summary>
        /// 表单字符串模板
        /// </summary>
        public string? FormTemplate { get; set; }
    }
}
