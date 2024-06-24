using Volo.Abp.Application;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;

namespace Rong.CodeGenerator;

[DependsOn(
    typeof(AbpDddApplicationModule),
    typeof(CodeGeneratorDomainModule),
    typeof(CodeGeneratorApplicationContractsModule)
)]
public class CodeGeneratorApplicationModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpAutoMapperOptions>(options =>
        {
            options.AddMaps<CodeGeneratorApplicationModule>();
        });
    }
}
