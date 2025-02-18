using AIToolbox.Options;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;

namespace AIToolbox.DependencyInjection;

public class AIToolboxBuilderTests
{
    public static TheoryData<Action, string> ConstructWithNullParameters =>
        new()
        {
            { () => _ = new AIToolboxBuilder(null!, null!), "options" },
            { () => _ = new AIToolboxBuilder(new AIToolboxOptions(), null!), "services" }
        };

    [Theory]
    [MemberData(nameof(ConstructWithNullParameters))]
    public void Should_Throw_On_Construct_With_Null_Parameters(Action act, string parameterName)
    {
        // Assert
        act.Should().Throw<ArgumentNullException>().WithParameterName(parameterName);
    }

    [Fact]
    public void Should_Construct_With_Valid_Parameters()
    {
        // Arrange
        var options = new AIToolboxOptions();
        var services = new ServiceCollection();

        // Act
        var builder = new AIToolboxBuilder(options, services);

        // Assert
        builder.Options.Should().Be(options);
        builder.Services.Should().BeEquivalentTo(services);
    }
}
