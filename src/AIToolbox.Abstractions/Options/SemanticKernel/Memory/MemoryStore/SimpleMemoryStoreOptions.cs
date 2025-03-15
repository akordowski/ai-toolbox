using AIToolbox.Options.Enums;

namespace AIToolbox.Options.SemanticKernel;

public sealed class SimpleMemoryStoreOptions
{
    public StorageType StorageType { get; set; } = StorageType.Volatile;
    public string Directory { get; set; } = "tmp-sk-memory";
}
