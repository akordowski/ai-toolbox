using AIToolbox.Options.Connectors;
using AIToolbox.Options.SemanticKernel;

namespace AIToolbox.Options;

public sealed class AIToolboxOptions
{
    public ConnectorOptions? Connectors { get; set; }
    public SemanticKernelOptions? SemanticKernel { get; set; }
}
