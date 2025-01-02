using AIToolbox.Options.SemanticKernel;

namespace AIToolbox.Agents.ChatCompletion.Models;

public sealed record Settings
{
    public required string Id { get; init; } = default!;
    public required string ChatId { get; init; } = default!;
    public ChatHistoryOptions? ChatHistory { get; init; }
    public MemorySearchOptions? MemorySearch { get; init; }
    public PromptExecutionOptions? PromptExecution { get; init; }
    public required DateTimeOffset Timestamp { get; init; }
};
