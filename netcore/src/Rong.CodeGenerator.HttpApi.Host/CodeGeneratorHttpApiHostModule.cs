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
    typeof(CodeGeneratorModule),
    typeof(CodeGeneratorVueModule)
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
