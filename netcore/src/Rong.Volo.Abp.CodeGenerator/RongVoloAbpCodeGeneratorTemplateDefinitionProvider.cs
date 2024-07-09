using Volo.Abp.TextTemplating;
using Volo.Abp.TextTemplating.Razor;

namespace Rong.Volo.Abp.CodeGenerator
{
    /// <summary>
    /// 模板定义
    /// </summary>
    public class RongVoloAbpCodeGeneratorTemplateDefinitionProvider : TemplateDefinitionProvider
    {
        public override void Define(ITemplateDefinitionContext context)
        {
            //应用层
            string[] appServices = new[] {
                RongVoloAbpCodeGeneratorTemplateNames.AppService_xxxAppService,
                RongVoloAbpCodeGeneratorTemplateNames.AppService_xxxMapper
            };
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
                        .WithProperty("path", "../$namespace.Application/$folderName/xxxs")
                        .WithProperty("name", $"{name}.cs")
                );
            }
            //应用层合同层
            string[] applicationContracts = new[]
            {
                RongVoloAbpCodeGeneratorTemplateNames.ApplicationContracts_IxxxAppService,
                RongVoloAbpCodeGeneratorTemplateNames.ApplicationContracts_xxxPermissions
            };
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
                        .WithProperty("path", "../$namespace.Application.Contracts/$folderName/xxxs")
                        .WithProperty("name", $"{name}.cs")
                );
            }
            //应用层合同层Dto
            string[] applicationContractDtos = new[]
            {
                RongVoloAbpCodeGeneratorTemplateNames.ApplicationContractsDto_xxxBaseOutput,
                RongVoloAbpCodeGeneratorTemplateNames.ApplicationContractsDto_xxxCreateInput,
                RongVoloAbpCodeGeneratorTemplateNames.ApplicationContractsDto_xxxCreateOrUpdateInputBase,
                RongVoloAbpCodeGeneratorTemplateNames.ApplicationContractsDto_xxxDetailOutput,
                RongVoloAbpCodeGeneratorTemplateNames.ApplicationContractsDto_xxxDropDownOutput,
                RongVoloAbpCodeGeneratorTemplateNames.ApplicationContractsDto_xxxDropDownSearchInput,
                RongVoloAbpCodeGeneratorTemplateNames.ApplicationContractsDto_xxxPageOutput,
                RongVoloAbpCodeGeneratorTemplateNames.ApplicationContractsDto_xxxPageSearchInput,
                RongVoloAbpCodeGeneratorTemplateNames.ApplicationContractsDto_xxxUpdateInput
            };
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
                        .WithProperty("path", "../$namespace.Application.Contracts/$folderName/xxxs/Dto")
                        .WithProperty("name", $"{name}.cs")
                );
            }
            //应用层合同层权限
            string[] applicationContractPermissions = new[] {
                RongVoloAbpCodeGeneratorTemplateNames.ApplicationContractsPermissions_PermissionAttribute,
                RongVoloAbpCodeGeneratorTemplateNames.ApplicationContractsPermissions_PermissionConsts,
                RongVoloAbpCodeGeneratorTemplateNames.ApplicationContractsPermissions_PermissionExtensions,
                RongVoloAbpCodeGeneratorTemplateNames.ApplicationContractsPermissions_PermissionMultiTenancySideAttribute,
 };
            foreach (var item in applicationContractPermissions)
            {
                string name = item.Split('_')[1];
                context.Add(
                    new TemplateDefinition(item) //模板名称
                        .WithRazorEngine()
                        .WithVirtualFilePath(
                            $"/Templates/Application.Contracts/Permissions/{name}.cshtml", //模板路径，属性窗口中将其标记为"嵌入式资源"
                            isInlineLocalized: true
                        )
                        .WithProperty("path", "../$namespace.Application.Contracts/Permissions")
                        .WithProperty("name", $"{name}.cs")
                );
            }

            //领域层
            string[] domains = new[] {
                RongVoloAbpCodeGeneratorTemplateNames.Domain_xxx,
                RongVoloAbpCodeGeneratorTemplateNames.Domain_IxxxRepository,
                RongVoloAbpCodeGeneratorTemplateNames.Domain_DomainServiceBase
            };
            foreach (var item in domains)
            {
                string name = item.Split('_')[1];

                var definition = new TemplateDefinition(item) //模板名称
                    .WithRazorEngine()
                    .WithVirtualFilePath(
                        $"/Templates/Domain/{name}.cshtml", //模板路径，属性窗口中将其标记为"嵌入式资源"
                        isInlineLocalized: true
                    );

                if (item == RongVoloAbpCodeGeneratorTemplateNames.Domain_DomainServiceBase)
                {
                    definition.WithProperty("path", "../$namespace.Domain");
                    definition.WithProperty("name", $"$project{name}.cs");
                }
                else
                {

                    definition.WithProperty("path", "../$namespace.Domain/$folderName/xxxs");
                    definition.WithProperty("name", $"{name}.cs");
                }

                context.Add(definition);
            }

            //领域层DomainService
            string[] domainServices = new[]
            {
                RongVoloAbpCodeGeneratorTemplateNames.DomainService_xxxManager,
                RongVoloAbpCodeGeneratorTemplateNames.DomainService_xxxMapper
            };
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
                        .WithProperty("path", "../$namespace.Domain/$folderName/xxxs/DomainService")
                        .WithProperty("name", $"{name}.cs")
                );
            }
            //领域层公共层
            string[] domainShareds = new[]
            {
                RongVoloAbpCodeGeneratorTemplateNames.DomainShared_xxxConsts
            };
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
                        .WithProperty("path", "../$namespace.Domain.Shared/$folderName/xxxs")
                        .WithProperty("name", $"{name}.cs")
                );
            }

            //领域层公共层Eto
            string[] domainSharedEtos = new[] { RongVoloAbpCodeGeneratorTemplateNames.DomainShared_xxxEto };
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
                        .WithProperty("path", "../$namespace.Domain.Shared/$folderName/xxxs/Eto")
                        .WithProperty("name", $"{name}.cs")
                );
            }

            //ef core层
            string[] entityFrameworkCores = new[] { RongVoloAbpCodeGeneratorTemplateNames.EntityFrameworkCore_xxxEntityTypeConfiguration, RongVoloAbpCodeGeneratorTemplateNames.EntityFrameworkCore_xxxRepository };
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
                        .WithProperty("path", "../$namespace.EntityFrameworkCore/$folderName/xxxs")
                        .WithProperty("name", $"{name}.cs")
                );
            }
            //api层
            string[] httpApis = new[] { RongVoloAbpCodeGeneratorTemplateNames.HttpApi_xxxController, RongVoloAbpCodeGeneratorTemplateNames.HttpApi_ControllerBase };
            foreach (var item in httpApis)
            {
                string name = item.Split('_')[1];

                var definition = new TemplateDefinition(item) //模板名称
                    .WithRazorEngine()
                    .WithVirtualFilePath(
                        $"/Templates/HttpApi/{name}.cshtml", //模板路径，属性窗口中将其标记为"嵌入式资源"
                        isInlineLocalized: true
                    );

                if (item == RongVoloAbpCodeGeneratorTemplateNames.HttpApi_ControllerBase)
                {
                    definition.WithProperty("path", "../$namespace.HttpApi");
                    definition.WithProperty("name", $"$project{name}.cs");
                }
                else
                {
                    definition.WithProperty("path", "../$namespace.HttpApi/$folderName/xxxs");
                    definition.WithProperty("name", $"{name}.cs");
                }

                context.Add(definition);

            }
        }
    }
}