using Microsoft.KernelMemory;

namespace AIToolbox.KernelMemory;

public interface IKernelMemoryBuilderConfigurator
{
    void Configure(IKernelMemoryBuilder builder);
}
