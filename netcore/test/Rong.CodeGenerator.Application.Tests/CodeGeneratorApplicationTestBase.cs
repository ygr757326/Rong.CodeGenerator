using Volo.Abp.Modularity;

namespace Rong.CodeGenerator;

/* Inherit from this class for your application layer tests.
 * See SampleAppService_Tests for example.
 */
public abstract class CodeGeneratorApplicationTestBase<TStartupModule> : CodeGeneratorTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
