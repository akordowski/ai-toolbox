using AIToolbox.Data;
using AIToolbox.Options.SemanticKernel;
using AIToolbox.SemanticKernel;
using AIToolbox.Tests;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Xunit.DependencyInjection;

namespace AIToolbox.DependencyInjection.SemanticKernel;

public class SemanticKernelBuilderTests : BaseTestWithFixture<AIToolboxFixture>
{
    private readonly KernelOptions _kernelOptions;

    public SemanticKernelBuilderTests(AIToolboxFixture fixture)
        : base(fixture)
    {
        _kernelOptions = fixture.Options.SemanticKernel!.Kernel!;
    }

    [Fact]
    public void Should_Get_Services_By_Default_Options()
    {
        var host = Fixture.GetHost((_, services) =>
        {
            services.AddAIToolbox(
                aiToolbox => aiToolbox.AddSemanticKernel(semanticKernel =>
                    semanticKernel.AddKernel(BuilderHelper.RegisterKernelMethods)),
                options => options.SemanticKernel = Fixture.Options.SemanticKernel);
        });

        // Assert
        AssertServices(host.Services);
    }

    [Theory]
    [MemberData(nameof(SemanticKernelBuilderTestData.ConfigureByOptionsAction), MemberType = typeof(SemanticKernelBuilderTestData))]
    public void Should_Configure_By_Options_Action(Action<IAIToolboxBuilder, SemanticKernelOptions> act)
    {
        // Arrange
        var host = Fixture.GetHost((_, services) =>
            services.AddAIToolbox(aiToolbox => act(aiToolbox, Fixture.Options.SemanticKernel!)));

        // Assert
        AssertServices(host.Services);
    }

    [Fact]
    public void Should_Get_Services_By_Config_File()
    {
        var host = Fixture.GetHost(
            (context, services) =>
                services.AddAIToolbox(
                    aiToolbox => aiToolbox.AddSemanticKernel(semanticKernel =>
                        semanticKernel.AddKernel(BuilderHelper.RegisterKernelMethods)),
                    context.Configuration),
            true);

        // Assert
        AssertServices(host.Services);
    }

    private void AssertServices(IServiceProvider services)
    {
        var options = Fixture.Options.SemanticKernel!;
        var connectors = options.Kernel!.Connectors!;

        services.GetService<KernelOptions>().Should().BeEquivalentTo(options.Kernel);
        services.GetService<IKernelProvider>().Should().NotBeNull();

        services.GetService<AzureOpenAIOptions>().Should().BeEquivalentTo(connectors.AzureOpenAI);
        services.GetService<GoogleOptions>().Should().BeEquivalentTo(connectors.Google);
        services.GetService<HuggingFaceOptions>().Should().BeEquivalentTo(connectors.HuggingFace);
        services.GetService<MistralAIOptions>().Should().BeEquivalentTo(connectors.MistralAI);
        services.GetService<OllamaOptions>().Should().BeEquivalentTo(connectors.Ollama);
        services.GetService<OpenAIOptions>().Should().BeEquivalentTo(connectors.OpenAI);
        services.GetService<VertexAIOptions>().Should().BeEquivalentTo(connectors.VertexAI);
    }
}
