using AIToolbox.Options.Agents;
using AIToolbox.Options.Connectors;
using AIToolbox.Options.KernelMemory;
using AIToolbox.Options.SemanticKernel;

namespace AIToolbox.Options;

public sealed class AIToolboxOptions
{
    public ConnectorOptions? Connectors { get; set; }
    public KernelOptions? Kernel { get; set; }
    public MemoryOptions? Memory { get; set; }
    public KernelMemoryOptions? KernelMemory { get; set; }
    public AgentOptions? Agents { get; set; }
}
