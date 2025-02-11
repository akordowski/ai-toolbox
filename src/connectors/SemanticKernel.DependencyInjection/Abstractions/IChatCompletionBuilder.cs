using AIToolbox.Options.DataStorage;

namespace AIToolbox.DependencyInjection;

public interface IChatCompletionBuilder
{
    IChatCompletionBuilder WithSemanticTextMemoryRetriever();
    IChatCompletionBuilder WithSimpleDataStorage(SimpleDataStorageOptions? options = null);
    IChatCompletionBuilder WithSimpleDataStorage(Action<SimpleDataStorageOptions> optionsAction);
}
