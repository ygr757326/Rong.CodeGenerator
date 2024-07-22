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
using Rong.Volo.Abp.CodeGenerator;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Reflection;
using Rong.CodeGenerator.Enums;

namespace Rong.CodeGenerator.Controllers;

/// <summary>
/// 字典代码生成器
/// </summary>
public class CodeDictController : AbpController
{
    private readonly RongVoloAbpCodeGeneratorDictionaryDataSeedStore _codeGeneratorStore;
    public CodeDictController(RongVoloAbpCodeGeneratorDictionaryDataSeedStore codeGeneratorStore)
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
        await _codeGeneratorStore.StartAsync(typeof(DictionaryTypeEnum), "Rong.CodeGenerator");

        
        return Content("ok");

    }
}
