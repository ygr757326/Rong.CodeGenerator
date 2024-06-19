﻿using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace Rong.CodeGenerator.EntityFrameworkCore;

[ConnectionStringName(CodeGeneratorDbProperties.ConnectionStringName)]
public class CodeGeneratorDbContext : AbpDbContext<CodeGeneratorDbContext>, ICodeGeneratorDbContext
{
    /* Add DbSet for each Aggregate Root here. Example:
     * public DbSet<Question> Questions { get; set; }
     */

    public CodeGeneratorDbContext(DbContextOptions<CodeGeneratorDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ConfigureCodeGenerator();
    }
}
