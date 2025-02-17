using AIToolbox.Options.SemanticKernel;
using AIToolbox.SemanticKernel;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;

namespace AIToolbox.DependencyInjection.SemanticKernel;

public class KernelBuilderExtensionsAzureOpenAITests : KernelBuilderExtensionsTestsBase
{
    public static TheoryData<Action> AddConnectorWithNullBuilder
    {
        get
        {
            KernelBuilder builder = null!;

            return
            [
                () => builder.WithAzureOpenAIConnector(),
                () => builder.WithAzureOpenAIConnector(new AzureOpenAIOptions()),
                () => builder.WithAzureOpenAIConnector(_ => { })
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
        var act = () => Builder.WithAzureOpenAIConnector((Action<AzureOpenAIOptions>)null!);

        // Assert
        act.Should().Throw<ArgumentNullException>().WithParameterName("optionsAction");
    }

    [Fact]
    public void Should_Throw_On_Add_Connector_With_No_Default_Options()
    {
        // Arrange
        var act = () => Builder.WithAzureOpenAIConnector();
        const string message = "No 'AzureOpenAIOptions' provided.*";

        // Assert
        act.Should().Throw<InvalidOperationException>().WithMessage(message);
    }

    [Fact]
    public void Should_Add_Connector_With_Default_Options()
    {
        // Arrange
        Options.Connectors = new KernelConnectorOptions
        {
            AzureOpenAI = new AzureOpenAIOptions()
        };

        // Act
        var result = Builder.WithAzureOpenAIConnector();

        // Assert
        result.Should().Be(Builder);
        Options.Connectors!.AzureOpenAI.Should().NotBeNull();

        AssertServices();
    }

    [Fact]
    public void Should_Add_Connector_With_Custom_Options()
    {
        // Arrange
        var options = new AzureOpenAIOptions();

        // Act
        var result = Builder.WithAzureOpenAIConnector(options);

        // Assert
        result.Should().Be(Builder);
        Options.Connectors!.AzureOpenAI.Should().Be(options);

        AssertServices();
    }

    [Fact]
    public void Should_Add_Connector_With_Options_Action()
    {
        // Arrange
        var audioToTextOptions = new AzureOpenAIAudioToTextOptions();
        var chatCompletionOptions = new AzureOpenAIChatCompletionOptions();
        var filesOptions = new AzureOpenAIFilesOptions();
        var textEmbeddingGenerationOptions = new AzureOpenAITextEmbeddingGenerationOptions();
        var textGenerationOptions = new AzureOpenAITextGenerationOptions();
        var textToAudioOptions = new AzureOpenAITextToAudioOptions();
        var textToImageOptions = new AzureOpenAITextToImageOptions();

        // Act
        var result = Builder.WithAzureOpenAIConnector(options =>
        {
            options.AudioToText = audioToTextOptions;
            options.ChatCompletion = chatCompletionOptions;
            options.Files = filesOptions;
            options.TextEmbeddingGeneration = textEmbeddingGenerationOptions;
            options.TextGeneration = textGenerationOptions;
            options.TextToAudio = textToAudioOptions;
            options.TextToImage = textToImageOptions;
        });

        // Assert
        result.Should().Be(Builder);
        Options.Connectors!.AzureOpenAI!.AudioToText.Should().Be(audioToTextOptions);
        Options.Connectors!.AzureOpenAI!.ChatCompletion.Should().Be(chatCompletionOptions);
        Options.Connectors!.AzureOpenAI!.Files.Should().Be(filesOptions);
        Options.Connectors!.AzureOpenAI!.TextEmbeddingGeneration.Should().Be(textEmbeddingGenerationOptions);
        Options.Connectors!.AzureOpenAI!.TextGeneration.Should().Be(textGenerationOptions);
        Options.Connectors!.AzureOpenAI!.TextToAudio.Should().Be(textToAudioOptions);
        Options.Connectors!.AzureOpenAI!.TextToImage.Should().Be(textToImageOptions);

        AssertServices();
    }

    private void AssertServices()
    {
        Services.Should().ContainSingle(descriptor => descriptor.Lifetime == ServiceLifetime.Singleton &&
                                                      descriptor.ServiceType == typeof(IKernelBuilderConfigurator) &&
                                                      descriptor.ImplementationType == typeof(AzureOpenAIKernelBuilderConfigurator));

        Services.Should().ContainSingle(descriptor => descriptor.Lifetime == ServiceLifetime.Singleton &&
                                                      descriptor.ServiceType == typeof(IMemoryBuilderConfigurator) &&
                                                      descriptor.ImplementationType == typeof(AzureOpenAIMemoryBuilderConfigurator));
    }
}
