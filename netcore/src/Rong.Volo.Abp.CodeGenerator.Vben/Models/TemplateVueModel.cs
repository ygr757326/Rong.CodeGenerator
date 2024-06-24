using System;
using System.Reflection;

namespace Rong.Volo.Abp.CodeGenerator.Vue.Models
{
    /// <summary>
    /// 模板模型
    /// </summary>
    public class TemplateVueModel
    {
        /// <summary>
        /// 实体
        /// </summary>
        public string Entity { get; set; }

        /// <summary>
        /// 实体小写
        /// </summary>
        public string? EntityCase => Entity.IsNullOrWhiteSpace() ? null : $"{char.ToLowerInvariant(Entity[0]) + Entity.Substring(1)}";

        /// <summary>
        /// 实体名称
        /// </summary>
        public string EntityName { get; set; }

        /// <summary>
        /// 命名空间
        /// </summary>
        public string? NameSpace { get; set; }

        /// <summary>
        /// 项目名称
        /// </summary>
        public string? Project
        {
            get
            {
                if (string.IsNullOrWhiteSpace(NameSpace))
                {
                    return NameSpace;
                }
                string[] s = NameSpace.Split('.');

                if (s.Length == 0 || s.Length == 1)
                {
                    return NameSpace;
                }
                return s[1];
            }
        }

        /// <summary>
        /// 构造
        /// </summary>
        public TemplateVueModel()
        {
        }

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="entityName"></param>
        /// <param name="nameSpace"></param>
        public TemplateVueModel(string entity, string entityName, string? nameSpace = null)
        {
            Entity = entity;
            EntityName = entityName;
            NameSpace = nameSpace;
        }
    }
}
