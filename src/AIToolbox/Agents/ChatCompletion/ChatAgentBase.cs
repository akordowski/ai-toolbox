using AIToolbox.Options.SemanticKernel;
using AIToolbox.SemanticKernel;
using AIToolbox.SemanticKernel.ChatCompletion;
using AIToolbox.SemanticKernel.Memory;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Memory;

namespace AIToolbox.Agents.ChatCompletion;

public abstract class ChatAgentBase
{
    private readonly IChatHistoryRetriever _chatHistoryRetriever;
    private readonly IPromptExecutionSettingsRetriever? _promptExecutionSettingsRetriever;
    private readonly ISemanticTextMemoryRetriever? _semanticTextMemoryRetriever;
    private readonly ChatHistoryOptions? _chatHistoryOptions;
    private readonly MemorySearchOptions? _memorySearchOptions;
    private readonly PromptExecutionOptions? _promptExecutionOptions;
    private readonly Kernel _kernel;

    protected ChatAgentBase(
        IKernelProvider kernelProvider,
        IChatHistoryRetriever chatHistoryRetriever,
        IPromptExecutionSettingsRetriever? promptExecutionSettingsRetriever = null,
        ISemanticTextMemoryRetriever? semanticTextMemoryRetriever = null,
        ChatHistoryOptions? chatHistoryOptions = null,
        MemorySearchOptions? memorySearchOptions = null,
        PromptExecutionOptions? promptExecutionOptions = null)
    {
        _chatHistoryRetriever = chatHistoryRetriever;
        _promptExecutionSettingsRetriever = promptExecutionSettingsRetriever;
        _semanticTextMemoryRetriever = semanticTextMemoryRetriever;
        _chatHistoryOptions = chatHistoryOptions;
        _memorySearchOptions = memorySearchOptions;
        _promptExecutionOptions = promptExecutionOptions;

#pragma warning disable CA1062 // Validate arguments of public methods
        _kernel = kernelProvider.GetKernel();
#pragma warning restore CA1062 // Validate arguments of public methods
    }

    protected Task<IReadOnlyList<ChatMessageContent>> GetChatMessageContentsAsync(
        List<MemoryQueryResult> memories,
        List<ChatMessageContent> messages,
        CancellationToken cancellationToken)
    {
        var chatCompletionService = GetChatCompletionService();
        var chatHistory = GetChatHistory(memories, messages);
        var promptExecutionSettings = GetPromptExecutionSettings(chatCompletionService.GetType());

        return chatCompletionService.GetChatMessageContentsAsync(
            chatHistory,
            promptExecutionSettings,
            _kernel,
            cancellationToken);
    }

    protected IAsyncEnumerable<StreamingChatMessageContent> GetStreamingChatMessageContentsAsync(
        List<MemoryQueryResult> memories,
        List<ChatMessageContent> messages,
        CancellationToken cancellationToken)
    {
        var chatCompletionService = GetChatCompletionService();
        var chatHistory = GetChatHistory(memories, messages);
        var promptExecutionSettings = GetPromptExecutionSettings(chatCompletionService.GetType());

        return chatCompletionService.GetStreamingChatMessageContentsAsync(
            chatHistory,
            promptExecutionSettings,
            _kernel,
            cancellationToken);
    }

    protected async Task<List<MemoryQueryResult>> GetMemoriesAsync(
        string? collection,
        string? query,
        CancellationToken cancellationToken)
    {
        if (_semanticTextMemoryRetriever is null ||
            string.IsNullOrWhiteSpace(collection) ||
            string.IsNullOrWhiteSpace(query))
        {
            return [];
        }

        var limit = _memorySearchOptions?.Limit ?? 1;
        var minRelevanceScore = _memorySearchOptions?.MinRelevanceScore ?? 0.7;

        return await _semanticTextMemoryRetriever
            .SearchMemoriesAsync(
                collection,
                query,
                limit,
                minRelevanceScore,
                false,
                _kernel,
                cancellationToken)
            .ToListAsync(cancellationToken)
            .ConfigureAwait(false);
    }

    private IChatCompletionService GetChatCompletionService() =>
        _kernel.GetRequiredService<IChatCompletionService>();

    private ChatHistory GetChatHistory(
        List<MemoryQueryResult> memories,
        List<ChatMessageContent> messages) =>
        _chatHistoryRetriever.GetChatHistory(
            memories,
            messages,
            _chatHistoryOptions);

    private PromptExecutionSettings? GetPromptExecutionSettings(Type serviceType) =>
        _promptExecutionSettingsRetriever?.GetPromptExecutionSettings(
            _kernel,
            serviceType,
            _promptExecutionOptions);
}
