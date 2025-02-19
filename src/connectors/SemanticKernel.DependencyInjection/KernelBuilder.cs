using AIToolbox.Options.SemanticKernel;
using Microsoft.Extensions.DependencyInjection;

namespace AIToolbox.DependencyInjection;

internal sealed class KernelBuilder : IKernelBuilder
{
    /// <inheritdoc />
    public KernelOptions Options { get; }

    /// <inheritdoc />
    public IServiceCollection Services { get; }

    public KernelBuilder(KernelOptions options, IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(options, nameof(options));
        ArgumentNullException.ThrowIfNull(services, nameof(services));

        Options = options;
        Services = services;

        services.AddSingleton(options);
    }
}
