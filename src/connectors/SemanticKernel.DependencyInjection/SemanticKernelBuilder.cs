using AIToolbox.Options.SemanticKernel;
using Microsoft.Extensions.DependencyInjection;

namespace AIToolbox.DependencyInjection;

internal sealed class SemanticKernelBuilder : ISemanticKernelBuilder
{
    public SemanticKernelBuilder(
        SemanticKernelOptions options,
        IServiceCollection services)
    {
        Verify.ThrowIfNull(options, nameof(options));
        Verify.ThrowIfNull(services, nameof(services));
    }

    public IAddKernel AddKernel(KernelOptions? options = null)
    {
        throw new NotImplementedException();
    }

    public IAddKernel AddKernel(Action<KernelOptions> optionsAction)
    {
        throw new NotImplementedException();
    }

    public IAddKernel AddKernel(Action<IKernelBuilder> builderAction, KernelOptions? options = null)
    {
        throw new NotImplementedException();
    }

    public IAddKernel AddKernel(Action<IKernelBuilder, KernelOptions> action)
    {
        throw new NotImplementedException();
    }

    public IAddMemory AddMemory(MemoryOptions? options = null)
    {
        throw new NotImplementedException();
    }

    public IAddMemory AddMemory(Action<MemoryOptions> optionsAction)
    {
        throw new NotImplementedException();
    }

    public IAddMemory AddMemory(Action<IMemoryBuilder> builderAction, MemoryOptions? options = null)
    {
        throw new NotImplementedException();
    }

    public IAddMemory AddMemory(Action<IMemoryBuilder, MemoryOptions> action)
    {
        throw new NotImplementedException();
    }

    public IAddChatCompletion AddChatCompletion(ChatCompletionOptions? options = null)
    {
        throw new NotImplementedException();
    }

    public IAddChatCompletion AddChatCompletion(Action<ChatCompletionOptions> optionsAction)
    {
        throw new NotImplementedException();
    }

    public IAddChatCompletion AddChatCompletion(Action<IChatCompletionBuilder> builderAction, ChatCompletionOptions? options = null)
    {
        throw new NotImplementedException();
    }

    public IAddChatCompletion AddChatCompletion(Action<IChatCompletionBuilder, ChatCompletionOptions> action)
    {
        throw new NotImplementedException();
    }
}
