using Microsoft.SemanticKernel.Memory;

namespace AIToolbox.SemanticKernel.Memory;

public interface IMemoryBuilderConfigurator
{
    void Configure(MemoryBuilder builder);
}
