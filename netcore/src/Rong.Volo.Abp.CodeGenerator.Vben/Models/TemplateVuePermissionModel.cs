using System;
using System.Reflection;

namespace Rong.Volo.Abp.CodeGenerator.Vue.Models
{
    /// <summary>
    /// 模板模型
    /// </summary>
    public class TemplateVuePermissionModel
    {
        /// <summary>
        /// 权限组
        /// </summary>
        public string? Group { get; set; }

        /// <summary>
        /// 权限 - 查看
        /// </summary>
        public string? Index { get; set; }

        /// <summary>
        /// 权限 - 新增
        /// </summary>
        public string? Create { get; set; }
        /// <summary>
        /// 权限 - 修改
        /// </summary>
        public string? Update { get; set; }
        /// <summary>
        /// 权限 - 删除
        /// </summary>
        public string? Delete { get; set; }

        /// <summary>
        /// 构造
        /// </summary>
        public TemplateVuePermissionModel()
        {

        }
        /// <summary>
        /// 构造
        /// </summary>
        public TemplateVuePermissionModel(string entity, string? permissionGroup)
        {
            Group = permissionGroup;

            if (entity == null)
            {
                return;
            }
            Index = (string.IsNullOrWhiteSpace(permissionGroup) ? null : permissionGroup + ".") + entity;
            Create = Index + ".Create";
            Update = Index + ".Update";
            Delete = Index + ".Delete";
        }

    }
}
