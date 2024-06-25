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
        /// 命名空间
        /// </summary>
        public string? NameSpace { get; set; }

        /// <summary>
        /// 权限组
        /// </summary>
        public string? PermissionGroup { get; set; }

        /// <summary>
        /// 项目名称
        /// </summary>
        public string? Project { get; set; }

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
        /// <param name="permissionGroup">权限组</param>
        /// <param name="nameSpace">命名空间</param>
        /// <param name="project">项目</param>
        public TemplateVueModel(string entity, string entityName, string? permissionGroup = null, string? nameSpace = null, string? project = null)
        {
            Entity = entity;
            EntityName = entityName;
            NameSpace = nameSpace;
            PermissionGroup = permissionGroup;
            Project = project;
        }

        /// <summary>
        /// 设置项目
        /// </summary>
        /// <returns></returns>
        public void SetProject()
        {
            if (!string.IsNullOrWhiteSpace(Project))
            {
                return;
            }

            Project = GetProject(NameSpace);
        }

        /// <summary>
        /// 获取项目
        /// </summary>
        /// <returns></returns>
        public static string? GetProject(string? nameSpace)
        {
            if (string.IsNullOrWhiteSpace(nameSpace))
            {
                return nameSpace;
            }

            string[] s = nameSpace.Split('.');

            if (s.Length == 0 || s.Length == 1)
            {
                return nameSpace;
            }
            return s[1];
        }
    }
}
