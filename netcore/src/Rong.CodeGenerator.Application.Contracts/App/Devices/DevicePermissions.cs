using Rong.CodeGenerator.Permissions;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Reflection;

namespace Rong.CodeGenerator.App.Devices
{
    /// <summary>
    /// Device管理 - 权限名称
    /// </summary>
    public class DevicePermissions
    {
        /// <summary>
        /// 权限组
        /// </summary>
        public const string GroupName = "Other";

        /// <summary>
        /// 权限
        /// </summary>
        public static class Permissions
        {
            /// <summary>
            /// Device管理（查看权限）
            /// </summary>
            [Display(Name = "Device管理")]
            public const string Default = GroupName + ".Device";

            /// <summary>
            /// 创建
            /// </summary>
            public const string Create = Default + PermissionConsts.Create;
            /// <summary>
            /// 修改
            /// </summary>
            public const string Update = Default + PermissionConsts.Update;
            /// <summary>
            /// 删除
            /// </summary>
            public const string Delete = Default + PermissionConsts.Delete;
        }

        /// <summary>
        /// 获取所有权限
        /// </summary>
        /// <returns></returns>
        public static string[] GetAll()
        {
            return ReflectionHelper.GetPublicConstantsRecursively(typeof(DevicePermissions));
        }
    }
}