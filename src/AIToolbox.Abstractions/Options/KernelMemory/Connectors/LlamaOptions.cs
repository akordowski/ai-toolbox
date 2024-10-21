namespace AIToolbox.Options.KernelMemory;

public sealed class LlamaOptions
{
    public string ModelPath { get; set; } = default!;
    public uint MaxTokenTotal { get; set; } = 4096;
    public int? GpuLayerCount { get; set; }
    public uint? Seed { get; set; } = 1337;
}
