using AIToolbox.Options.SemanticKernel;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Microsoft.SemanticKernel.Embeddings;
using Microsoft.SemanticKernel.Memory;
using Moq;

namespace AIToolbox.SemanticKernel.Memory;

public class MemoryProviderTests
{
    public static TheoryData<Action, string> ConstructWithNullParameters =>
        new()
        {
            { () => _ = new MemoryProvider(null!), "options" }
        };

    [Theory]
    [MemberData(nameof(ConstructWithNullParameters))]
    public void Should_Throw_On_Construct_With_Null_Parameters(Action act, string parameterName)
    {
        // Assert
        act.Should().Throw<ArgumentNullException>().WithParameterName(parameterName);
    }

    [Fact]
    public void Should_Construct_With_Valid_Parameters()
    {
        // Arrange
        var options = new MemoryOptions();

        // Act
        _ = new MemoryProvider(options);
    }

    [Fact]
    public void Should_Get_Memory()
    {
        // Arrange
        var memoryStoreFactoryMock = new Mock<IMemoryStoreFactory>();
        memoryStoreFactoryMock
            .Setup(o => o.GetMemoryStore(It.IsAny<ILoggerFactory>(), It.IsAny<HttpClient>()))
            .Returns(Mock.Of<IMemoryStore>());

        var textEmbeddingGenerationServiceMock = new Mock<ITextEmbeddingGenerationService>();

        var options = new MemoryOptions();
        var kernelProvider = new MemoryProvider(
            options,
            memoryStoreFactoryMock.Object,
            textEmbeddingGenerationServiceMock.Object);

        // Act
        var memory = kernelProvider.GetMemory();

        // Assert
        memory.Should().NotBeNull();
    }
}
