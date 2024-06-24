using System.Collections.Generic;

namespace Rong.Volo.Abp.CodeGenerator.Vue.Models
{
    /// <summary>
    /// 修改页模型
    /// </summary>
    public class TemplateVueModifyModel : TemplateVueModel
    {
        /// <summary>
        /// 表单数据
        /// </summary>
        public List<TemplateVueModelData>? Form { get; set; }
    }
}
