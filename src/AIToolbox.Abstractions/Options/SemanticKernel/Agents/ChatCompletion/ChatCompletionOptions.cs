using AIToolbox.Options.DataStorage;

namespace AIToolbox.Options.SemanticKernel;

public sealed class ChatCompletionOptions
{
    public DataStorageOptions? DataStorage { get; set; }
    public ChatHistoryOptions? ChatHistory { get; set; }
    public MemorySearchOptions? MemorySearch { get; set; }
    public PromptExecutionOptions? PromptExecution { get; set; }
    public ClassOptions? ChatHistoryRetriever { get; set; }
    public ClassOptions? PromptExecutionSettingsRetriever { get; set; }
}
