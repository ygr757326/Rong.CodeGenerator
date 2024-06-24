using Volo.Abp.Modularity;

namespace Rong.CodeGenerator;

/* Inherit from this class for your domain layer tests.
 * See SampleManager_Tests for example.
 */
public abstract class CodeGeneratorDomainTestBase<TStartupModule> : CodeGeneratorTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
