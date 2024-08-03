using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Rong.CodeGenerator.EntityFrameworkCore;
using System.Collections.Generic;
using Rong.Volo.Abp.CodeGenerator;
using Rong.Volo.Abp.CodeGenerator.Vue;
using Rong.Volo.Abp.CodeGenerator.Vue.Enums;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc.UI.MultiTenancy;
using Volo.Abp.AspNetCore.Serilog;
using Volo.Abp.Autofac;
using Volo.Abp.Caching.StackExchangeRedis;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Modularity;
using Volo.Abp.Swashbuckle;

namespace Rong.CodeGenerator;

[DependsOn(
    typeof(CodeGeneratorApplicationModule),
    typeof(CodeGeneratorEntityFrameworkCoreModule),
    typeof(CodeGeneratorHttpApiModule),
    typeof(AbpAspNetCoreMvcUiMultiTenancyModule),
    typeof(AbpAutofacModule),
    typeof(AbpCachingStackExchangeRedisModule),
    typeof(AbpAspNetCoreSerilogModule),
    typeof(AbpSwashbuckleModule),

    //代码生成
    typeof(RongVoloAbpCodeGeneratorModule),
    typeof(RongVoloAbpCodeGeneratorVueModule)
    )]
public class CodeGeneratorHttpApiHostModule : AbpModule
{

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        var hostingEnvironment = context.Services.GetHostingEnvironment();
        var configuration = context.Services.GetConfiguration();

        Configure<AbpDbContextOptions>(options =>
        {
            options.UseSqlServer();
        });

        context.Services.AddAbpSwaggerGenWithOAuth(
            configuration["AuthServer:Authority"]!,
            new Dictionary<string, string>
            {
                {"CodeGenerator", "CodeGenerator API"}
            },
            options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "CodeGenerator API", Version = "v1" });
                options.DocInclusionPredicate((docName, description) => true);
                options.CustomSchemaIds(type => type.FullName);
            });

        //vue代码生成器
        Configure<RongVoloAbpCodeGeneratorVueOptions>(options =>
        {
            //Ant 的 Tabled 的 DataIndex 嵌套模式:。
            //2.x 版本 为 a.b.c
            //3.x,4.x 版本 为 ['a','b','c']
            options.AntTabledDataIndexMode = AntTabledDataIndexModeEnum.Dotted;

            //Vben 组件替换
            options.ComponentMapForVben = new();

            //枚举Select
            options.EnumSelectComponent = "MyEnumSelect";
            options.EnumSelectComponentProp = "code";

            //枚举Radio
            options.EnumRadioComponent = "MyEnumRadio";
            options.EnumRadioComponentProp = "code";

            //枚举Checkbox
            options.EnumCheckboxComponent = "MyEnumCheckbox";
            options.EnumCheckboxComponentProp = "code";

            //字典Select
            options.DictionarySelectComponent = "MyDictSelect";
            options.DictionarySelectComponentProp = "code";

            //字典Radio
            options.DictionaryRadioComponent = "MyDictRadio";
            options.DictionaryRadioComponentProp = "code";

            //字典Checkbox
            options.DictionaryCheckboxComponent = "MyDictCheckbox";
            options.DictionaryCheckboxComponentProp = "code";

            //bool
            options.BoolSelectComponent = "MyBoolSelect";
            options.BoolRadioComponent = "BoolRadio";

            //文件上传
            options.FileUploadComponent = "MyUploadFile";

            //图片上传
            options.ImageUploadComponent = "MyUploadFile";

            //文件预览
            options.FilePreviewComponent = "MyFileViewList";
            options.FilePreviewComponentProp = "fileList";

            //图片预览
            options.ImagePreviewComponent = "MyImagePreview";
            options.ImagePreviewComponentProp = "fileId";

            ////编辑器
            //options.EditorComponent = "MyEditor";
        });

    }

    public override void OnApplicationInitialization(ApplicationInitializationContext context)
    {
        var app = context.GetApplicationBuilder();
        var env = context.GetEnvironment();

        //if (env.IsDevelopment())
        //{
        //    app.UseDeveloperExceptionPage();
        //}
        //else
        //{
        //    app.UseHsts();
        //}

        app.UseHttpsRedirection();
        app.UseCorrelationId();
        app.UseStaticFiles();
        app.UseRouting();
        //app.UseCors();
        //app.UseAuthentication();

        app.UseAbpRequestLocalization();
        //app.UseAuthorization();
        app.UseSwagger();
        app.UseAbpSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "Support APP API");

            var configuration = context.GetConfiguration();
            options.OAuthClientId(configuration["AuthServer:SwaggerClientId"]);
            options.OAuthScopes("CodeGenerator");
        });
        //app.UseAuditing();
        app.UseAbpSerilogEnrichers();
        app.UseConfiguredEndpoints();
    }
}
