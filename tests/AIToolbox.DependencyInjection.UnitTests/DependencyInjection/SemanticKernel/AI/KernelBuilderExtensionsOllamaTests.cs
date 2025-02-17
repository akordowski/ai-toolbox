using AIToolbox.Options.SemanticKernel;
using AIToolbox.SemanticKernel;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;

namespace AIToolbox.DependencyInjection.SemanticKernel;

public class KernelBuilderExtensionsOllamaTests : KernelBuilderExtensionsTestsBase
{
    public static TheoryData<Action> AddConnectorWithNullBuilder
    {
        get
        {
            KernelBuilder builder = null!;

            return
            [
                () => builder.WithOllamaConnector(),
                () => builder.WithOllamaConnector(new OllamaOptions()),
                () => builder.WithOllamaConnector(_ => { })
            ];
        }
    }

    [Theory]
    [MemberData(nameof(AddConnectorWithNullBuilder))]
    public void Should_Throw_On_Add_Connector_With_Null_Builder(Action act)
    {
        // Assert
        act.Should().Throw<ArgumentNullException>().WithParameterName("builder");
    }

    [Fact]
    public void Should_Throw_On_Add_Connector_With_Invalid_Parameters()
    {
        // Arrange
        var act = () => Builder.WithOllamaConnector((Action<OllamaOptions>)null!);

        // Assert
        act.Should().Throw<ArgumentNullException>().WithParameterName("optionsAction");
    }

    [Fact]
    public void Should_Throw_On_Add_Connector_With_No_Default_Options()
    {
        // Arrange
        var act = () => Builder.WithOllamaConnector();
        const string message = "No 'OllamaOptions' provided.*";

        // Assert
        act.Should().Throw<InvalidOperationException>().WithMessage(message);
    }

    [Fact]
    public void Should_Add_Connector_With_Default_Options()
    {
        // Arrange
        Options.Connectors = new KernelConnectorOptions
        {
            Ollama = new OllamaOptions()
        };

        // Act
        var result = Builder.WithOllamaConnector();

        // Assert
        result.Should().Be(Builder);
        Options.Connectors!.Ollama.Should().NotBeNull();

        AssertServices();
    }

    [Fact]
    public void Should_Add_Connector_With_Custom_Options()
    {
        // Arrange
        var options = new OllamaOptions();

        // Act
        var result = Builder.WithOllamaConnector(options);

        // Assert
        result.Should().Be(Builder);
        Options.Connectors!.Ollama.Should().Be(options);

        AssertServices();
    }

    [Fact]
    public void Should_Add_Connector_With_Options_Action()
    {
        // Arrange
        var chatCompletionOptions = new OllamaChatCompletionOptions();
        var textEmbeddingGenerationOptions = new OllamaTextEmbeddingGenerationOptions();
        var textGenerationOptions = new OllamaTextGenerationOptions();

        // Act
        var result = Builder.WithOllamaConnector(options =>
        {
            options.ChatCompletion = chatCompletionOptions;
            options.TextEmbeddingGeneration = textEmbeddingGenerationOptions;
            options.TextGeneration = textGenerationOptions;
        });

        // Assert
        result.Should().Be(Builder);
        Options.Connectors!.Ollama!.ChatCompletion.Should().Be(chatCompletionOptions);
        Options.Connectors!.Ollama!.TextEmbeddingGeneration.Should().Be(textEmbeddingGenerationOptions);
        Options.Connectors!.Ollama!.TextGeneration.Should().Be(textGenerationOptions);

        AssertServices();
    }

    private void AssertServices()
    {
        Services.Should().ContainSingle(descriptor => descriptor.Lifetime == ServiceLifetime.Singleton &&
                                                      descriptor.ServiceType == typeof(IKernelBuilderConfigurator) &&
                                                      descriptor.ImplementationType == typeof(OllamaKernelBuilderConfigurator));

        Services.Should().ContainSingle(descriptor => descriptor.Lifetime == ServiceLifetime.Singleton &&
                                                      descriptor.ServiceType == typeof(IMemoryBuilderConfigurator) &&
                                                      descriptor.ImplementationType == typeof(OllamaMemoryBuilderConfigurator));
    }
}
