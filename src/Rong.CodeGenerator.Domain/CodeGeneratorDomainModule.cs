using Volo.Abp.Domain;
using Volo.Abp.Modularity;

namespace Rong.CodeGenerator;

[DependsOn(
    typeof(AbpDddDomainModule),
    typeof(CodeGeneratorDomainSharedModule)
)]
public class CodeGeneratorDomainModule : AbpModule
{

}
