using AIToolbox.Options.SemanticKernel;

namespace AIToolbox.DependencyInjection;

public interface IMemoryServiceBuilder : IServiceBuilder<MemoryOptions>, IAddAgents
{
    IMemoryServiceBuilder IncludeSimpleMemoryStore(SimpleMemoryStoreOptions? options = null);
    IMemoryServiceBuilder IncludeSimpleMemoryStore(Action<SimpleMemoryStoreOptions> optionsAction);
}
