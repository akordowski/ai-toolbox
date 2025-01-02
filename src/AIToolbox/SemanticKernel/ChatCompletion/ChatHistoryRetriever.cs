using AIToolbox.Options.SemanticKernel;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Memory;
using System.Text;

namespace AIToolbox.SemanticKernel.ChatCompletion;

public class ChatHistoryRetriever : IChatHistoryRetriever
{
    public ChatHistory GetChatHistory(
        IEnumerable<MemoryQueryResult> memories,
        IEnumerable<ChatMessageContent> messages,
        ChatHistoryOptions? options = null)
    {
        var chatHistory = new ChatHistory();

        AddSystemMessages(chatHistory, options);
        AddMemories(chatHistory, memories, options);
        AddMessages(chatHistory, messages);

        return chatHistory;
    }

    private static void AddSystemMessages(
        ChatHistory chatHistory,
        ChatHistoryOptions? options)
    {
        if (options is null)
        {
            return;
        }

        var systemOptions = new List<string?>
            {
                options.SystemDescription,
                options.SystemResponse
            }
            .Where(value => !string.IsNullOrWhiteSpace(value))
            .ToList();

        if (systemOptions.Count > 0)
        {
            var systemMessage = string.Join("\n\n", systemOptions);
            chatHistory.AddSystemMessage(systemMessage);
        }

        if (!string.IsNullOrWhiteSpace(options.InitialAssistantMessage))
        {
            chatHistory.AddAssistantMessage(options.InitialAssistantMessage.Trim());
        }
    }

    private static void AddMemories(
        ChatHistory chatHistory,
        IEnumerable<MemoryQueryResult> memories,
        ChatHistoryOptions? options)
    {
        memories = memories.ToList();

        if (!memories.Any())
        {
            return;
        }

        var initialMemoryMessage = !string.IsNullOrWhiteSpace(options?.InitialMemoryMessage)
            ? options.InitialMemoryMessage
            : "User provided memories:";

        var memoryMessageBuilder = new StringBuilder()
            .AppendLine(initialMemoryMessage);

        foreach (var memory in memories)
        {
            memoryMessageBuilder.AppendLine(memory.Metadata.Text);
        }

        var systemMessage = memoryMessageBuilder.ToString().Trim();
        chatHistory.AddSystemMessage(systemMessage);
    }

    private static void AddMessages(
        ChatHistory chatHistory,
        IEnumerable<ChatMessageContent> messages)
    {
        chatHistory.AddRange(messages);
    }
}
