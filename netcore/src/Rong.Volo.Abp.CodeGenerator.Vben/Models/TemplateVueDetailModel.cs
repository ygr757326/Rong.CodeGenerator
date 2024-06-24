using System.Collections.Generic;

namespace Rong.Volo.Abp.CodeGenerator.Vue.Models
{
    /// <summary>
    /// 详情页模型
    /// </summary>
    public class TemplateVueDetailModel: TemplateVueModel
    {
        /// <summary>
        /// 视图数据
        /// </summary>
        public List<TemplateVueModelData>? View { get; set; }

    }
}
