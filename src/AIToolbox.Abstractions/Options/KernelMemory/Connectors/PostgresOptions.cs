namespace AIToolbox.Options.KernelMemory;

public sealed class PostgresOptions
{
    public string ConnectionString { get; set; } = default!;
    public string Schema { get; set; } = "public";
    public string TableNamePrefix { get; set; } = "km-";
    public Dictionary<string, string> Columns { get; set; } = new();
    public List<string> CreateTableSql { get; set; } = [];
}
