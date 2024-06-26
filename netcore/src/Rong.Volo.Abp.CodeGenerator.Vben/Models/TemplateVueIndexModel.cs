using System.Collections.Generic;

namespace Rong.Volo.Abp.CodeGenerator.Vue.Models
{
    /// <summary>
    /// 主页模型
    /// </summary>
    public class TemplateVueIndexModel : TemplateVueModel
    {
        /// <summary>
        /// Table Columns列
        /// </summary>
        public List<TemplateVueModelData>? TableColumns { get; set; } = new List<TemplateVueModelData>();

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
        public List<TemplateVueModelData>? TableSchemas { get; set; } = new List<TemplateVueModelData>();

        /// <summary>
        /// Table Schemas 查询表单模板字符串
        /// </summary>
        public string? TableSchemasTemplate { get; set; }

    }
}
