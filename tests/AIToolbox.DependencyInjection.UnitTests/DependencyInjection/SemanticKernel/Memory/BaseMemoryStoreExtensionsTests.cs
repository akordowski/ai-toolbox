using AIToolbox.Options.SemanticKernel;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace AIToolbox.DependencyInjection.SemanticKernel;

public abstract class BaseMemoryStoreExtensionsTests
{
    protected MemoryOptions Options { get; } = new();
    protected ServiceCollection Services { get; } = [];
    protected Mock<IServiceBuilderService> BuilderServiceMock { get; } = new();
    protected IMemoryServiceBuilder Builder { get; }

    protected BaseMemoryStoreExtensionsTests()
    {
        Builder = new MemoryServiceBuilder(Options, Services, BuilderServiceMock.Object);
    }
}
