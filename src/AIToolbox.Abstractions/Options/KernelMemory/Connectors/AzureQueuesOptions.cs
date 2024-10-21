namespace AIToolbox.Options.KernelMemory;

public sealed class AzureQueuesOptions
{
    public AzureQueuesAuthType AuthType { get; set; }
    public string ConnectionString { get; set; } = string.Empty;
    public string Account { get; set; } = string.Empty;
    public string AccountKey { get; set; } = string.Empty;
    public string EndpointSuffix { get; set; } = "core.windows.net";
    public int PollDelayMsecs { get; set; } = 100;
    public int FetchBatchSize { get; set; } = 3;
    public int FetchLockSeconds { get; set; } = 300;
    public int MaxRetriesBeforePoisonQueue { get; set; } = 20;
    public string PoisonQueueSuffix { get; set; } = "-poison";
}
