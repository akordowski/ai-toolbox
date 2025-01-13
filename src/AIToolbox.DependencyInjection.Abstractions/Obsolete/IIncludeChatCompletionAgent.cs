using AIToolbox.Options.SemanticKernel;

namespace AIToolbox.DependencyInjection;

public interface IIncludeChatCompletionAgent
{
    IChatCompletionAgentServiceBuilder IncludeChatCompletionAgent(ChatCompletionOptions? options = null);
    IChatCompletionAgentServiceBuilder IncludeChatCompletionAgent(Action<ChatCompletionOptions> optionsAction);
}
