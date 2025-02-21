using AIToolbox.Options.SemanticKernel;
using FluentAssertions;

namespace AIToolbox.SemanticKernel.Kernel;

public class KernelProviderTests
{
    public static TheoryData<Action, string> ConstructWithNullParameters =>
        new()
        {
            { () => _ = new KernelProvider(null!, null!), "options" },
            { () => _ = new KernelProvider(new KernelOptions(), null!), "configurators" }
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
        var options = new KernelOptions();
        var configurators = Enumerable.Empty<IKernelBuilderConfigurator>();

        // Act
        _ = new KernelProvider(options, configurators);
    }

    [Fact]
    public void Should_Get_Kernel()
    {
        // Arrange
        var options = new KernelOptions();
        var configurators = Enumerable.Empty<IKernelBuilderConfigurator>();
        var kernelProvider = new KernelProvider(options, configurators);

        // Act
        var kernel = kernelProvider.GetKernel();

        // Assert
        kernel.Should().NotBeNull();
    }
}
