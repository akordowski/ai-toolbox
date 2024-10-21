namespace AIToolbox.Options.KernelMemory;

public sealed class AzureBlobsOptions
{
    public string ConnectionString { get; set; } = default!;
    public string Account { get; set; } = default!;
    public string AccountKey { get; set; } = default!;
    public string EndpointSuffix { get; set; } = "core.windows.net";
    public string Container { get; set; } = default!;
}
