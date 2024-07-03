using System.Collections.Generic;

namespace Rong.Volo.Abp.CodeGenerator.Vue.Models.Pages
{
    /// <summary>
    /// 主页模型
    /// </summary>
    public class TemplateVuePageIndexModel : TemplateVueEntityModelBase
    {
        /// <summary>
        /// Table Columns列
        /// </summary>
        public List<TemplateVueEntityPropertyData>? TableColumns { get; set; } = new List<TemplateVueEntityPropertyData>();

        /// <summary>
        /// Table Columns列模板字符串
        /// </summary>
        public string? TableColumnsTemplate { get; set; }

        /// <summary>
        /// Table Columns列Slots模板字符串
        /// </summary>
        public string? TableColumnsSlotsTemplate { get; set; }

        /// <summary>
        /// Table Schemas 查询表单
        /// </summary>
        public List<TemplateVueEntityPropertyData>? TableSchemas { get; set; } = new List<TemplateVueEntityPropertyData>();

        /// <summary>
        /// Table Schemas 查询表单模板字符串
        /// </summary>
        public string? TableSchemasTemplate { get; set; }

    }
}
