using Rong.Volo.Abp.CodeGenerator.Vue.Models;
using Rong.Volo.Abp.CodeGenerator.Vue.TemplateHelpers.Vben2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rong.Volo.Abp.CodeGenerator.Vue.Enums;
using Volo.Abp.DependencyInjection;

namespace Rong.Volo.Abp.CodeGenerator.Vue.TemplateHelpers
{
    /// <summary>
    /// vben代码生成器帮助器
    /// </summary>
    public interface IRongVoloAbpVueVbenTemplate : ISingletonDependency
    {
        VbenVersionEnum Version { get; }

        /// <summary>
        /// 获取table表头列插槽 模板
        /// </summary>
        /// <param name="models"></param>
        /// <param name="space"></param>
        /// <returns></returns>
        string GetTableColumnsSlotsTemplate(List<TemplateVueEntityPropertyData> models, int space = 8);

        /// <summary>
        /// 获取vue详情页 模板
        /// </summary>
        /// <returns></returns>
        string? GetDetailTemplate(List<TemplateVueEntityPropertyData> models, int space = 8);


        /// <summary>
        /// 获取table表头列 模板
        /// </summary>
        /// <returns></returns>
        string? GetTableColumnsTemplate(List<TemplateVueEntityPropertyData> models, int space = 4);


        /// <summary>
        /// 获取table表单 模板
        /// </summary>
        /// <returns></returns>
        string? GetTableSchemasTemplate(List<TemplateVueEntityPropertyData> models, int space = 8);


        /// <summary>
        /// 获取新增编辑表单 模板
        /// </summary>
        /// <returns></returns>
        string? GetFormTemplate(List<TemplateVueEntityPropertyData> models, int space = 4);

    }
}