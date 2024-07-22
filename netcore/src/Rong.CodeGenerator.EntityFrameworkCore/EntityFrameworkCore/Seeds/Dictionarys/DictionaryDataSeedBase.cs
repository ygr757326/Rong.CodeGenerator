using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Rong.CodeGenerator.App.Dictionarys;
using Rong.CodeGenerator.Enums;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.DistributedLocking;
using Volo.Abp.Guids;
using Volo.Abp.MultiTenancy;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using Namotion.Reflection;
using System.Xml.Linq;

namespace Rong.CodeGenerator.EntityFrameworkCore.Seeds.Dictionarys
{
    /// <summary>
    /// 字典数据种子基类
    /// </summary>
    public abstract class DictionaryDataSeedBase : IDataSeedContributor, ITransientDependency
    {
        /// <summary>
        /// 字典类型
        /// </summary>
        protected abstract DictionaryTypeEnum DictionaryType { get; }

        public IAbpLazyServiceProvider LazyServiceProvider { get; set; }
        private IDictionaryRepository DictionaryRepository => LazyServiceProvider.LazyGetRequiredService<IDictionaryRepository>();
        protected IGuidGenerator GuidGenerator => LazyServiceProvider.LazyGetRequiredService<IGuidGenerator>();
        protected ICurrentTenant CurrentTenant => LazyServiceProvider.LazyGetRequiredService<ICurrentTenant>();
        protected IAbpDistributedLock AbpDistributedLock => LazyServiceProvider.LazyGetRequiredService<IAbpDistributedLock>();
        /// <summary>
        /// 构造
        /// </summary>
        protected DictionaryDataSeedBase()
        {
        }


        /// <summary>
        /// 种子
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public abstract Task SeedAsync(DataSeedContext context);

        /// <summary>
        /// 添加
        /// </summary>
        /// <returns></returns>
        protected async Task<Dictionary?> AddByEnumValueAsync<TEnum>(TEnum enumValue, string? displayName = null, int sort = 0, string? remark = null, bool isActive = true) where TEnum : Enum
        {
            var enum1 = ConvertToEnum<TEnum>(enumValue);
            return await AddAsync(enum1.ToString("D"), GetDisplayName(enum1), displayName, sort, remark, isActive);
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <returns></returns>
        protected async Task<Dictionary?> AddByEnumCodeAsync<TEnum>(TEnum enumValue, string? displayName = null, int sort = 0, string? remark = null, bool isActive = true) where TEnum : Enum
        {
            var enum1 = ConvertToEnum<TEnum>(enumValue);
            return await AddAsync(enum1.ToString(), GetDisplayName(enum1), displayName, sort, remark, isActive);
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <returns></returns>
        protected async Task<Dictionary?> AddAsync(string value, string name, string? displayName = null, int sort = 0, string? remark = null, bool isActive = true)
        {
            string code = DictionaryType.ToString();

            await using (var handle =
                         await AbpDistributedLock.TryAcquireAsync($"DictionarySeedBase_{code}_{name}_{value}"))
            {
                if (handle == null)
                {
                    return null;
                }

                var isAny = await (await DictionaryRepository.GetQueryableAsync()).IgnoreQueryFilters()
                    .FirstOrDefaultAsync(a => a.Code == code && a.Value == value);
                if (isAny != null)
                {
                    return isAny;
                }

                var entity = new Dictionary(GuidGenerator.Create())
                {
                    Code = code,
                    Value = value,
                    Name = name,
                    Sort = sort,
                    Remark = remark,
                    IsActive = isActive,
                    DisplayName= string.IsNullOrWhiteSpace(displayName) ? name : displayName
            };


                await DictionaryRepository.InsertAsync(entity);

                return entity;
            }
        }

        /// <summary>
        /// 通过枚举字段获取特性 Display.Name
        ///  <para>若无 Display.Name，则返回 field.Name</para>
        /// </summary>
        /// <typeparam name="T">枚举字段</typeparam>
        /// <returns></returns>
        private string? GetDisplayName<T>(T field) where T : Enum
        {
            if (field == null)
            {
                return null;
            }
            var myField = typeof(T).GetField(field.ToString());

            var displayName = myField.GetCustomAttribute<DisplayAttribute>()?.Name;
            if (string.IsNullOrWhiteSpace(displayName))
            {
                displayName = myField.Name;
            }
            if (string.IsNullOrWhiteSpace(displayName))
            {
                displayName = myField.GetXmlDocsSummary();
            }
            return displayName;

        }

        /// <summary>
        /// 通过枚举值或字段获取枚举
        /// </summary>
        /// <typeparam name="T">枚举字段</typeparam>
        /// <returns></returns>
        private T ConvertToEnum<T>(object value) where T : Enum
        {
            var isOk = Enum.TryParse(typeof(T), value?.ToString(), true, out object e);
            if (isOk)
            {
                return (T)e;
            }

            return default;
        }
    }
}
