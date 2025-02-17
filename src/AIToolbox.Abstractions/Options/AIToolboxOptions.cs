using AIToolbox.Options.Connectors;
using AIToolbox.Options.SemanticKernel;

namespace AIToolbox.Options;

public sealed class AIToolboxOptions
{
    public GlobalConnectorOptions? GlobalConnectors { get; set; }
    public SemanticKernelOptions? SemanticKernel { get; set; }
}
