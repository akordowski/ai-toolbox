using AIToolbox.Options.SemanticKernel;

namespace AIToolbox.DependencyInjection;

public interface IAddKernel
{
    IKernelServiceBuilder AddKernel(KernelOptions? options = null);
    IKernelServiceBuilder AddKernel(Action<KernelOptions> optionsAction);
}
