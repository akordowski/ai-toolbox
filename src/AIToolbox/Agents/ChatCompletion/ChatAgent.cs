using AIToolbox.Options.SemanticKernel;
using AIToolbox.SemanticKernel;
using AIToolbox.SemanticKernel.ChatCompletion;
using AIToolbox.SemanticKernel.Memory;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Memory;
using System.Runtime.CompilerServices;
using System.Text;

namespace AIToolbox.Agents.ChatCompletion;

public class ChatAgent : ChatAgentBase, IChatAgent
{
    /// <inheritdoc />
    public IReadOnlyList<ChatMessageContent> ChatMessageContents => _chatMessageContents;

    private readonly List<ChatMessageContent> _chatMessageContents = [];

    public ChatAgent(
        IKernelProvider kernelProvider,
        IChatHistoryRetriever chatHistoryRetriever,
        IPromptExecutionSettingsRetriever? promptExecutionSettingsRetriever = null,
        ISemanticTextMemoryRetriever? semanticTextMemoryRetriever = null,
        ChatHistoryOptions? chatHistoryOptions = null,
        MemorySearchOptions? memorySearchOptions = null,
        PromptExecutionOptions? promptExecutionOptions = null)
        : base(
            kernelProvider,
            chatHistoryRetriever,
            promptExecutionSettingsRetriever,
            semanticTextMemoryRetriever,
            chatHistoryOptions,
            memorySearchOptions,
            promptExecutionOptions)
    {
    }

    /// <inheritdoc />
    public async Task<IReadOnlyList<ChatMessageContent>> SendMessageAsync(
        string message,
        string? memoryCollection = null,
        CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(message, nameof(message));

        StoreMessage(AuthorRole.User, message);

        var memories = await GetMemoriesAsync(memoryCollection, cancellationToken).ConfigureAwait(false);

        var contents = await GetChatMessageContentsAsync(
                memories,
                _chatMessageContents,
                cancellationToken)
            .ConfigureAwait(false);

        StoreMessages(contents);

        return contents;
    }

    /// <inheritdoc />
    public async IAsyncEnumerable<StreamingChatMessageContent> SendMessageAsStreamAsync(
        string message,
        string? memoryCollection = null,
        [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(message, nameof(message));

        StoreMessage(AuthorRole.User, message);

        var memories = await GetMemoriesAsync(memoryCollection, cancellationToken).ConfigureAwait(false);

        var contents = GetStreamingChatMessageContentsAsync(
            memories,
            _chatMessageContents,
            cancellationToken);

        var contentBuilder = new StringBuilder();
        string? modelId = null;

        await foreach (var content in contents.ConfigureAwait(false))
        {
            contentBuilder.Append(content.Content);

            if (content.IsLastMessage())
            {
                modelId = content.ModelId;
            }

            yield return content;
        }

        StoreMessage(AuthorRole.Assistant, contentBuilder.ToString(), modelId);
    }

    private void StoreMessage(
        AuthorRole role,
        string? content,
        string? modelId = null)
    {
        var message = new ChatMessageContent(role, content, modelId);
        _chatMessageContents.Add(message);
    }

    private void StoreMessages(IReadOnlyList<ChatMessageContent> messages) =>
        _chatMessageContents.AddRange(messages);

    private async Task<List<MemoryQueryResult>> GetMemoriesAsync(
        string? collection,
        CancellationToken cancellationToken)
    {
        var query = _chatMessageContents.LastOrDefault(m => m.Role == AuthorRole.User)?.Content;
        return await GetMemoriesAsync(collection, query, cancellationToken).ConfigureAwait(false);
    }
}
