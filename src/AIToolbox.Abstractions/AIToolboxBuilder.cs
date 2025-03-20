using AIToolbox.Options;
using Microsoft.Extensions.DependencyInjection;

namespace AIToolbox;

public sealed class AIToolboxBuilder
{
    internal AIToolboxOptions Options { get; }

    internal IServiceCollection Services { get; }

    public AIToolboxBuilder(AIToolboxOptions options, IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(options, nameof(options));
        ArgumentNullException.ThrowIfNull(services, nameof(services));

        Options = options;
        Services = services;
    }
}
