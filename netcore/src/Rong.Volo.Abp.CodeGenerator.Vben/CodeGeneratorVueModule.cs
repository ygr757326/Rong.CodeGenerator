using Microsoft.CodeAnalysis;
//using Microsoft.Extensions.WebEncoders;
using Volo.Abp.Modularity;
using Volo.Abp.TextTemplating.Razor;
using Volo.Abp.VirtualFileSystem;

namespace Rong.Volo.Abp.CodeGenerator.Vue
{
    /// <summary>
    /// 模板模块
    /// </summary>
    [DependsOn(
        //Razor模板
        typeof(AbpTextTemplatingRazorModule)
    )]
    public class CodeGeneratorVueModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            //配置Razor页面的文本编码（当前用途：代码生成器编译cshtml时，汉字会被编码）
            //Configure<WebEncoderOptions>(options =>
            //{
            //    options.TextEncoderSettings = new TextEncoderSettings(UnicodeRanges.All);
            //});

            Configure<AbpRazorTemplateCSharpCompilerOptions>(options =>
            {
                options.References.Add(
                    MetadataReference.CreateFromFile(typeof(CodeGeneratorVueModule).Assembly.Location));
            });

            Configure<AbpVirtualFileSystemOptions>(options =>
            {
                options.FileSets.AddEmbedded<CodeGeneratorVueModule>("Rong.Volo.Abp.CodeGenerator.Vue");
            });
        }
    }
}