using AIToolbox.Options.SemanticKernel;
using AIToolbox.SemanticKernel;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;

namespace AIToolbox.DependencyInjection.SemanticKernel;

public class KernelBuilderExtensionsOpenAITests : KernelBuilderExtensionsTestsBase
{
    public static TheoryData<Action> AddConnectorWithNullBuilder
    {
        get
        {
            KernelBuilder builder = null!;

            return
            [
                () => builder.WithOpenAIConnector(),
                () => builder.WithOpenAIConnector(new OpenAIOptions()),
                () => builder.WithOpenAIConnector(_ => { })
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
        var act = () => Builder.WithOpenAIConnector((Action<OpenAIOptions>)null!);

        // Assert
        act.Should().Throw<ArgumentNullException>().WithParameterName("optionsAction");
    }

    [Fact]
    public void Should_Throw_On_Add_Connector_With_No_Default_Options()
    {
        // Arrange
        var act = () => Builder.WithOpenAIConnector();
        const string message = "No 'OpenAIOptions' provided.*";

        // Assert
        act.Should().Throw<InvalidOperationException>().WithMessage(message);
    }

    [Fact]
    public void Should_Add_Connector_With_Default_Options()
    {
        // Arrange
        Options.Connectors = new KernelConnectorOptions
        {
            OpenAI = new OpenAIOptions()
        };

        // Act
        var result = Builder.WithOpenAIConnector();

        // Assert
        result.Should().Be(Builder);
        Options.Connectors!.OpenAI.Should().NotBeNull();

        AssertServices();
    }

    [Fact]
    public void Should_Add_Connector_With_Custom_Options()
    {
        // Arrange
        var options = new OpenAIOptions();

        // Act
        var result = Builder.WithOpenAIConnector(options);

        // Assert
        result.Should().Be(Builder);
        Options.Connectors!.OpenAI.Should().Be(options);

        AssertServices();
    }

    [Fact]
    public void Should_Add_Connector_With_Options_Action()
    {
        // Arrange
        var audioToTextOptions = new OpenAIAudioToTextOptions();
        var chatCompletionOptions = new OpenAIChatCompletionOptions();
        var filesOptions = new OpenAIFilesOptions();
        var textEmbeddingGenerationOptions = new OpenAITextEmbeddingGenerationOptions();
        var textToAudioOptions = new OpenAITextToAudioOptions();
        var textToImageOptions = new OpenAITextToImageOptions();

        // Act
        var result = Builder.WithOpenAIConnector(options =>
        {
            options.AudioToText = audioToTextOptions;
            options.ChatCompletion = chatCompletionOptions;
            options.Files = filesOptions;
            options.TextEmbeddingGeneration = textEmbeddingGenerationOptions;
            options.TextToAudio = textToAudioOptions;
            options.TextToImage = textToImageOptions;
        });

        // Assert
        result.Should().Be(Builder);
        Options.Connectors!.OpenAI!.AudioToText.Should().Be(audioToTextOptions);
        Options.Connectors!.OpenAI!.ChatCompletion.Should().Be(chatCompletionOptions);
        Options.Connectors!.OpenAI!.Files.Should().Be(filesOptions);
        Options.Connectors!.OpenAI!.TextEmbeddingGeneration.Should().Be(textEmbeddingGenerationOptions);
        Options.Connectors!.OpenAI!.TextToAudio.Should().Be(textToAudioOptions);
        Options.Connectors!.OpenAI!.TextToImage.Should().Be(textToImageOptions);

        AssertServices();
    }

    private void AssertServices()
    {
        Services.Should().ContainSingle(descriptor => descriptor.Lifetime == ServiceLifetime.Singleton &&
                                                      descriptor.ServiceType == typeof(IKernelBuilderConfigurator) &&
                                                      descriptor.ImplementationType == typeof(OpenAIKernelBuilderConfigurator));

        Services.Should().ContainSingle(descriptor => descriptor.Lifetime == ServiceLifetime.Singleton &&
                                                      descriptor.ServiceType == typeof(IMemoryBuilderConfigurator) &&
                                                      descriptor.ImplementationType == typeof(OpenAIMemoryBuilderConfigurator));
    }
}
