namespace AIToolbox.Options.KernelMemory;

public sealed class MongoDbAtlasOptions
{
    public string ConnectionString { get; set; } = default!;
    public string DatabaseName { get; set; } = default!;
    public bool UseSingleCollectionForVectorSearch { get; set; }
}
