using Rong.CodeGenerator.EntityFrameworkCore;
using System;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Rong.CodeGenerator.App.Dictionarys
{
    /// <summary>
    /// Dictionary - 字典管理 - 自定义仓储实现
    /// </summary>
    public class DictionaryRepository : EfCoreRepository<CodeGeneratorDbContext, Dictionary, Guid>, IDictionaryRepository
    {
        /// <summary>
        /// 构造
        /// </summary>
        public DictionaryRepository(IDbContextProvider<CodeGeneratorDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }
        
    }
}
