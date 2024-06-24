using Rong.CodeGenerator.Localization;
using Volo.Abp.Application.Services;

namespace Rong.CodeGenerator;

/* Inherit your application services from this class.
 */
public abstract class CodeGeneratorAppService : ApplicationService
{
    protected CodeGeneratorAppService()
    {
        LocalizationResource = typeof(CodeGeneratorResource);
    }
}
