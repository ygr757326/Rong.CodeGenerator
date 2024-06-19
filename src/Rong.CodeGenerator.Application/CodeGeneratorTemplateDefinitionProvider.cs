using Volo.Abp.TextTemplating;
using Volo.Abp.TextTemplating.Razor;

namespace Rong.Volo.Abp.CodeGenerator
{
    /// <summary>
    /// 模板定义
    /// </summary>
    public class CodeGeneratorTemplateDefinitionProvider : TemplateDefinitionProvider
    {
        public override void Define(ITemplateDefinitionContext context)
        {
            //应用层
            string[] appServices = new[] { CodeGeneratorTemplateNames.AppService_xxxAppService, CodeGeneratorTemplateNames.AppService_xxxMapper };
            foreach (var item in appServices)
            {
                string name = item.Split('_')[1];
                context.Add(
                    new TemplateDefinition(item) //模板名称
                        .WithRazorEngine()
                        .WithVirtualFilePath(
                           $"/Templates/Application/{name}.cshtml", //模板路径，属性窗口中将其标记为"嵌入式资源"
                            isInlineLocalized: true
                        )
                        .WithProperty("path", "../$namespace.Application/App/xxxs")
                        .WithProperty("name", $"{name}.cs")
                );
            }
            //应用层合同层
            string[] applicationContracts = new[] { CodeGeneratorTemplateNames.ApplicationContracts_IxxxAppService, CodeGeneratorTemplateNames.ApplicationContracts_xxxPermissions };
            foreach (var item in applicationContracts)
            {
                string name = item.Split('_')[1];
                context.Add(
                    new TemplateDefinition(item) //模板名称
                        .WithRazorEngine()
                        .WithVirtualFilePath(
                            $"/Templates/Application.Contracts/{name}.cshtml", //模板路径，属性窗口中将其标记为"嵌入式资源"
                            isInlineLocalized: true
                        )
                        .WithProperty("path", "../$namespace.Application.Contracts/App/xxxs")
                        .WithProperty("name", $"{name}.cs")
                );
            }
            //应用层合同层Dto
            string[] applicationContractDtos = new[] { CodeGeneratorTemplateNames.ApplicationContractsDto_xxxBaseOutput, CodeGeneratorTemplateNames.ApplicationContractsDto_xxxCreateInput, CodeGeneratorTemplateNames.ApplicationContractsDto_xxxCreateOrUpdateInputBase, CodeGeneratorTemplateNames.ApplicationContractsDto_xxxDetailOutput, CodeGeneratorTemplateNames.ApplicationContractsDto_xxxDropDownOutput, CodeGeneratorTemplateNames.ApplicationContractsDto_xxxDropDownSearchInput, CodeGeneratorTemplateNames.ApplicationContractsDto_xxxPageOutput, CodeGeneratorTemplateNames.ApplicationContractsDto_xxxPageSearchInput, CodeGeneratorTemplateNames.ApplicationContractsDto_xxxUpdateInput };
            foreach (var item in applicationContractDtos)
            {
                string name = item.Split('_')[1];
                context.Add(
                    new TemplateDefinition(item) //模板名称
                        .WithRazorEngine()
                        .WithVirtualFilePath(
                            $"/Templates/Application.Contracts/Dto/{name}.cshtml", //模板路径，属性窗口中将其标记为"嵌入式资源"
                            isInlineLocalized: true
                        )
                        .WithProperty("path", "../$namespace.Application.Contracts/App/xxxs/Dto")
                        .WithProperty("name", $"{name}.cs")
                );
            }

            //领域层
            string[] domains = new[] { CodeGeneratorTemplateNames.Domain_xxx, CodeGeneratorTemplateNames.Domain_IxxxRepository, CodeGeneratorTemplateNames.Domain_DomainServiceBase };
            foreach (var item in domains)
            {
                string name = item.Split('_')[1];

                var definition = new TemplateDefinition(item) //模板名称
                    .WithRazorEngine()
                    .WithVirtualFilePath(
                        $"/Templates/Domain/{name}.cshtml", //模板路径，属性窗口中将其标记为"嵌入式资源"
                        isInlineLocalized: true
                    );

                if (item == CodeGeneratorTemplateNames.Domain_DomainServiceBase)
                {
                    definition.WithProperty("path", "../$namespace.Domain");
                    definition.WithProperty("name", $"$project{name}.cs");
                }
                else
                {

                    definition.WithProperty("path", "../$namespace.Domain/App/xxxs");
                    definition.WithProperty("name", $"{name}.cs");
                }

                context.Add(definition);
            }

            //领域层DomainService
            string[] domainServices = new[] { CodeGeneratorTemplateNames.DomainService_xxxManager, CodeGeneratorTemplateNames.DomainService_xxxMapper };
            foreach (var item in domainServices)
            {
                string name = item.Split('_')[1];
                context.Add(
                    new TemplateDefinition(item) //模板名称
                        .WithRazorEngine()
                        .WithVirtualFilePath(
                            $"/Templates/Domain/DomainService/{name}.cshtml", //模板路径，属性窗口中将其标记为"嵌入式资源"
                            isInlineLocalized: true
                        )
                        .WithProperty("path", "../$namespace.Domain/App/xxxs/DomainService")
                        .WithProperty("name", $"{name}.cs")
                );
            }
            //领域层公共层
            string[] domainShareds = new[] { CodeGeneratorTemplateNames.DomainShared_xxxConsts };
            foreach (var item in domainShareds)
            {
                string name = item.Split('_')[1];
                context.Add(
                    new TemplateDefinition(item) //模板名称
                        .WithRazorEngine()
                        .WithVirtualFilePath(
                            $"/Templates/Domain.Shared/{name}.cshtml", //模板路径，属性窗口中将其标记为"嵌入式资源"
                            isInlineLocalized: true
                        )
                        .WithProperty("path", "../$namespace.Domain.Shared/App/xxxs")
                        .WithProperty("name", $"{name}.cs")
                );
            }

            //领域层公共层Eto
            string[] domainSharedEtos = new[] { CodeGeneratorTemplateNames.DomainShared_xxxEto };
            foreach (var item in domainSharedEtos)
            {
                string name = item.Split('_')[1];
                context.Add(
                    new TemplateDefinition(item) //模板名称
                        .WithRazorEngine()
                        .WithVirtualFilePath(
                            $"/Templates/Domain.Shared/Eto/{name}.cshtml", //模板路径，属性窗口中将其标记为"嵌入式资源"
                            isInlineLocalized: true
                        )
                        .WithProperty("path", "../$namespace.Domain.Shared/App/xxxs/Eto")
                        .WithProperty("name", $"{name}.cs")
                );
            }

            //ef core层
            string[] entityFrameworkCores = new[] { CodeGeneratorTemplateNames.EntityFrameworkCore_xxxEntityTypeConfiguration, CodeGeneratorTemplateNames.EntityFrameworkCore_xxxRepository };
            foreach (var item in entityFrameworkCores)
            {
                string name = item.Split('_')[1];
                context.Add(
                    new TemplateDefinition(item) //模板名称
                        .WithRazorEngine()
                        .WithVirtualFilePath(
                            $"/Templates/EntityFrameworkCore/{name}.cshtml", //模板路径，属性窗口中将其标记为"嵌入式资源"
                            isInlineLocalized: true
                        )
                        .WithProperty("path", "../$namespace.EntityFrameworkCore/App/xxxs")
                        .WithProperty("name", $"{name}.cs")
                );
            }
            //api层
            string[] httpApis = new[] { CodeGeneratorTemplateNames.HttpApi_xxxController, CodeGeneratorTemplateNames.HttpApi_ControllerBase };
            foreach (var item in httpApis)
            {
                string name = item.Split('_')[1];

                var definition = new TemplateDefinition(item) //模板名称
                    .WithRazorEngine()
                    .WithVirtualFilePath(
                        $"/Templates/HttpApi/{name}.cshtml", //模板路径，属性窗口中将其标记为"嵌入式资源"
                        isInlineLocalized: true
                    );

                if (item == CodeGeneratorTemplateNames.HttpApi_ControllerBase)
                {
                    definition.WithProperty("path", "../$namespace.HttpApi");
                    definition.WithProperty("name", $"$project{name}.cs");
                }
                else
                {
                    definition.WithProperty("path", "../$namespace.HttpApi/App/xxxs");
                    definition.WithProperty("name", $"{name}.cs");
                }

                context.Add(definition);

            }
        }
    }
}