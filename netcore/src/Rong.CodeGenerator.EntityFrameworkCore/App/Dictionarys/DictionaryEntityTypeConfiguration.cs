using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Rong.CodeGenerator.App.Dictionarys
{
    /// <summary>
    /// Dictionary - 字典管理 - 实体配置
    /// </summary>
    public class DictionaryEntityTypeConfiguration : IEntityTypeConfiguration<Dictionary>
    {
        public void Configure(EntityTypeBuilder<Dictionary> builder)
        {
            builder.HasIndex(a => new { a.Code, a.Value });
        }
    }
}
