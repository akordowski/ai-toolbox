using Microsoft.SemanticKernel.Memory;

namespace AIToolbox.SemanticKernel;

public interface IMemoryBuilderConfigurator
{
    void Configure(MemoryBuilder builder);
}
