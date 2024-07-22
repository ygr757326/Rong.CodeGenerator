using Volo.Abp.TextTemplating;
using Volo.Abp.TextTemplating.Razor;

namespace Rong.Volo.Abp.CodeGenerator.TemplateDefinitionProviders
{
    /// <summary>
    /// 字典种子数据模板定义
    /// </summary>
    public class RongVoloAbpCodeGeneratorDictionaryDataSeedTemplateDefinitionProvider : TemplateDefinitionProvider
    {
        public const string TemplateName = "DictionaryDataSeed";
        public const string DictionaryDataSeedBase = "DictionaryDataSeedBase";

        public override void Define(ITemplateDefinitionContext context)
        {
            context.Add(
                   new TemplateDefinition(TemplateName) //模板名称
                       .WithRazorEngine()
                       .WithVirtualFilePath(
                          $"/Templates/DictionaryDataSeed/Dictionary_xxx_DataSeed.cshtml", //模板路径，属性窗口中将其标记为"嵌入式资源"
                           isInlineLocalized: true
                       )
                       .WithProperty("path", "../$namespace.$nameSpaceLayerName/$saveFolderPath/Dictionarys")
                       .WithProperty("name", "Dictionary_xxx_DataSeed.cs")
               );
            context.Add(
                new TemplateDefinition(DictionaryDataSeedBase) //模板名称
                    .WithRazorEngine()
                    .WithVirtualFilePath(
                        $"/Templates/DictionaryDataSeed/DictionaryDataSeedBase.cshtml", //模板路径，属性窗口中将其标记为"嵌入式资源"
                        isInlineLocalized: true
                    )
                    .WithProperty("path", "../$namespace.$nameSpaceLayerName/$saveFolderPath/Dictionarys")
                    .WithProperty("name", "DictionaryDataSeedBase.cs")
            );

        }
    }
}