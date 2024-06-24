using Rong.CodeGenerator.Localization;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;

namespace Rong.CodeGenerator{
    /// <summary>
    /// 控制器基类
    /// </summary>
    [Area(CodeGeneratorRemoteServiceConsts.ModuleName)]
    [RemoteService(Name = CodeGeneratorRemoteServiceConsts.RemoteServiceName)]
    public abstract class CodeGeneratorControllerBase : AbpControllerBase
    {
        protected CodeGeneratorControllerBase()
        {
            LocalizationResource = typeof(CodeGeneratorResource);
        }
    }
}