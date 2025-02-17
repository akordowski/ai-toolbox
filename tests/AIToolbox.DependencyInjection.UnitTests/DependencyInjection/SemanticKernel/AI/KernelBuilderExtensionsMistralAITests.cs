using AIToolbox.Options.SemanticKernel;
using AIToolbox.SemanticKernel;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;

namespace AIToolbox.DependencyInjection.SemanticKernel;

public class KernelBuilderExtensionsMistralAITests : KernelBuilderExtensionsTestsBase
{
    public static TheoryData<Action> AddConnectorWithNullBuilder
    {
        get
        {
            KernelBuilder builder = null!;

            return
            [
                () => builder.WithMistralAIConnector(),
                () => builder.WithMistralAIConnector(new MistralOptions()),
                () => builder.WithMistralAIConnector(_ => { })
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
        var act = () => Builder.WithMistralAIConnector((Action<MistralOptions>)null!);

        // Assert
        act.Should().Throw<ArgumentNullException>().WithParameterName("optionsAction");
    }

    [Fact]
    public void Should_Throw_On_Add_Connector_With_No_Default_Options()
    {
        // Arrange
        var act = () => Builder.WithMistralAIConnector();
        const string message = "No 'MistralOptions' provided.*";

        // Assert
        act.Should().Throw<InvalidOperationException>().WithMessage(message);
    }

    [Fact]
    public void Should_Add_Connector_With_Default_Options()
    {
        // Arrange
        Options.Connectors = new KernelConnectorOptions
        {
            Mistral = new MistralOptions()
        };

        // Act
        var result = Builder.WithMistralAIConnector();

        // Assert
        result.Should().Be(Builder);
        Options.Connectors!.Mistral.Should().NotBeNull();

        AssertServices();
    }

    [Fact]
    public void Should_Add_Connector_With_Custom_Options()
    {
        // Arrange
        var options = new MistralOptions();

        // Act
        var result = Builder.WithMistralAIConnector(options);

        // Assert
        result.Should().Be(Builder);
        Options.Connectors!.Mistral.Should().Be(options);

        AssertServices();
    }

    [Fact]
    public void Should_Add_Connector_With_Options_Action()
    {
        // Arrange
        var chatCompletionOptions = new MistralChatCompletionOptions();
        var textEmbeddingGeneration = new MistralTextEmbeddingGenerationOptions();

        // Act
        var result = Builder.WithMistralAIConnector(options =>
        {
            options.ChatCompletion = chatCompletionOptions;
            options.TextEmbeddingGeneration = textEmbeddingGeneration;
        });

        // Assert
        result.Should().Be(Builder);
        Options.Connectors!.Mistral!.ChatCompletion.Should().Be(chatCompletionOptions);
        Options.Connectors!.Mistral!.TextEmbeddingGeneration.Should().Be(textEmbeddingGeneration);

        AssertServices();
    }

    private void AssertServices()
    {
        Services.Should().ContainSingle(descriptor => descriptor.Lifetime == ServiceLifetime.Singleton &&
                                                      descriptor.ServiceType == typeof(IKernelBuilderConfigurator) &&
                                                      descriptor.ImplementationType == typeof(MistralAIKernelBuilderConfigurator));
    }
}
