using Volo.Abp.Application;
using Volo.Abp.Modularity;
using Volo.Abp.Authorization;

namespace Rong.CodeGenerator;

[DependsOn(
    typeof(CodeGeneratorDomainSharedModule),
    typeof(AbpDddApplicationContractsModule),
    typeof(AbpAuthorizationModule)
    )]
public class CodeGeneratorApplicationContractsModule : AbpModule
{

}
