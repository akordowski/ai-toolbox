using AIToolbox.Options.SemanticKernel;
using Microsoft.Extensions.DependencyInjection;

namespace AIToolbox.DependencyInjection;

internal sealed class SemanticKernelBuilder : ISemanticKernelBuilder
{
    private readonly SemanticKernelOptions _options;
    private readonly IServiceCollection _services;

    public SemanticKernelBuilder(SemanticKernelOptions options, IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(options, nameof(options));
        ArgumentNullException.ThrowIfNull(services, nameof(services));

        _options = options;
        _services = services;
    }
}
