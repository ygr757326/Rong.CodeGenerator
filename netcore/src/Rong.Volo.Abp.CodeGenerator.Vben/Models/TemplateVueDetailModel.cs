﻿using System.Collections.Generic;

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
        public List<TemplateVueModelData>? View { get; set; } = new List<TemplateVueModelData>();
        /// <summary>
        /// 视图数据字符串
        /// </summary>
        public string? ViewString { get; set; }

    }
}
