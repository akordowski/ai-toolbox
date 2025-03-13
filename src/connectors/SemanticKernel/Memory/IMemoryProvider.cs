using Microsoft.SemanticKernel.Memory;

namespace AIToolbox.SemanticKernel.Memory;

public interface IMemoryProvider
{
    /// <summary>
    /// Gets a <see cref="ISemanticTextMemory"/>.
    /// </summary>
    /// <returns>An instance of <see cref="ISemanticTextMemory"/>.</returns>
    ISemanticTextMemory GetMemory();
}
