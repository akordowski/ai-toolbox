using AIToolbox.Options.SemanticKernel;

namespace AIToolbox.DependencyInjection;

public interface IMemoryBuilder
{
    IMemoryBuilder WithSimpleMemoryStore(SimpleMemoryStoreOptions? options = null);
    IMemoryBuilder WithSimpleMemoryStore(Action<SimpleMemoryStoreOptions> optionsAction);
}
