using AIToolbox.Options;
using Microsoft.Extensions.DependencyInjection;

namespace AIToolbox.DependencyInjection;

internal sealed class BuilderFactory : IBuilderFactory
{
    /// <inheritdoc />
    public AIToolboxOptions Options { get; }

    /// <inheritdoc />
    public IServiceCollection Services { get; }

    public BuilderFactory(AIToolboxOptions options, IServiceCollection services)
    {
        Verify.ThrowIfNull(options, nameof(options));
        Verify.ThrowIfNull(services, nameof(services));

        Options = options;
        Services = services;
    }
}
