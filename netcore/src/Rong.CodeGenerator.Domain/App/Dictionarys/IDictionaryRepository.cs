using System;
using Volo.Abp.Domain.Repositories;

namespace Rong.CodeGenerator.App.Dictionarys
{
    /// <summary>
    /// Dictionary - 字典管理 - 自定义仓储
    /// <para>如果实体没有 Id 主键（或有一个复合主键），则使用 IRepository&lt;Dictionary&gt;</para>
    /// </summary>
    public interface IDictionaryRepository : IRepository<Dictionary, Guid>
    {
        
    }
}
