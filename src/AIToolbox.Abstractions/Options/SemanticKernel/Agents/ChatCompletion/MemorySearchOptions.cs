namespace AIToolbox.Options.SemanticKernel;

public sealed class MemorySearchOptions
{
    public int Limit { get; set; } = 1;
    public double MinRelevanceScore { get; set; } = 0.7;
}
