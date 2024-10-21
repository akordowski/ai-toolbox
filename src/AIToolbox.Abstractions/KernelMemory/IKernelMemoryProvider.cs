using Microsoft.KernelMemory;

namespace AIToolbox.KernelMemory;

public interface IKernelMemoryProvider
{
    IKernelMemory GetKernelMemory();
}
