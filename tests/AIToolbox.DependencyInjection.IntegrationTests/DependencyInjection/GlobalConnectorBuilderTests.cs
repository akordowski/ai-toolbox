using AIToolbox.Data;
using AIToolbox.Options.Connectors;
using AIToolbox.Tests;
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
                    aiToolbox.ConfigureGlobalConnectorOptions(globalConnectors =>
                    {
                        globalConnectors
                            .AddGlobalAzureOpenAIOptions()
                            .AddGlobalGoogleOptions()
                            .AddGlobalHuggingFaceOptions()
                            .AddGlobalMistralAIOptions()
                            .AddGlobalOllamaOptions()
                            .AddGlobalOpenAIOptions()
                            .AddGlobalVertexAIOptions();
                    });
                },
                options =>
                {
                    options.GlobalConnectors = Fixture.Options.GlobalConnectors;
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
            services.AddAIToolbox(aiToolbox => act(aiToolbox, Fixture.Options.GlobalConnectors!));
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
                        aiToolbox.ConfigureGlobalConnectorOptions(globalConnectors =>
                        {
                            globalConnectors
                                .AddGlobalAzureOpenAIOptions()
                                .AddGlobalGoogleOptions()
                                .AddGlobalHuggingFaceOptions()
                                .AddGlobalMistralAIOptions()
                                .AddGlobalOllamaOptions()
                                .AddGlobalOpenAIOptions()
                                .AddGlobalVertexAIOptions();
                        });
                    },
                    context.Configuration);
            },
            "ConfigGlobalConnectorBuilder.json");

        // Assert
        AssertServices(host.Services);
    }

    private void AssertServices(IServiceProvider services)
    {
        var options = Fixture.Options.GlobalConnectors!;

        services.GetService<GlobalAzureOpenAIOptions>().Should().BeEquivalentTo(options.AzureOpenAI);
        services.GetService<GlobalGoogleOptions>().Should().BeEquivalentTo(options.Google);
        services.GetService<GlobalHuggingFaceOptions>().Should().BeEquivalentTo(options.HuggingFace);
        services.GetService<GlobalMistralAIOptions>().Should().BeEquivalentTo(options.MistralAI);
        services.GetService<GlobalOllamaOptions>().Should().BeEquivalentTo(options.Ollama);
        services.GetService<GlobalOpenAIOptions>().Should().BeEquivalentTo(options.OpenAI);
        services.GetService<GlobalVertexAIOptions>().Should().BeEquivalentTo(options.VertexAI);
    }
}
