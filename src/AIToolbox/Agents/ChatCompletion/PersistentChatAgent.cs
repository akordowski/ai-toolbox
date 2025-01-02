using AIToolbox.Agents.ChatCompletion.Models;
using AIToolbox.Agents.ChatCompletion.Services;
using AIToolbox.Options.SemanticKernel;
using AIToolbox.SemanticKernel;
using AIToolbox.SemanticKernel.ChatCompletion;
using AIToolbox.SemanticKernel.Memory;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Memory;
using System.Runtime.CompilerServices;
using System.Text;
using AuthorRole = Microsoft.SemanticKernel.ChatCompletion.AuthorRole;

namespace AIToolbox.Agents.ChatCompletion;

public class PersistentChatAgent : ChatAgentBase, IPersistentChatAgent
{
    /// <inheritdoc />
    public bool IsInitialized => Chat is not null;

    /// <inheritdoc />
    public IReadOnlyList<ChatMessageContent> ChatMessageContents => _chatMessageContents;

    /// <inheritdoc />
    public Chat? Chat { get; private set; }

    /// <inheritdoc />
    public IReadOnlyList<Message> Messages => _messages;

    /// <inheritdoc />
    public IReadOnlyList<Participant> Participants => _participants;

    private readonly IPersistentChatAgentService _service;

    private readonly List<ChatMessageContent> _chatMessageContents = [];
    private readonly List<Message> _messages = [];
    private readonly List<Participant> _participants = [];

    public PersistentChatAgent(
        IPersistentChatAgentService service,
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
        _service = service;
    }

    /// <inheritdoc />
    public async Task<Chat> CreateChatAsync(
        string title,
        string userId,
        string userName,
        CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(title, nameof(title));
        ArgumentException.ThrowIfNullOrWhiteSpace(userId, nameof(userId));
        ArgumentException.ThrowIfNullOrWhiteSpace(userName, nameof(userName));
        ThrowIfInitialized("The chat cannot be created because the agent is already initialized.");

        var chatExists = await _service.Chats
            .AnyAsync(chat => chat.Title == title, cancellationToken)
            .ConfigureAwait(false);

        if (chatExists)
        {
            throw new InvalidOperationException($"Chat with title '{title}' already exists.");
        }

        var timestamp = DateTimeOffset.UtcNow;

        var chat = new Chat
        {
            Id = Guid.NewGuid().ToString(),
            Title = title,
            Timestamp = timestamp
        };

        var participant = new Participant
        {
            Id = Guid.NewGuid().ToString(),
            ChatId = chat.Id,
            UserId = userId,
            UserName = userName,
            Timestamp = timestamp
        };

        await _service.Chats.AddOrUpdateAsync(chat, cancellationToken).ConfigureAwait(false);
        await _service.Participants.AddOrUpdateAsync(participant, cancellationToken).ConfigureAwait(false);

        Chat = chat;
        _participants.Add(participant);

        return Chat;
    }

    /// <inheritdoc />
    public async Task<Chat> LoadChatAsync(
        string chatId,
        CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(chatId, nameof(chatId));
        ThrowIfInitialized("The chat cannot be loaded because the agent is already initialized.");

        Chat = await _service.Chats
            .GetAsync(chatId, cancellationToken)
            .ConfigureAwait(false);

        if (Chat is null)
        {
            throw new InvalidOperationException($"Chat with id '{chatId}' does not exist.");
        }

        var messages = await _service.Messages
            .GetAllByChatIdAsync(chatId, cancellationToken)
            .ConfigureAwait(false);

        messages = messages.OrderBy(m => m.Timestamp);
        _messages.AddRange(messages);

        var participants = await _service.Participants
            .GetAllByChatIdAsync(chatId, cancellationToken)
            .ConfigureAwait(false);

        participants = participants.OrderBy(m => m.Timestamp);
        _participants.AddRange(participants);

        return Chat;
    }

    /// <inheritdoc />
    public async Task<IReadOnlyList<ChatMessageContent>> SendMessageAsync(
        string userId,
        string message,
        string? memoryCollection = null,
        CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(userId, nameof(userId));
        ArgumentException.ThrowIfNullOrWhiteSpace(message, nameof(message));
        ThrowIfNotInitialized();
        ThrowIfUserNotExists(userId);

        var userMessageData = new MessageData(
            Role: AuthorRole.User,
            Content: message,
            UserId: userId);

        await StoreMessageAsync(userMessageData, cancellationToken).ConfigureAwait(false);

        memoryCollection ??= Chat!.Id;
        var memories = await GetMemoriesAsync(memoryCollection, cancellationToken).ConfigureAwait(false);

        var contents = await GetChatMessageContentsAsync(
                memories,
                _chatMessageContents,
                cancellationToken)
            .ConfigureAwait(false);

        await StoreMessagesAsync(contents, cancellationToken).ConfigureAwait(false);

        return contents;
    }

    /// <inheritdoc />
    public async IAsyncEnumerable<StreamingChatMessageContent> SendMessageAsStreamAsync(
        string userId,
        string message,
        string? memoryCollection = null,
        [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(userId, nameof(userId));
        ArgumentException.ThrowIfNullOrWhiteSpace(message, nameof(message));
        ThrowIfNotInitialized();
        ThrowIfUserNotExists(userId);

        var userMessageData = new MessageData(
            Role: AuthorRole.User,
            Content: message,
            UserId: userId);

        await StoreMessageAsync(userMessageData, cancellationToken).ConfigureAwait(false);

        memoryCollection ??= Chat!.Id;
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

        var assistantMessageData = new MessageData(
            Role: AuthorRole.Assistant,
            Content: contentBuilder.ToString(),
            ModelId: modelId);

        await StoreMessageAsync(assistantMessageData, cancellationToken).ConfigureAwait(false);
    }

    private Message CreateMessageModel(MessageData messageData) =>
        new()
        {
            Id = Guid.NewGuid().ToString(),
            ChatId = Chat!.Id,
            UserId = messageData.UserId,
            Content = messageData.Content,
            Role = messageData.Role.ToEnum(),
            Timestamp = messageData.Timestamp ?? DateTimeOffset.UtcNow
        };

    private async Task StoreMessageAsync(
        MessageData messageData,
        CancellationToken cancellationToken = default)
    {
        var message = new ChatMessageContent(
            messageData.Role,
            messageData.Content,
            messageData.ModelId);

        var messageModel = CreateMessageModel(messageData);

        _chatMessageContents.Add(message);
        _messages.Add(messageModel);
        await _service.Messages.AddOrUpdateAsync(messageModel, cancellationToken).ConfigureAwait(false);
    }

    private async Task StoreMessagesAsync(
        IReadOnlyList<ChatMessageContent> messages,
        CancellationToken cancellationToken)
    {
        var timestamp = DateTimeOffset.UtcNow;
        var messageModels = messages
            .Select(m => CreateMessageModel(new MessageData(
                Role: m.Role,
                Content: m.Content,
                Timestamp: timestamp)))
            .ToList();

        _chatMessageContents.AddRange(messages);
        _messages.AddRange(messageModels);
        await _service.Messages.AddOrUpdateRangeAsync(messageModels, cancellationToken).ConfigureAwait(false);
    }

    private async Task<List<MemoryQueryResult>> GetMemoriesAsync(
        string? collection,
        CancellationToken cancellationToken)
    {
        var query = _chatMessageContents.LastOrDefault(m => m.Role == AuthorRole.User)?.Content;
        return await GetMemoriesAsync(collection, query, cancellationToken).ConfigureAwait(false);
    }

    private void ThrowIfInitialized(string message)
    {
        if (IsInitialized)
        {
            throw new InvalidOperationException(message);
        }
    }

    private void ThrowIfNotInitialized()
    {
        if (!IsInitialized)
        {
            throw new InvalidOperationException("To use the agent create or load a chat first.");
        }
    }

    private void ThrowIfUserNotExists(string userId)
    {
        var userExists = _participants.Any(o => o.UserId == userId);

        if (!userExists)
        {
            throw new InvalidOperationException($"User with id '{userId}' is not a participant of the chat with id '{Chat!.Id}'.");
        }
    }

    private sealed record MessageData(
        AuthorRole Role,
        string? Content,
        string? ModelId = null,
        string? UserId = null,
        DateTimeOffset? Timestamp = null);
}
