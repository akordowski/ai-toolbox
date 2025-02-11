using AIToolbox.Options.SemanticKernel;

namespace AIToolbox.DependencyInjection;

public interface IAddKernel : IAddChatCompletion, IAddMemory
{
    IAddKernel AddKernel(KernelOptions? options = null);
    IAddKernel AddKernel(Action<KernelOptions> optionsAction);
    IAddKernel AddKernel(Action<IKernelBuilder> builderAction, KernelOptions? options = null);
    IAddKernel AddKernel(Action<IKernelBuilder, KernelOptions> action);
}
