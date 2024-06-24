using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace Rong.CodeGenerator.EntityFrameworkCore;

[DependsOn(
    typeof(CodeGeneratorDomainModule),
    typeof(AbpEntityFrameworkCoreModule)
)]
public class CodeGeneratorEntityFrameworkCoreModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAbpDbContext<CodeGeneratorDbContext>(options =>
        {
                /* Add custom repositories here. Example:
                 * options.AddRepository<Question, EfCoreQuestionRepository>();
                 */
        });
    }
}
