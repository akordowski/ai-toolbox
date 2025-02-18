using AIToolbox.Options;
using Microsoft.Extensions.DependencyInjection;

namespace AIToolbox;

public interface IAIToolboxBuilder
{
    /// <summary>
    /// Gets the <see cref="AIToolboxOptions"/>.
    /// </summary>
    AIToolboxOptions Options { get; }

    /// <summary>
    /// Gets the <see cref="IServiceCollection"/> where AIToolbox services are configured.
    /// </summary>
    IServiceCollection Services { get; }
}
