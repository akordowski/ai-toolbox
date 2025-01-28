using AIToolbox.DependencyInjection;
using AIToolbox.Options;
using AIToolbox.Options.Connectors;
using AIToolbox.Options.SemanticKernel;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AIToolbox;

public class AIToolboxTests
{
    private readonly ServiceCollection _services = [];

    [Fact]
    public void Should_Build_AIToolbox_Dependencies_With_Default_Options()
    {
        // Arrange
        _services.AddAIToolbox(builder =>
        {
            builder.UseSemanticKernel(skBuilder => { });
        });
    }

    [Fact]
    public void Should_Build_AIToolbox_Dependencies_With_Custom_Options()
    {
        // Arrange
        _services.AddAIToolbox(
            builder =>
            {
                builder
                    .ConfigureConnectors(new ConnectorOptions())
                    .UseSemanticKernel(skBuilder => { }, new SemanticKernelOptions());
            },
            new AIToolboxOptions());
    }

    [Fact]
    public void Should_Build_AIToolbox_Dependencies_With_Options_Action()
    {
        // Arrange
        _services.AddAIToolbox(
            builder =>
            {
                builder
                    .ConfigureConnectors(options => { })
                    .UseSemanticKernel(skBuilder => { }, options => { });
            },
            options => { });
    }

    [Fact]
    public void Should_Build_AIToolbox_Dependencies_With_Configuration()
    {
        // Arrange
        const string section = "CustomSection";
        var config = new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string>
            {
                [$"{section}:Key"] = "Value"
            }!)
            .Build();

        _services.AddAIToolbox(
            builder =>
            {
                builder.UseSemanticKernel(skBuilder => { });
            },
            config,
            section);
    }
}
