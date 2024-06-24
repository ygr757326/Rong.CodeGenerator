using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;
using System.Threading.Tasks;

namespace Rong.CodeGenerator;

public class Program
{
    public static async Task<int> Main(string[] args)
    {
        var assemblyName = typeof(Program).Assembly.GetName().Name;
        //日志配置
        SerilogConfigurationHelper.Configure(assemblyName);

        try
        {
            Log.Information($"开始启动 {assemblyName}.");
            var builder = WebApplication.CreateBuilder(args);
            builder.Host.AddAppSettingsSecretsJson()
                .UseAutofac()
                .UseSerilog();
            await builder.AddApplicationAsync<CodeGeneratorHttpApiHostModule>();
            var app = builder.Build();
            await app.InitializeApplicationAsync();
            await app.RunAsync();
            return 0;
        }
        catch (Exception ex)
        {
            if (ex is HostAbortedException)
            {
                throw;
            }

            Log.Fatal(ex, $"{assemblyName} 意外终止!");
            return 1;
        }
        finally
        {
            Log.CloseAndFlush();
        }
    }
}