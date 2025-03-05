using AIToolbox.Options.SemanticKernel;
using Microsoft.Extensions.DependencyInjection;

namespace AIToolbox.DependencyInjection;

public interface IMemoryBuilder
{
    /// <summary>
    /// Gets the <see cref="MemoryOptions"/>.
    /// </summary>
    MemoryOptions Options { get; }

    /// <summary>
    /// Gets the <see cref="IServiceCollection"/> where services are configured.
    /// </summary>
    IServiceCollection Services { get; }
}
