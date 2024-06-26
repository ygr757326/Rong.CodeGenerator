using System.Collections.Generic;

namespace Rong.Volo.Abp.CodeGenerator.Vue.Models
{
    /// <summary>
    /// 详情页模型
    /// </summary>
    public class TemplateVueDetailModel: TemplateVueModel
    {
        /// <summary>
        /// 详情模板
        /// </summary>
        public List<TemplateVueModelData>? Detail { get; set; } = new List<TemplateVueModelData>();

        /// <summary>
        /// 详情字符串模板
        /// </summary>
        public string? DetailTemplate { get; set; }

    }
}
