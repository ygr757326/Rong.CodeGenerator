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
        public string? EntityCase { get; internal set; }

        /// <summary>
        /// 实体名称
        /// </summary>
        public string EntityName { get; set; }

        /// <summary>
        /// 服务名称
        /// </summary>
        public string? ServiceName { get; set; }

        /// <summary>
        /// 权限组
        /// </summary>
        public string? PermissionGroup { get; set; }

        /// <summary>
        /// 实体类型
        /// </summary>
        public TemplateVueModelType EntityType { get; set; }

        /// <summary>
        /// 构造
        /// </summary>
        public TemplateVueModel()
        {
        }

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="entityName">实体显示名称</param>
        /// <param name="entityType">实体类型</param>
        /// <param name="permissionGroup">权限组</param>
        /// <param name="serviceName">服务名称</param>
        public TemplateVueModel(string entity, string entityName, TemplateVueModelType entityType, string? permissionGroup = null, string? serviceName = null)
        {
            Entity = entity;
            EntityName = entityName;
            PermissionGroup = permissionGroup;
            ServiceName = serviceName;
            EntityType = entityType;
        }
    }
}
