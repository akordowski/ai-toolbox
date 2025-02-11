using AIToolbox.Options.SemanticKernel;
using AIToolbox.SemanticKernel;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.SemanticKernel;
using Moq;

namespace AIToolbox.DependencyInjection.SemanticKernel;

public class KernelBuilderTests
{
    private readonly KernelOptions _options = new();
    private readonly ServiceCollection _services = [];
    private readonly KernelBuilder _builder;

    public KernelBuilderTests()
    {
        _builder = new KernelBuilder(_options, _services);
    }

    public static TheoryData<Action, string, string> BuilderWithInvalidParameters =>
        new()
        {
            { () => _ = new KernelBuilder(null!, null!), "options", "No 'KernelOptions' provided.*" },
            { () => _ = new KernelBuilder(new KernelOptions(), null!), "services", "*" }
        };

    public static TheoryData<Action, string> MethodsWithInvalidParameters
    {
        get
        {
            var builder = new KernelBuilder(new KernelOptions(), new ServiceCollection());

            return new TheoryData<Action, string>
            {
                { () => builder.WithCustomAIServiceSelector((Func<IServiceProvider, IAIServiceSelector>)null!), "factory" },
                { () => builder.WithCustomAIServiceSelector((IAIServiceSelector)null!), "instance" },

                { () => builder.WithCustomFunctionInvocationFilter((Func<IServiceProvider, IFunctionInvocationFilter>)null!), "factory" },
                { () => builder.WithCustomFunctionInvocationFilter((IFunctionInvocationFilter)null!), "instance" }
            };
        }
    }

    [Theory]
    [MemberData(nameof(BuilderWithInvalidParameters))]
    public void Should_Throw_On_Construct_With_Invalid_Parameters(Action act, string parameterName, string message)
    {
        // Assert
        act.Should().Throw<ArgumentNullException>()
            .WithParameterName(parameterName)
            .WithMessage(message);
    }

    [Fact]
    public void Should_Construct_With_Valid_Parameters()
    {
        // Arrange
        var services = new ServiceCollection();

        // Act
        _ = new KernelBuilder(_options, services);

        // Assert
        services.Should().ContainSingle(descriptor => descriptor.ServiceType == typeof(KernelOptions) &&
                                                      descriptor.Lifetime == ServiceLifetime.Singleton);

        services.Should().ContainSingle(descriptor => descriptor.ServiceType == typeof(IKernelProvider) &&
                                                      descriptor.Lifetime == ServiceLifetime.Singleton);
    }

    [Theory]
    [MemberData(nameof(MethodsWithInvalidParameters))]
    public void Should_Throw_On_Methods_With_Invalid_Parameters(Action act, string parameterName)
    {
        // Assert
        act.Should().Throw<ArgumentNullException>().WithParameterName(parameterName);
    }

    [Fact]
    public void Should_Add_Custom_AIServiceSelector_By_Factory()
    {
        // Act
        var result = _builder.WithCustomAIServiceSelector(_ => Mock.Of<IAIServiceSelector>());

        // Assert
        result.Should().Be(_builder);

        _services.Should().ContainSingle(descriptor => descriptor.ServiceType == typeof(IKernelBuilderConfigurator) &&
                                                       descriptor.Lifetime == ServiceLifetime.Singleton);
    }

    [Fact]
    public void Should_Add_Custom_AIServiceSelector_As_Instance()
    {
        // Act
        var result = _builder.WithCustomAIServiceSelector(Mock.Of<IAIServiceSelector>());

        // Assert
        result.Should().Be(_builder);

        _services.Should().ContainSingle(descriptor => descriptor.ServiceType == typeof(IKernelBuilderConfigurator) &&
                                                       descriptor.Lifetime == ServiceLifetime.Singleton);
    }

    [Fact]
    public void Should_Add_Custom_FunctionInvocationFilter_By_Factory()
    {
        // Act
        var result = _builder.WithCustomFunctionInvocationFilter(_ => Mock.Of<IFunctionInvocationFilter>());

        // Assert
        result.Should().Be(_builder);

        _services.Should().ContainSingle(descriptor => descriptor.ServiceType == typeof(IKernelBuilderConfigurator) &&
                                                       descriptor.Lifetime == ServiceLifetime.Singleton);
    }

    [Fact]
    public void Should_Add_Custom_FunctionInvocationFilter_As_Instance()
    {
        // Act
        var result = _builder.WithCustomFunctionInvocationFilter(Mock.Of<IFunctionInvocationFilter>());

        // Assert
        result.Should().Be(_builder);

        _services.Should().ContainSingle(descriptor => descriptor.ServiceType == typeof(IKernelBuilderConfigurator) &&
                                                       descriptor.Lifetime == ServiceLifetime.Singleton);
    }
}
