namespace AIToolbox.Options.SemanticKernel;

public sealed class DuckDBMemoryStoreOptions
{
    public string Filename { get; set; } = default!;
    public int? VectorSize { get; set; } = default!;
}
