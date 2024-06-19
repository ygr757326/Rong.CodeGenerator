using Volo.Abp.Reflection;

namespace Rong.CodeGenerator.Permissions;

public class CodeGeneratorPermissions
{
    public const string GroupName = "CodeGenerator";

    public static string[] GetAll()
    {
        return ReflectionHelper.GetPublicConstantsRecursively(typeof(CodeGeneratorPermissions));
    }
}
