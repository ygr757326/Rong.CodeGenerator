using Rong.CodeGenerator.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace Rong.CodeGenerator;

public abstract class CodeGeneratorController : AbpControllerBase
{
    protected CodeGeneratorController()
    {
        LocalizationResource = typeof(CodeGeneratorResource);
    }
}
