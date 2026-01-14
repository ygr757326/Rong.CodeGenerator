using Microsoft.AspNetCore.Mvc;
using Rong.Volo.Abp.CodeGenerator;
using Rong.Volo.Abp.CodeGenerator.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.Mvc;

namespace Rong.CodeGenerator.Controllers;

/// <summary>
/// 代码生成器
/// </summary>
public class CodeController : AbpController
{
    private readonly RongVoloAbpCodeGeneratorStore _codeGeneratorStore;
    public CodeController(RongVoloAbpCodeGeneratorStore codeGeneratorStore)
    {
        _codeGeneratorStore = codeGeneratorStore;
    }

    /// <summary>
    /// 代码生成
    /// </summary>
    /// <returns></returns>+
    public async Task<ActionResult> GoAsync()
    {
        List<TemplateModel> list = new List<TemplateModel>()
        {
            new ("MyTest", "我的测试"),
        };

        //开始生成
        await _codeGeneratorStore.StartAsync(list, "Rong.CodeGenerator", saveFolderName: "App");

        return Content("ok");

    }
}
