using System.Collections.Generic;

namespace Rong.Volo.Abp.CodeGenerator.Vue.Models
{
    /// <summary>
    /// 主页模型
    /// </summary>
    public class TemplateVueIndexModel: TemplateVueModel
    {
        /// <summary>
        /// Table 列表数据
        /// </summary>
        public List<TemplateVueModelData>? Table { get; set; }

        /// <summary>
        /// 查询数据
        /// </summary>
        public List<TemplateVueModelData>? Search { get; set; }

    }
}
