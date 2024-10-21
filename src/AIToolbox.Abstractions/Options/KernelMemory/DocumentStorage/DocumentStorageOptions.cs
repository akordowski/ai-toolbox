namespace AIToolbox.Options.KernelMemory;

public sealed class DocumentStorageOptions
{
    public AWSS3Options? AWSS3 { get; set; }
    public AzureBlobsOptions? AzureBlobs { get; set; }
    public MongoDbAtlasOptions? MongoDbAtlas { get; set; }
    public SimpleFileStorageOptions? SimpleFileStorage { get; set; }
}
