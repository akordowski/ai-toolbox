using AIToolbox.Options.SemanticKernel;
using AIToolbox.SemanticKernel;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;

namespace AIToolbox.DependencyInjection.SemanticKernel;

public class KernelBuilderExtensionsGoogleTests : KernelBuilderExtensionsTestsBase
{
    public static TheoryData<Action> AddConnectorWithNullBuilder
    {
        get
        {
            KernelBuilder builder = null!;

            return
            [
                () => builder.WithGoogleConnector(),
                () => builder.WithGoogleConnector(new GoogleAIOptions()),
                () => builder.WithGoogleConnector(_ => { })
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
        var act = () => Builder.WithGoogleConnector((Action<GoogleAIOptions>)null!);

        // Assert
        act.Should().Throw<ArgumentNullException>().WithParameterName("optionsAction");
    }

    [Fact]
    public void Should_Throw_On_Add_Connector_With_No_Default_Options()
    {
        // Arrange
        var act = () => Builder.WithGoogleConnector();
        const string message = "No 'GoogleAIOptions' provided.*";

        // Assert
        act.Should().Throw<InvalidOperationException>().WithMessage(message);
    }

    [Fact]
    public void Should_Add_Connector_With_Default_Options()
    {
        // Arrange
        Options.Connectors = new KernelConnectorOptions
        {
            GoogleAI = new GoogleAIOptions()
        };

        // Act
        var result = Builder.WithGoogleConnector();

        // Assert
        result.Should().Be(Builder);
        Options.Connectors!.GoogleAI.Should().NotBeNull();

        AssertServices();
    }

    [Fact]
    public void Should_Add_Connector_With_Custom_Options()
    {
        // Arrange
        var options = new GoogleAIOptions();

        // Act
        var result = Builder.WithGoogleConnector(options);

        // Assert
        result.Should().Be(Builder);
        Options.Connectors!.GoogleAI.Should().Be(options);

        AssertServices();
    }

    [Fact]
    public void Should_Add_Connector_With_Options_Action()
    {
        // Arrange
        var chatCompletionOptions = new GoogleAIChatCompletionOptions();
        var embeddingGenerationOptions = new GoogleAIEmbeddingGenerationOptions();

        // Act
        var result = Builder.WithGoogleConnector(options =>
        {
            options.ChatCompletion = chatCompletionOptions;
            options.EmbeddingGeneration = embeddingGenerationOptions;
        });

        // Assert
        result.Should().Be(Builder);
        Options.Connectors!.GoogleAI!.ChatCompletion.Should().Be(chatCompletionOptions);
        Options.Connectors!.GoogleAI!.EmbeddingGeneration.Should().Be(embeddingGenerationOptions);

        AssertServices();
    }

    private void AssertServices()
    {
        Services.Should().ContainSingle(descriptor => descriptor.Lifetime == ServiceLifetime.Singleton &&
                                                      descriptor.ServiceType == typeof(IKernelBuilderConfigurator) &&
                                                      descriptor.ImplementationType == typeof(GoogleKernelBuilderConfigurator));
    }
}
