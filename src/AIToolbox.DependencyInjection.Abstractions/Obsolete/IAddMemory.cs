using AIToolbox.Options.SemanticKernel;

namespace AIToolbox.DependencyInjection;

public interface IAddMemory
{
    IMemoryServiceBuilder AddMemory(MemoryOptions? options = null);
    IMemoryServiceBuilder AddMemory(Action<MemoryOptions> optionsAction);
}
