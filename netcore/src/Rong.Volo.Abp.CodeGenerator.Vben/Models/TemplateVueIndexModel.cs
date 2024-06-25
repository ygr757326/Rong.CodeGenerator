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
        public List<TemplateVueModelData>? Table { get; set; } = new List<TemplateVueModelData>();

        /// <summary>
        /// Table 列表数据字符串
        /// </summary>
        public string? TableString { get; set; }

        /// <summary>
        /// 查询数据
        /// </summary>
        public List<TemplateVueModelData>? Search { get; set; } = new List<TemplateVueModelData>();

        /// <summary>
        /// 查询数据字符串
        /// </summary>
        public string? SearchString { get; set; }

    }
}
