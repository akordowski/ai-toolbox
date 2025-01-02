using AIToolbox.Options.SemanticKernel;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Memory;

namespace AIToolbox.SemanticKernel.ChatCompletion;

public interface IChatHistoryRetriever
{
    ChatHistory GetChatHistory(
        IEnumerable<MemoryQueryResult> memories,
        IEnumerable<ChatMessageContent> messages,
        ChatHistoryOptions? options = null);
}
