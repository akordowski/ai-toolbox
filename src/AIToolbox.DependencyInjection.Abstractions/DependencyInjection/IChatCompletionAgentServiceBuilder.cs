using AIToolbox.Options.DataStorage;
using AIToolbox.Options.SemanticKernel;

namespace AIToolbox.DependencyInjection;

public interface IChatCompletionAgentServiceBuilder : IServiceBuilder<ChatCompletionOptions>
{
    IChatCompletionAgentServiceBuilder WithSemanticTextMemoryRetriever();
    IChatCompletionAgentServiceBuilder WithSimpleDataStorage(SimpleDataStorageOptions? options = null);
    IChatCompletionAgentServiceBuilder WithSimpleDataStorage(Action<SimpleDataStorageOptions> optionsAction);
}
