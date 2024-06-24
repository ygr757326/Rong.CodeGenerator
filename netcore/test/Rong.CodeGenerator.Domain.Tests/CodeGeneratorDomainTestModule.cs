using Volo.Abp.Modularity;

namespace Rong.CodeGenerator;

[DependsOn(
    typeof(CodeGeneratorDomainModule),
    typeof(CodeGeneratorTestBaseModule)
)]
public class CodeGeneratorDomainTestModule : AbpModule
{

}
