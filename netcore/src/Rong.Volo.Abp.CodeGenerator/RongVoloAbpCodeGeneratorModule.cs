using Microsoft.CodeAnalysis;

//using Microsoft.Extensions.WebEncoders;
using Volo.Abp.Modularity;
using Volo.Abp.TextTemplating.Razor;
using Volo.Abp.VirtualFileSystem;

namespace Rong.Volo.Abp.CodeGenerator
{
    /// <summary>
    /// ģ��ģ��
    /// </summary>
    [DependsOn(
        //Razorģ��
        typeof(AbpTextTemplatingRazorModule)
    )]
    public class RongVoloAbpCodeGeneratorModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            //����Razorҳ����ı����루��ǰ��;����������������cshtmlʱ�����ֻᱻ���룩
            //Configure<WebEncoderOptions>(options =>
            //{
            //    options.TextEncoderSettings = new TextEncoderSettings(UnicodeRanges.All);
            //});

            Configure<AbpRazorTemplateCSharpCompilerOptions>(options =>
            {
                options.References.Add(
                    MetadataReference.CreateFromFile(typeof(RongVoloAbpCodeGeneratorModule).Assembly.Location));
            });


            Configure<AbpVirtualFileSystemOptions>(options =>
            {
                options.FileSets.AddEmbedded<RongVoloAbpCodeGeneratorModule>("Rong.Volo.Abp.CodeGenerator");
            });
        }
    }
}