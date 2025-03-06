using AIToolbox.Options.Connectors;
using AIToolbox.TestHelper;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Xunit.DependencyInjection;

namespace AIToolbox.DependencyInjection;

public class GlobalConnectorBuilderTests : BaseTestWithFixture<AIToolboxFixture>
{
    public GlobalConnectorBuilderTests(AIToolboxFixture fixture)
        : base(fixture)
    {
    }

    [Fact]
    public void Should_Configure_By_Default_Options()
    {
        // Arrange
        var host = Fixture.GetHost((_, services) =>
        {
            services.AddAIToolbox(
                aiToolbox =>
                {
                    aiToolbox.ConfigureGlobalConnectorOptions(TestBuilder.AddGlobalConnectorMethods);
                },
                options =>
                {
                    options.GlobalConnectors = TestOptions.AIToolbox.GlobalConnectors;
                });
        });

        // Assert
        AssertServices(host.Services);
    }

    [Theory]
    [MemberData(nameof(GlobalConnectorBuilderTestData.ConfigureByOptionsAction), MemberType = typeof(GlobalConnectorBuilderTestData))]
    public void Should_Configure_By_Options_Action(Action<IAIToolboxBuilder, GlobalConnectorOptions> act)
    {
        // Arrange
        var host = Fixture.GetHost((_, services) =>
        {
            services.AddAIToolbox(aiToolbox => act(aiToolbox, TestOptions.AIToolbox.GlobalConnectors!));
        });

        // Assert
        AssertServices(host.Services);
    }

    [Fact]
    public void Should_Configure_By_Config_File()
    {
        // Arrange
        var host = Fixture.GetHost(
            (context, services) =>
            {
                services.AddAIToolbox(
                    aiToolbox =>
                    {
                        aiToolbox.ConfigureGlobalConnectorOptions(TestBuilder.AddGlobalConnectorMethods);
                    },
                    context.Configuration);
            },
            true);

        // Assert
        AssertServices(host.Services);
    }

    private void AssertServices(IServiceProvider services)
    {
        var options = TestOptions.AIToolbox.GlobalConnectors!;

        services.GetService<GlobalAzureOpenAIOptions>().Should().BeEquivalentTo(options.AzureOpenAI);
        services.GetService<GlobalGoogleOptions>().Should().BeEquivalentTo(options.Google);
        services.GetService<GlobalHuggingFaceOptions>().Should().BeEquivalentTo(options.HuggingFace);
        services.GetService<GlobalMistralAIOptions>().Should().BeEquivalentTo(options.MistralAI);
        services.GetService<GlobalOllamaOptions>().Should().BeEquivalentTo(options.Ollama);
        services.GetService<GlobalOpenAIOptions>().Should().BeEquivalentTo(options.OpenAI);
        services.GetService<GlobalVertexAIOptions>().Should().BeEquivalentTo(options.VertexAI);
    }
}
