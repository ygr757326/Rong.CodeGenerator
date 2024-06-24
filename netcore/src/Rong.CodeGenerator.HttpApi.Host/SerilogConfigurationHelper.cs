using Serilog;
using Serilog.Events;
using System.Text;

namespace Rong.CodeGenerator;

/// <summary>
/// Serilog 日志配置
/// </summary>
public static class SerilogConfigurationHelper
{
    /// <summary>
    /// 配置日志
    /// </summary>
    /// <param name="applicationName"></param>
    public static void Configure(string applicationName)
    {
        // var configuration = new ConfigurationBuilder()
        //     .SetBasePath(Directory.GetCurrentDirectory())
        //     .AddJsonFile("appsettings.json")
        //     .AddEnvironmentVariables()
        //     .Build();


        long mb = 1024 * 1024 * 10;

        string SerilogOutputTemplate = "{NewLine}时间：{Timestamp:yyyy-MM-dd HH:mm:ss.fff}{NewLine}级别：{Level}{NewLine}消息：{Message}{NewLine}{Exception}";

        //使用配置文件
        //Log.Logger = new LoggerConfiguration()
        //    .ReadFrom.Configuration(configuration, sectionName: "Serilog")
        //    .CreateLogger();

        //不使用配置文件
        Log.Logger = new LoggerConfiguration()
#if DEBUG
                            .MinimumLevel.Verbose()
#else
                                .MinimumLevel.Information()
#endif
                            //覆盖其他日志记录源的级别
                            .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                            .MinimumLevel.Override("Microsoft.EntityFrameworkCore", LogEventLevel.Warning)

                            //记录相关上下文信息 
                            .Enrich.FromLogContext()
                            .Enrich.WithProperty("Application", $"{applicationName}")
                            //.WriteTo.Async(c => c.File("Logs/logs.txt"))
                            //#if DEBUG

                            //输出到控制台
                            .WriteTo.Async(c => c.Console(outputTemplate: SerilogOutputTemplate))
                            //#endif

                            //输出到文件
                            .WriteTo.Logger(lc => lc
                                .Filter.ByIncludingOnly(a => a.Level == LogEventLevel.Debug)
                                .WriteTo.File("Logs/Debug/Debug-.txt",
                                    fileSizeLimitBytes: mb,
                                    rollOnFileSizeLimit: true,
                                    rollingInterval: RollingInterval.Day,
                                    outputTemplate: SerilogOutputTemplate,
                                    encoding: Encoding.UTF8))

                            .WriteTo.Logger(lc => lc
                                .Filter.ByIncludingOnly(a => a.Level == LogEventLevel.Information)
                                .WriteTo.File("Logs/Info/Info-.txt",
                                    fileSizeLimitBytes: mb,
                                    rollOnFileSizeLimit: true,
                                    rollingInterval: RollingInterval.Day,
                                    outputTemplate: SerilogOutputTemplate,
                                    encoding: Encoding.UTF8))

                            .WriteTo.Logger(lc => lc
                                .Filter.ByIncludingOnly(a => a.Level == LogEventLevel.Warning)
                                .WriteTo.File("Logs/Warn/Warn-.txt",
                                    fileSizeLimitBytes: mb,
                                    rollOnFileSizeLimit: true,
                                    rollingInterval: RollingInterval.Day,
                                    outputTemplate: SerilogOutputTemplate,
                                    encoding: Encoding.UTF8))

                            .WriteTo.Logger(lc => lc
                                .Filter.ByIncludingOnly(a => a.Level == LogEventLevel.Error || a.Level == LogEventLevel.Fatal)
                                .WriteTo.File("Logs/Error/Error-.txt",
                                    fileSizeLimitBytes: mb,
                                    rollOnFileSizeLimit: true,
                                    rollingInterval: RollingInterval.Day,
                                    outputTemplate: SerilogOutputTemplate,
                                    encoding: Encoding.UTF8))

            //.WriteTo.Elasticsearch(
            //    new ElasticsearchSinkOptions(new Uri(configuration["ElasticSearch:Url"]))
            //    {
            //        AutoRegisterTemplate = true,
            //        AutoRegisterTemplateVersion = AutoRegisterTemplateVersion.ESv6,
            //        IndexFormat = "MyProjectName-log-{0:yyyy.MM}"
            //    })
            .CreateLogger();
    }
}