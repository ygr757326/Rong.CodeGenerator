using System;
using Microsoft.AspNetCore.Mvc;
using Rong.CodeGenerator.App.Apps.Dto;
using Rong.Volo.Abp.CodeGenerator.Vue;
using Rong.Volo.Abp.CodeGenerator.Vue.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Namotion.Reflection;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Domain.Entities;

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
        List<TemplateVueModel> list = new List<TemplateVueModel>();

        var entitys = typeof(CodeGeneratorDomainModule).Assembly.GetTypes()
            .Where(x => typeof(IEntity).IsAssignableFrom(x));

        var dtos = typeof(CodeGeneratorApplicationContractsModule).Assembly.GetTypes();

        foreach (var entity in entitys)
        {
            var name = entity.Name;
            var displayName = entity.GetCustomAttribute<TableAttribute>()?.Name??entity.GetXmlDocsSummary();
            var page = dtos.FirstOrDefault(a => a.Name == $"{name}PageOutput");
            var search = dtos.FirstOrDefault(a => a.Name == $"{name}PageSearchInput");
            var create = dtos.FirstOrDefault(a => a.Name == $"{name}CreateInput");
            var update = dtos.FirstOrDefault(a => a.Name == $"{name}UpdateInput");
            var detail = dtos.FirstOrDefault(a => a.Name == $"{name}DetailOutput");

            list.Add(new TemplateVueModel(name, displayName, new TemplateVueModelType()
            {
                SearchType = search,
                PageType = page,
                DetailType = detail,
                CreateType = create,
                UpdateType = update,
            }));
        }


        //开始生成
        await _codeGeneratorStore.StartAsync(list, "CodeGenerator", "E:\\MY\\Rong.CodeGenerator\\vue\\vben_demo\\");

        return Content("ok");

    }
}
