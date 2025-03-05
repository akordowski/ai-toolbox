using AIToolbox.Options.SemanticKernel;
using Microsoft.Extensions.DependencyInjection;

namespace AIToolbox.DependencyInjection;

internal sealed class MemoryBuilder : IMemoryBuilder
{
    /// <inheritdoc />
    public MemoryOptions Options { get; }

    /// <inheritdoc />
    public IServiceCollection Services { get; }

    public MemoryBuilder(MemoryOptions options, IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(options, nameof(options));
        ArgumentNullException.ThrowIfNull(services, nameof(services));

        Options = options;
        Services = services;

        services.AddSingleton(options);
    }
}
