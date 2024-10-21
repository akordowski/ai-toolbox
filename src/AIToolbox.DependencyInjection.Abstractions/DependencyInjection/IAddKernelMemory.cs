using AIToolbox.Options.KernelMemory;

namespace AIToolbox.DependencyInjection;

public interface IAddKernelMemory
{
    IKernelMemoryServiceBuilder AddKernelMemory(KernelMemoryOptions? options = null);
    IKernelMemoryServiceBuilder AddKernelMemory(Action<KernelMemoryOptions> optionsAction);
}
