using AIToolbox.Options.SemanticKernel;

namespace AIToolbox.DependencyInjection;

public interface IAddChatCompletion
{
    IAddChatCompletion AddChatCompletion(ChatCompletionOptions? options = null);
    IAddChatCompletion AddChatCompletion(Action<ChatCompletionOptions> optionsAction);
    IAddChatCompletion AddChatCompletion(Action<IChatCompletionBuilder> builderAction, ChatCompletionOptions? options = null);
    IAddChatCompletion AddChatCompletion(Action<IChatCompletionBuilder, ChatCompletionOptions> action);
}
