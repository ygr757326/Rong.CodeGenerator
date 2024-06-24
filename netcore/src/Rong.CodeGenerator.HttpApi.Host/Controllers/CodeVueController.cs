using Microsoft.AspNetCore.Mvc;
using Rong.Volo.Abp.CodeGenerator.Vue;
using Rong.Volo.Abp.CodeGenerator.Vue.Models;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.Mvc;

namespace Rong.CodeGenerator.Controllers;

/// <summary>
/// vue代码生成器
/// </summary>
public class CodeVueController : AbpController
{
    private readonly CodeGeneratorVueStore _codeGeneratorStore;
    public CodeVueController(CodeGeneratorVueStore codeGeneratorStore)
    {
        _codeGeneratorStore = codeGeneratorStore;
    }

    /// <summary>
    /// 代码生成
    /// </summary>
    /// <returns></returns>+
    public async Task<ActionResult> GoAsync()
    {
        List<TemplateVueModel> list = new List<TemplateVueModel>()
        {
            new ("App", "应用"),
        };

        //开始生成
        await _codeGeneratorStore.StartAsync(list, "Rong.CodeGenerator", "F:\\MY\\Rong.CodeGenerator\\vue\\vben_demo");

        return Content("ok");

    }
}
