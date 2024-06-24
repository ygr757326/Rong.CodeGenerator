using IObjectMapper = Volo.Abp.ObjectMapping.IObjectMapper;
using Volo.Abp.Data;
using Volo.Abp.Domain.Services;
using Volo.Abp.Json;
using Volo.Abp.Settings;
using Volo.Abp.Uow;
using Volo.Abp.Users;
using Volo.Abp.DistributedLocking;

namespace Rong.CodeGenerator{
    /// <summary>
    /// 领域服务基类
    /// </summary>
    public abstract class CodeGeneratorDomainServiceBase : DomainService
    {
        protected IAbpDistributedLock AbpDistributedLock => LazyServiceProvider.LazyGetRequiredService<IAbpDistributedLock>();
        protected ISettingProvider SettingProvider => LazyServiceProvider.LazyGetRequiredService<ISettingProvider>();
        protected IUnitOfWorkManager UnitOfWorkManager => LazyServiceProvider.LazyGetRequiredService<IUnitOfWorkManager>();
        protected IUnitOfWork? CurrentUnitOfWork => UnitOfWorkManager?.Current;
        protected IObjectMapper ObjectMapper => LazyServiceProvider.LazyGetRequiredService<IObjectMapper>();
        protected ICurrentUser CurrentUser => LazyServiceProvider.LazyGetRequiredService<ICurrentUser>();
        protected IJsonSerializer JsonSerializer => LazyServiceProvider.LazyGetRequiredService<IJsonSerializer>();
        protected IDataFilter DataFilter => LazyServiceProvider.LazyGetRequiredService<IDataFilter>();

        /// <summary>
        /// 构造
        /// </summary>
        protected CodeGeneratorDomainServiceBase() : base()
        {
        }
    }
}