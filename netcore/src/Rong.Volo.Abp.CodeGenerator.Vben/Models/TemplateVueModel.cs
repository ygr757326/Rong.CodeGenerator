using System;
using System.Reflection;

namespace Rong.Volo.Abp.CodeGenerator.Vue.Models
{
    /// <summary>
    /// 模板模型
    /// </summary>
    public class TemplateVueModel
    {
        public CodeGeneratorVueOptions Options { get; internal set; }

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
        /// api根路径
        /// <para>例：生成的api为 '/api/<see cref="ApiRootPath"/>/<see cref="EntityCase"/>/getList',</para>
        /// </summary>
        public string? ApiRootPath { get; set; }

        /// <summary>
        /// 权限组
        /// </summary>
        public string? PermissionGroup { get; set; }


        /// <summary>
        /// 权限 - 查看
        /// </summary>
        public string? PermissionIndex => (string.IsNullOrWhiteSpace(PermissionGroup) ? null : PermissionGroup + ".") + Entity;

        /// <summary>
        /// 权限 - 新增
        /// </summary>
        public string? PermissionCreate => PermissionIndex + ".Create";
        /// <summary>
        /// 权限 - 修改
        /// </summary>
        public string? PermissionUpdate => PermissionIndex + ".Update";
        /// <summary>
        /// 权限 - 删除
        /// </summary>
        public string? PermissionDelete => PermissionIndex + ".Delete";

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
        /// <param name="apiRootPath">api根路径</param>
        public TemplateVueModel(string entity, string entityName, TemplateVueModelType entityType, string? permissionGroup = null, string? apiRootPath = null)
        {
            Entity = entity;
            EntityName = entityName;
            PermissionGroup = permissionGroup;
            ApiRootPath = apiRootPath;
            EntityType = entityType;
        }
    }
}
