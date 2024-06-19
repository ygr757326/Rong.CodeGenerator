namespace Rong.CodeGenerator;

public static class CodeGeneratorDbProperties
{
    public static string DbTablePrefix { get; set; } = "CodeGenerator";

    public static string? DbSchema { get; set; } = null;

    public const string ConnectionStringName = "CodeGenerator";
}
