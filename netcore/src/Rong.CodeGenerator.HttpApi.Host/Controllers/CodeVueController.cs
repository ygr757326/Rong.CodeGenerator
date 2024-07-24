using System;
using Microsoft.AspNetCore.Mvc;
using Rong.CodeGenerator.App.Apps.Dto;
using Rong.Volo.Abp.CodeGenerator.Vue;
using Rong.Volo.Abp.CodeGenerator.Vue.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Namotion.Reflection;
using Rong.CodeGenerator.App.Dictionarys;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Reflection;

namespace Rong.CodeGenerator.Controllers;

/// <summary>
/// vue代码生成器
/// </summary>
public class CodeVueController : AbpController
{
    private readonly RongVoloAbpCodeGeneratorVueStore _codeGeneratorStore;
    public CodeVueController(RongVoloAbpCodeGeneratorVueStore codeGeneratorStore)
    {
        _codeGeneratorStore = codeGeneratorStore;
    }

    /// <summary>
    /// 代码生成
    /// </summary>
    /// <returns></returns>+
    public async Task<ActionResult> GoAsync()
    {

        //开始生成
        await _codeGeneratorStore.StartAsync(typeof(IEntity), 
            typeof(CodeGeneratorDomainModule), 
            typeof(CodeGeneratorApplicationContractsModule), 
            CodeGeneratorRemoteServiceConsts.RootPath, 
            "E:\\MY\\Rong.CodeGenerator\\vue\\vben_demo"
            ,new []{typeof(Dictionary)}
            );

        
        return Content("ok");

    }
}
