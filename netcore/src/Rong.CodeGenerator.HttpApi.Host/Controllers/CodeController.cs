﻿using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Rong.Volo.Abp.CodeGenerator;
using Volo.Abp.AspNetCore.Mvc;

namespace Rong.CodeGenerator.Controllers;

/// <summary>
/// 代码生成器
/// </summary>
public class CodeController : AbpController
{
    private readonly CodeGeneratorStore _codeGeneratorStore;
    public CodeController(CodeGeneratorStore codeGeneratorStore)
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
        await _codeGeneratorStore.StartAsync(list, false, nameSpace: "Rong.CodeGenerator");

        return Content("ok");

    }
}
