using AIToolbox.Options.SemanticKernel;

namespace AIToolbox.DependencyInjection;

public interface IAddKernel
{
    /// <summary>
    /// Adds the Kernel to Semantic Kernel.
    /// </summary>
    /// <returns>The instance of <see cref="IAddKernel"/>.</returns>
    IAddKernel AddKernel();

    /// <summary>
    /// Adds the Kernel to Semantic Kernel.
    /// </summary>
    /// <param name="builderAction">>A delegate that is used to configure an <see cref="IKernelBuilder"/>.</param>
    /// <returns>The instance of <see cref="IAddKernel"/>.</returns>
    IAddKernel AddKernel(Action<IKernelBuilder> builderAction);

    /// <summary>
    /// Adds the Kernel to Semantic Kernel.
    /// </summary>
    /// <param name="builderAction">>A delegate that is used to configure an <see cref="IKernelBuilder"/>.</param>
    /// <param name="optionsAction">A delegate that is used to configure the <see cref="KernelOptions"/>.</param>
    /// <returns>The instance of <see cref="IAddKernel"/>.</returns>
    IAddKernel AddKernel(Action<IKernelBuilder> builderAction, Action<KernelOptions> optionsAction);
}
