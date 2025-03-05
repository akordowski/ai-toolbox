using FluentAssertions;

namespace AIToolbox.DependencyInjection.SemanticKernel;

public class MemoryBuilderExtensionsTests
{
    [Theory]
    [MemberData(nameof(MemoryBuilderExtensionsTestData.AddMemoryWithNullBuilder), MemberType = typeof(MemoryBuilderExtensionsTestData))]
    public void Should_Throw_On_Add_Memory_With_Null_Builder(Action act)
    {
        // Assert
        act.Should().Throw<ArgumentNullException>().WithParameterName("builder");
    }

    [Theory]
    [MemberData(nameof(MemoryBuilderExtensionsTestData.AddMemoryWithNullParameters), MemberType = typeof(MemoryBuilderExtensionsTestData))]
    public void Should_Throw_On_Add_Memory_With_Null_Parameters(Action act, string parameterName)
    {
        // Assert
        act.Should().Throw<ArgumentNullException>().WithParameterName(parameterName);
    }

    [Theory]
    [MemberData(nameof(MemoryBuilderExtensionsTestData.AddMemoryWithNoDefaultOptions), MemberType = typeof(MemoryBuilderExtensionsTestData))]
    public void Should_Throw_On_Add_Memory_With_No_Default_Options(Action act, string message)
    {
        // Assert
        act.Should().Throw<InvalidOperationException>().WithMessage(message);
    }

    [Theory]
    [MemberData(nameof(MemoryBuilderExtensionsTestData.AddMemoryWithDefaultOptions), MemberType = typeof(MemoryBuilderExtensionsTestData))]
    public void Should_Add_Memory_With_Default_Options(
        Func<IMemoryBuilder> func,
        IMemoryBuilder builder,
        Action assertAct)
    {
        // Act
        var result = func();

        // Assert
        result.Should().Be(builder);
        assertAct();
    }

    [Theory]
    [MemberData(nameof(MemoryBuilderExtensionsTestData.AddMemoryWithCustomOptions), MemberType = typeof(MemoryBuilderExtensionsTestData))]
    public void Should_Add_Memory_With_Custom_Options(
        Func<object, IMemoryBuilder> func,
        IMemoryBuilder builder,
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
    [MemberData(nameof(MemoryBuilderExtensionsTestData.AddMemoryWithOptionsAction), MemberType = typeof(MemoryBuilderExtensionsTestData))]
    public void Should_Add_Memory_With_Options_Action(
        Func<IMemoryBuilder> func,
        IMemoryBuilder builder,
        Action assertAct)
    {
        // Act
        var result = func();

        // Assert
        result.Should().Be(builder);
        assertAct();
    }
}
