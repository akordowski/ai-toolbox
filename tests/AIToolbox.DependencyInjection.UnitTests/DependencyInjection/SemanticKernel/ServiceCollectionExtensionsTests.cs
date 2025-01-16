using AIToolbox.Options;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AIToolbox.DependencyInjection.SemanticKernel;

public class ServiceCollectionExtensionsTests
{
    private readonly ServiceCollection _services = [];

    [Fact]
    public void Should_Add_AIToolbox_With_Default_Options()
    {
        // Act
        var builder = _services.AddAIToolbox();

        // Assert
        builder.Should().NotBeNull();
    }

    [Fact]
    public void Should_Add_AIToolbox_With_Custom_Options()
    {
        // Act
        var builder = _services.AddAIToolbox(new AIToolboxOptions());

        // Assert
        builder.Should().NotBeNull();
    }

    [Fact]
    public void Should_Throw_Exception_When_Options_Are_Null()
    {
        // Act
        var act = () => _services.AddAIToolbox((AIToolboxOptions)null!);

        // Assert
        act.Should().Throw<ArgumentNullException>().WithMessage("*options*");
    }

    [Fact]
    public void Should_Add_AIToolbox_With_Options_Action()
    {
        // Act
        var builder = _services.AddAIToolbox((Action<AIToolboxOptions>)(options => { }));

        // Assert
        builder.Should().NotBeNull();
    }

    [Fact]
    public void Should_Throw_Exception_When_Options_Action_Is_Null()
    {
        // Act
        var act = () => _services.AddAIToolbox((Action<AIToolboxOptions>)null!);

        // Assert
        act.Should().Throw<ArgumentNullException>().WithMessage("*optionsAction*");
    }

    [Fact]
    public void Should_Add_AIToolbox_With_Configuration_And_Default_Section()
    {
        // Arrange
        const string section = "AIToolbox";
        var configuration = GetConfiguration(section);

        // Act
        var builder = _services.AddAIToolbox(configuration);

        // Assert
        builder.Should().NotBeNull();
    }

    [Fact]
    public void Should_Add_AIToolbox_With_Configuration_And_Custom_Section()
    {
        // Arrange
        const string section = "CustomSection";
        var configuration = GetConfiguration(section);

        // Act
        var builder = _services.AddAIToolbox(configuration, section);

        // Assert
        builder.Should().NotBeNull();
    }

    private static IConfiguration GetConfiguration(string section) =>
        new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string>
            {
                [$"{section}:Key"] = "Value"
            }!)
            .Build();
}
