namespace AIToolbox.Options.KernelMemory;

public sealed class OrchestrationOptions
{
    public OrchestrationType OrchestrationType { get; set; }
    public AzureQueuesOptions? AzureQueues { get; set; }
    public RabbitMqOptions? RabbitMq { get; set; }
    public SimpleQueuesOptions? SimpleQueues { get; set; }
}
