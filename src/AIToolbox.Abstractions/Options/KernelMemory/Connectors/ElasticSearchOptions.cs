namespace AIToolbox.Options.KernelMemory;

public sealed class ElasticSearchOptions
{
    public string CertificateFingerPrint { get; set; } = default!;
    public string Endpoint { get; set; } = default!;
    public string UserName { get; set; } = default!;
    public string Password { get; set; } = default!;
    public string IndexPrefix { get; set; } = default!;
    public int? ShardCount { get; set; } = 1;
    public int? ReplicaCount { get; set; } = 0;
}
