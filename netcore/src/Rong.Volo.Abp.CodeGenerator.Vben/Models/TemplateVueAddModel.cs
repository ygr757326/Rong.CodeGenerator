using System.Collections.Generic;

namespace Rong.Volo.Abp.CodeGenerator.Vue.Models
{
    /// <summary>
    /// 新增页模型
    /// </summary>
    public class TemplateVueAddModel: TemplateVueModel
    {
        /// <summary>
        /// 表单数据
        /// </summary>
        public List<TemplateVueModelData>? Form { get; set; }

    }
}
