using System;
using System.Reflection;

namespace Rong.Volo.Abp.CodeGenerator.Vue.Models
{
    /// <summary>
    /// dto模型类
    /// </summary>
    public class TemplateVueDtoType
    {
        /// <summary>
        /// 查询类型
        /// </summary>
        public Type? SearchType { get; set; }

        /// <summary>
        /// 分页列表类型
        /// </summary>
        public Type? PageType { get; set; }

        /// <summary>
        /// 详情类型
        /// </summary>
        public Type? DetailType { get; set; }

        /// <summary>
        /// 创建类型
        /// </summary>
        public Type? CreateType { get; set; }

        /// <summary>
        /// 修改类型
        /// </summary>
        public Type? UpdateType { get; set; }
    }
}
