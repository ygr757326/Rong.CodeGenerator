using Volo.Abp.Modularity;

namespace Rong.CodeGenerator;

[DependsOn(
    typeof(CodeGeneratorApplicationModule),
    typeof(CodeGeneratorDomainTestModule)
    )]
public class CodeGeneratorApplicationTestModule : AbpModule
{

}
