using Rong.CodeGenerator.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace Rong.CodeGenerator.Permissions;

public class CodeGeneratorPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(CodeGeneratorPermissions.GroupName, L("Permission:CodeGenerator"));

        //context.CreateAllPermission<CodeGeneratorResource>(typeof(CodeGeneratorApplicationContractsModule));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<CodeGeneratorResource>(name);
    }
}
