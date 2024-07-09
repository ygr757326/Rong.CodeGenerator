namespace Rong.Volo.Abp.CodeGenerator
{
    /// <summary>
    /// 模板名称
    /// </summary>
    public class RongVoloAbpCodeGeneratorTemplateNames
    {
        //应用层
        public const string AppService_xxxAppService = "AppService_xxxAppService";
        public const string AppService_xxxMapper = "AppService_xxxMapper";

        //应用层合同层
        public const string ApplicationContracts_IxxxAppService = "ApplicationContracts_IxxxAppService";
        public const string ApplicationContracts_xxxPermissions = "ApplicationContracts_xxxPermissions";

        //应用层合同层Dto
        public const string ApplicationContractsDto_xxxBaseOutput = "ApplicationContractsDto_xxxBaseOutput";
        public const string ApplicationContractsDto_xxxCreateInput = "ApplicationContractsDto_xxxCreateInput";
        public const string ApplicationContractsDto_xxxCreateOrUpdateInputBase = "ApplicationContractsDto_xxxCreateOrUpdateInputBase";
        public const string ApplicationContractsDto_xxxDetailOutput = "ApplicationContractsDto_xxxDetailOutput";
        public const string ApplicationContractsDto_xxxDropDownOutput = "ApplicationContractsDto_xxxDropDownOutput";
        public const string ApplicationContractsDto_xxxDropDownSearchInput = "ApplicationContractsDto_xxxDropDownSearchInput";
        public const string ApplicationContractsDto_xxxPageOutput = "ApplicationContractsDto_xxxPageOutput";
        public const string ApplicationContractsDto_xxxPageSearchInput = "ApplicationContractsDto_xxxPageSearchInput";
        public const string ApplicationContractsDto_xxxUpdateInput = "ApplicationContractsDto_xxxUpdateInput";

        //权限
        public const string ApplicationContractsPermissions_PermissionAttribute = "ApplicationContractsPermissions_PermissionAttribute";
        public const string ApplicationContractsPermissions_PermissionConsts = "ApplicationContractsPermissions_PermissionConsts";
        public const string ApplicationContractsPermissions_PermissionExtensions = "ApplicationContractsPermissions_PermissionExtensions";
        public const string ApplicationContractsPermissions_PermissionMultiTenancySideAttribute = "ApplicationContractsPermissions_PermissionMultiTenancySideAttribute";

        //领域层DomainService
        public const string Domain_xxx = "Domain_xxx";
        public const string Domain_IxxxRepository = "Domain_IxxxRepository";

        public const string DomainService_xxxManager = "DomainService_xxxManager";
        public const string DomainService_xxxMapper = "DomainService_xxxMapper";

        public const string Domain_DomainServiceBase = "Domain_DomainServiceBase";

        //领域层公共层
        public const string DomainShared_xxxConsts = "DomainShared_xxxConsts";
        public const string DomainShared_xxxEto = "DomainShared_xxxEto";
        //ef core层
        public const string EntityFrameworkCore_xxxEntityTypeConfiguration = "EntityFrameworkCore_xxxEntityTypeConfiguration";
        public const string EntityFrameworkCore_xxxRepository = "EntityFrameworkCore_xxxRepository";
        //api层
        public const string HttpApi_xxxController = "HttpApi_xxxController";
        public const string HttpApi_ControllerBase = "HttpApi_ControllerBase";
    }
}
