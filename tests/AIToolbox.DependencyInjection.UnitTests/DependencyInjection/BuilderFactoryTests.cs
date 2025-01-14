using AIToolbox.Options;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;

namespace AIToolbox.DependencyInjection;

public class BuilderFactoryTests
{
    [Fact]
    public void Should_Throw_Exception_When_Constructed_With_Null_Options()
    {
        // Act
        var act = () => new BuilderFactory(null!, null!);

        // Assert
        act.Should().Throw<ArgumentNullException>().WithMessage("*options*");
    }

    [Fact]
    public void Should_Throw_Exception_When_Constructed_With_Null_Services()
    {
        // Arrange
        var options = new AIToolboxOptions();

        // Act
        var act = () => new BuilderFactory(options, null!);

        // Assert
        act.Should().Throw<ArgumentNullException>().WithMessage("*services*");
    }

    [Fact]
    public void Should_Construct_With_Valid_Parameters()
    {
        // Arrange
        var options = new AIToolboxOptions();
        var services = new ServiceCollection();

        // Act
        var builderFactory = new BuilderFactory(options, services);

        // Assert
        builderFactory.Options.Should().Be(options);
        builderFactory.Services.Should().BeEquivalentTo(services);
    }
}
