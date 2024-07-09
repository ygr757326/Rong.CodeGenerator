using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Rong.CodeGenerator.EntityFrameworkCore;
using System.Collections.Generic;
using Rong.Volo.Abp.CodeGenerator;
using Rong.Volo.Abp.CodeGenerator.Vue;
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
            //Vben 组件替换
            options.ComponentMapForVben = new();

            //枚举Select
            options.EnumSelectComponent = "EnumSelect";
            options.EnumSelectComponentProp = "code";

            //枚举Radio
            options.EnumRadioComponent = "EnumRadio";
            options.EnumRadioComponentProp = "code";

            //字典Select
            options.DictionarySelectComponent = "DictSelect";
            options.DictionarySelectComponentProp = "code";

            //字典Radio
            options.DictionaryRadioComponent = "DictRadio";
            options.DictionaryRadioComponentProp = "code";

            //bool
            options.BoolSelectComponent = "BoolSelect";
            options.BoolRadioComponent = "BoolRadio";

            //文件上传
            options.FileUploadComponent = "GzUploadFile";

            //图片上传
            options.ImageUploadComponent = "GzUploadFile";

            //文件预览
            options.FilePreviewComponent = "GzUploadFile";
            options.FilePreviewComponentProp = "v-model";

            //图片预览
            options.ImagePreviewComponent = "GzImagePreview";
            options.ImagePreviewComponentProp = "fileId";

            ////编辑器
            //options.EditorComponent = "GzEditor";
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
