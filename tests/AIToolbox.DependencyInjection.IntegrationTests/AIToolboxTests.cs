using AIToolbox.DependencyInjection;
using AIToolbox.Options;
using AIToolbox.Options.Connectors;
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
        _services.AddAIToolbox((Action<AIToolboxOptions>)(builder => { }));
    }

    [Fact]
    public void Should_Build_AIToolbox_Dependencies_With_Custom_Options()
    {
        // Arrange
        _services.AddAIToolbox(
            builder =>
            {
                builder.ConfigureConnectors(new ConnectorOptions());
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
                builder.ConfigureConnectors(options => { });
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


        _services.AddAIToolbox(builder => { }, config, section);
    }
}
