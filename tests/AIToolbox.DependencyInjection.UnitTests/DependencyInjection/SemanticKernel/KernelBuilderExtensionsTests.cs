using FluentAssertions;

namespace AIToolbox.DependencyInjection.SemanticKernel;

public class KernelBuilderExtensionsTests
{
    [Theory]
    [MemberData(nameof(KernelBuilderExtensionsTestData.AddConnectorWithNullBuilder), MemberType = typeof(KernelBuilderExtensionsTestData))]
    public void Should_Throw_On_Add_Connector_With_Null_Builder(Action act)
    {
        // Assert
        act.Should().Throw<ArgumentNullException>().WithParameterName("builder");
    }

    [Theory]
    [MemberData(nameof(KernelBuilderExtensionsTestData.AddConnectorWithNullParameters), MemberType = typeof(KernelBuilderExtensionsTestData))]
    public void Should_Throw_On_Add_Connector_With_Null_Parameters(Action act, string parameterName)
    {
        // Assert
        act.Should().Throw<ArgumentNullException>().WithParameterName(parameterName);
    }

    [Theory]
    [MemberData(nameof(KernelBuilderExtensionsTestData.AddConnectorWithNoDefaultOptions), MemberType = typeof(KernelBuilderExtensionsTestData))]
    public void Should_Throw_On_Add_Connector_With_No_Default_Options(Action act, string message)
    {
        // Assert
        act.Should().Throw<InvalidOperationException>().WithMessage(message);
    }

    [Theory]
    [MemberData(nameof(KernelBuilderExtensionsTestData.AddConnectorWithDefaultOptions), MemberType = typeof(KernelBuilderExtensionsTestData))]
    public void Should_Add_Connector_With_Default_Options(
        Func<IKernelBuilder> func,
        IKernelBuilder builder,
        Action assertAct)
    {
        // Act
        var result = func();

        // Assert
        result.Should().Be(builder);
        assertAct();
    }

    [Theory]
    [MemberData(nameof(KernelBuilderExtensionsTestData.AddConnectorWithCustomOptions), MemberType = typeof(KernelBuilderExtensionsTestData))]
    public void Should_Add_Connector_With_Custom_Options(
        Func<object, IKernelBuilder> func,
        IKernelBuilder builder,
        object options,
        Action assertAct)
    {
        // Act
        var result = func(options);

        // Assert
        result.Should().Be(builder);
        assertAct();
    }

    [Theory]
    [MemberData(nameof(KernelBuilderExtensionsTestData.AddConnectorWithOptionsAction), MemberType = typeof(KernelBuilderExtensionsTestData))]
    public void Should_Add_Connector_With_Options_Action(
        Func<IKernelBuilder> func,
        IKernelBuilder builder,
        Action assertAct)
    {
        // Act
        var result = func();

        // Assert
        result.Should().Be(builder);
        assertAct();
    }
}
