using AIToolbox.Options.SemanticKernel;
using Microsoft.Extensions.DependencyInjection;

namespace AIToolbox.DependencyInjection.SemanticKernel;

public abstract class KernelBuilderExtensionsTestsBase
{
    protected KernelOptions Options { get; } = new();
    protected ServiceCollection Services { get; } = [];
    protected IKernelBuilder Builder { get; }

    protected KernelBuilderExtensionsTestsBase()
    {
        Builder = new KernelBuilder(Options, Services);
    }
}
