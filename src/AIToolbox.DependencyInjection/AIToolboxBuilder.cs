using AIToolbox.Options;
using Microsoft.Extensions.DependencyInjection;

namespace AIToolbox.DependencyInjection;

internal sealed class AIToolboxBuilder : IAIToolboxBuilder
{
    /// <inheritdoc />
    public AIToolboxOptions Options { get; }

    /// <inheritdoc />
    public IServiceCollection Services { get; }

    public AIToolboxBuilder(AIToolboxOptions options, IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(options, nameof(options));
        ArgumentNullException.ThrowIfNull(services, nameof(services));

        Options = options;
        Services = services;
    }
}
