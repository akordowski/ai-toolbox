using AIToolbox.Options.SemanticKernel;
using AIToolbox.SemanticKernel;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;

namespace AIToolbox.DependencyInjection.SemanticKernel;

public class KernelBuilderExtensionsHuggingFaceTests : KernelBuilderExtensionsTestsBase
{
    public static TheoryData<Action> AddConnectorWithNullBuilder
    {
        get
        {
            KernelBuilder builder = null!;

            return
            [
                () => builder.WithHuggingFaceConnector(),
                () => builder.WithHuggingFaceConnector(new HuggingFaceOptions()),
                () => builder.WithHuggingFaceConnector(_ => { })
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
        var act = () => Builder.WithHuggingFaceConnector((Action<HuggingFaceOptions>)null!);

        // Assert
        act.Should().Throw<ArgumentNullException>().WithParameterName("optionsAction");
    }

    [Fact]
    public void Should_Throw_On_Add_Connector_With_No_Default_Options()
    {
        // Arrange
        var act = () => Builder.WithHuggingFaceConnector();
        const string message = "No 'HuggingFaceOptions' provided.*";

        // Assert
        act.Should().Throw<InvalidOperationException>().WithMessage(message);
    }

    [Fact]
    public void Should_Add_Connector_With_Default_Options()
    {
        // Arrange
        Options.Connectors = new KernelConnectorOptions
        {
            HuggingFace = new HuggingFaceOptions()
        };

        // Act
        var result = Builder.WithHuggingFaceConnector();

        // Assert
        result.Should().Be(Builder);
        Options.Connectors!.HuggingFace.Should().NotBeNull();

        AssertServices();
    }

    [Fact]
    public void Should_Add_Connector_With_Custom_Options()
    {
        // Arrange
        var options = new HuggingFaceOptions();

        // Act
        var result = Builder.WithHuggingFaceConnector(options);

        // Assert
        result.Should().Be(Builder);
        Options.Connectors!.HuggingFace.Should().Be(options);

        AssertServices();
    }

    [Fact]
    public void Should_Add_Connector_With_Options_Action()
    {
        // Arrange
        var chatCompletionOptions = new HuggingFaceChatCompletionOptions();
        var imageToTextOptions = new HuggingFaceImageToTextOptions();
        var textEmbeddingGenerationOptions = new HuggingFaceTextEmbeddingGenerationOptions();
        var textGenerationOptions = new HuggingFaceTextGenerationOptions();

        // Act
        var result = Builder.WithHuggingFaceConnector(options =>
        {
            options.ChatCompletion = chatCompletionOptions;
            options.ImageToText = imageToTextOptions;
            options.TextEmbeddingGeneration = textEmbeddingGenerationOptions;
            options.TextGeneration = textGenerationOptions;
        });

        // Assert
        result.Should().Be(Builder);
        Options.Connectors!.HuggingFace!.ChatCompletion.Should().Be(chatCompletionOptions);
        Options.Connectors!.HuggingFace!.ImageToText.Should().Be(imageToTextOptions);
        Options.Connectors!.HuggingFace!.TextEmbeddingGeneration.Should().Be(textEmbeddingGenerationOptions);
        Options.Connectors!.HuggingFace!.TextGeneration.Should().Be(textGenerationOptions);

        AssertServices();
    }

    private void AssertServices()
    {
        Services.Should().ContainSingle(descriptor => descriptor.Lifetime == ServiceLifetime.Singleton &&
                                                      descriptor.ServiceType == typeof(IKernelBuilderConfigurator) &&
                                                      descriptor.ImplementationType == typeof(HuggingFaceKernelBuilderConfigurator));
    }
}
