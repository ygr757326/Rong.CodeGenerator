using System.ComponentModel.DataAnnotations;
using Volo.Abp.Reflection;

namespace Rong.CodeGenerator.Permissions;

/// <summary>
/// 公共权限
/// </summary>
public class PermissionConsts
{
    /// <summary>
    /// 新增
    /// </summary>
    [Display(Name = "新增")]
    public const string Create = ".Create";

    /// <summary>
    /// 删除
    /// </summary>
    [Display(Name = "删除")]
    public const string Delete = ".Delete";

    /// <summary>
    /// 修改
    /// </summary>
    [Display(Name = "修改")]
    public const string Update = ".Update";

    /// <summary>
    /// 启用
    /// </summary>
    [Display(Name = "启用")]
    public const string Enabled = ".Enabled";
    /// <summary>
    /// 禁用
    /// </summary>
    [Display(Name = "禁用")]
    public const string Disabled = ".Disabled";

    /// <summary>
    /// 导出
    /// </summary>
    [Display(Name = "导出")]
    public const string Export = ".Export";

    /// <summary>
    /// 导入
    /// </summary>
    [Display(Name = "导入")]
    public const string Import = ".Import";

    public static string[] GetAll()
    {
        return ReflectionHelper.GetPublicConstantsRecursively(typeof(PermissionConsts));
    }
}

