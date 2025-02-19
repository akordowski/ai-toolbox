using Microsoft.SemanticKernel;

namespace AIToolbox.SemanticKernel;

public interface IKernelProvider
{
    /// <summary>
    /// Gets the Kernel of Semantic Kernel.
    /// </summary>
    /// <returns>An instance of <see cref="Kernel"/>.</returns>
    Kernel GetKernel();
}
