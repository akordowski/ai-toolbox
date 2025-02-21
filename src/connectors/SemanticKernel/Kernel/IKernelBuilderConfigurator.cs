using Microsoft.SemanticKernel;

namespace AIToolbox.SemanticKernel;

public interface IKernelBuilderConfigurator
{
    void Configure(IKernelBuilder builder);
}
