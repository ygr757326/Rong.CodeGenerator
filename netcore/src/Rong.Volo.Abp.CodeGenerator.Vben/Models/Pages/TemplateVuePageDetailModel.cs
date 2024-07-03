using System.Collections.Generic;

namespace Rong.Volo.Abp.CodeGenerator.Vue.Models.Pages
{
    /// <summary>
    /// 详情页模型
    /// </summary>
    public class TemplateVuePageDetailModel : TemplateVueEntityModelBase
    {
        /// <summary>
        /// 详情模板
        /// </summary>
        public List<TemplateVueEntityPropertyData>? Detail { get; set; } = new List<TemplateVueEntityPropertyData>();

        /// <summary>
        /// 详情字符串模板
        /// </summary>
        public string? DetailTemplate { get; set; }

    }
}
