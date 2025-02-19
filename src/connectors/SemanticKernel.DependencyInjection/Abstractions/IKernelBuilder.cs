using AIToolbox.Options.SemanticKernel;
using Microsoft.Extensions.DependencyInjection;

namespace AIToolbox.DependencyInjection;

public interface IKernelBuilder
{
    /// <summary>
    /// Gets the <see cref="KernelOptions"/>.
    /// </summary>
    KernelOptions Options { get; }

    /// <summary>
    /// Gets the <see cref="IServiceCollection"/> where services are configured.
    /// </summary>
    IServiceCollection Services { get; }
}
