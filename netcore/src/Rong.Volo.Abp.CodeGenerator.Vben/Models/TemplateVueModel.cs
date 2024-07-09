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
        /// 实体显示名称
        /// </summary>
        public string EntityName { get; set; }

        /// <summary>
        /// api根路径
        /// <para>例：生成的api为 '/api/<see cref="ApiRootPath"/>/<see cref="EntityCase"/>/getList',</para>
        /// </summary>
        public string? ApiRootPath { get; set; }

        /// <summary>
        /// 实体dto类型
        /// </summary>
        public TemplateVueDtoType EntityDtoType { get; set; }

        /// <summary>
        /// 权限
        /// </summary>
        public TemplateVuePermissionModel Permission { get; set; }

        /// <summary>
        /// 配置
        /// </summary>
        public RongVoloAbpCodeGeneratorVueOptions Options { get; internal set; }

        /// <summary>
        /// 实体小写
        /// </summary>
        public string? EntityCase { get; set; }

        public TemplateVueModel()
        {

        }

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="entityName">实体显示名称</param>
        /// <param name="entityDtoType">实体dto类型</param>
        /// <param name="permission">权限</param>
        /// <param name="apiRootPath">api根路径</param>
        public TemplateVueModel(string entity, string entityName, TemplateVueDtoType entityDtoType, TemplateVuePermissionModel permission, string? apiRootPath = null)
        {
            Entity = entity;
            EntityCase = Entity.ToCamelCase();

            EntityName = entityName;
            ApiRootPath = apiRootPath;

            Permission = permission;
            EntityDtoType = entityDtoType;
        }
    }
}
