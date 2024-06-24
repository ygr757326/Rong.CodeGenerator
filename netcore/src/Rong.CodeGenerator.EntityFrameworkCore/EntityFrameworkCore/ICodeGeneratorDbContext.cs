using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace Rong.CodeGenerator.EntityFrameworkCore;

[ConnectionStringName(CodeGeneratorDbProperties.ConnectionStringName)]
public interface ICodeGeneratorDbContext : IEfCoreDbContext
{
    /* Add DbSet for each Aggregate Root here. Example:
     * DbSet<Question> Questions { get; }
     */
}
