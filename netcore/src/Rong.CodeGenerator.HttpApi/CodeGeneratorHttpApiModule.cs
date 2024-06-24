using Localization.Resources.AbpUi;
using Rong.CodeGenerator.Localization;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Microsoft.Extensions.DependencyInjection;

namespace Rong.CodeGenerator;

[DependsOn(
    typeof(CodeGeneratorApplicationContractsModule),
    typeof(AbpAspNetCoreMvcModule))]
public class CodeGeneratorHttpApiModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        PreConfigure<IMvcBuilder>(mvcBuilder =>
        {
            mvcBuilder.AddApplicationPartIfNotExists(typeof(CodeGeneratorHttpApiModule).Assembly);
        });
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpLocalizationOptions>(options =>
        {
            options.Resources
                .Get<CodeGeneratorResource>()
                .AddBaseTypes(typeof(AbpUiResource));
        });
    }
}
