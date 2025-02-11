using AIToolbox.Options.SemanticKernel;

namespace AIToolbox.DependencyInjection;

public interface IAddMemory : IAddChatCompletion
{
    IAddMemory AddMemory(MemoryOptions? options = null);
    IAddMemory AddMemory(Action<MemoryOptions> optionsAction);
    IAddMemory AddMemory(Action<IMemoryBuilder> builderAction, MemoryOptions? options = null);
    IAddMemory AddMemory(Action<IMemoryBuilder, MemoryOptions> action);
}
