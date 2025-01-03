using AIToolbox.Options.Connectors;
using FluentAssertions;
using Moq;

namespace AIToolbox.DependencyInjection.SemanticKernel;

public class AIToolboxServiceBuilderTests
{
    private readonly Mock<IServiceBuilderService> _builderServiceMock = new();
    private readonly AIToolboxServiceBuilder _builder;

    public AIToolboxServiceBuilderTests()
    {
        _builder = new AIToolboxServiceBuilder(_builderServiceMock.Object);
    }

    [Fact]
    public void Should_Construct_With_Valid_Parameters()
    {
        // Act
        var act = () => new AIToolboxServiceBuilder(_builderServiceMock.Object);

        // Assert
        act.Should().NotThrow();
    }

    [Fact]
    public void Should_Throw_Exception_When_Constructed_With_Null_BuilderService()
    {
        // Act
        var act = () => new AIToolboxServiceBuilder(null!);

        // Assert
        act.Should().Throw<ArgumentNullException>().WithParameterName("builderService");
    }

    [Fact]
    public void Should_Add_Connectors_With_Options()
    {
        // Arrange
        var options = new ConnectorOptions();
        var connectorsBuilder = Mock.Of<IConnectorServiceBuilder>();
        _builderServiceMock
            .Setup(o => o.AddConnectors(options))
            .Returns(connectorsBuilder);

        // Act
        var result = _builder.AddConnectors(options);

        // Assert
        result.Should().Be(connectorsBuilder);
        _builderServiceMock.Verify(o => o.AddConnectors(options), Times.Once);
    }

    [Fact]
    public void Should_Add_Connectors_With_Null_Options()
    {
        // Arrange
        ConnectorOptions? options = null;
        var connectorsBuilder = Mock.Of<IConnectorServiceBuilder>();
        _builderServiceMock
            .Setup(o => o.AddConnectors(options))
            .Returns(connectorsBuilder);

        // Act
        var result = _builder.AddConnectors(options);

        // Assert
        result.Should().Be(connectorsBuilder);
        _builderServiceMock.Verify(o => o.AddConnectors(options), Times.Once);
    }

    [Fact]
    public void Should_Add_Connectors_With_Options_Action()
    {
        // Arrange
        Action<ConnectorOptions> optionsAction = _ => { };
        var connectorsBuilder = Mock.Of<IConnectorServiceBuilder>();
        _builderServiceMock
            .Setup(o => o.AddConnectors(optionsAction))
            .Returns(connectorsBuilder);

        // Act
        var result = _builder.AddConnectors(optionsAction);

        // Assert
        result.Should().Be(connectorsBuilder);
        _builderServiceMock.Verify(o => o.AddConnectors(optionsAction), Times.Once);
    }
}
