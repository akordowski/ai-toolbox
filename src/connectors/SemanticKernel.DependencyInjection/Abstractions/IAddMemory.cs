using AIToolbox.Options.SemanticKernel;

namespace AIToolbox.DependencyInjection;

public interface IAddMemory
{
    /// <summary>
    /// Adds the Memory to Semantic Kernel.
    /// </summary>
    /// <param name="builderAction">>A delegate that is used to configure an <see cref="IMemoryBuilder"/>.</param>
    /// <returns>The instance of <see cref="IAddKernel"/>.</returns>
    IAddMemory AddMemory(Action<IMemoryBuilder> builderAction);

    /// <summary>
    /// Adds the Memory to Semantic Kernel.
    /// </summary>
    /// <param name="builderAction">>A delegate that is used to configure an <see cref="IMemoryBuilder"/>.</param>
    /// <param name="optionsAction">A delegate that is used to configure the <see cref="MemoryOptions"/>.</param>
    /// <returns>The instance of <see cref="IAddMemory"/>.</returns>
    IAddMemory AddMemory(Action<IMemoryBuilder> builderAction, Action<MemoryOptions> optionsAction);
}
