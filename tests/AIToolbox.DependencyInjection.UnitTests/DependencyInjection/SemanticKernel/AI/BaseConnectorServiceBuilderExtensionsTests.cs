using AIToolbox.Options.Connectors;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace AIToolbox.DependencyInjection.SemanticKernel;

public abstract class BaseConnectorServiceBuilderExtensionsTests
{
    protected ConnectorOptions Options { get; } = new();
    protected ServiceCollection Services { get; } = [];
    protected Mock<IServiceBuilderService> BuilderServiceMock { get; } = new();
    protected IConnectorServiceBuilder Builder { get; }

    protected BaseConnectorServiceBuilderExtensionsTests()
    {
        Builder = new ConnectorServiceBuilder(Options, Services, BuilderServiceMock.Object);
    }
}
