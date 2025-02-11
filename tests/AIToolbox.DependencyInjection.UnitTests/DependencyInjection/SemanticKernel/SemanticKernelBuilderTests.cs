using AIToolbox.Agents.ChatCompletion;
using AIToolbox.Options.SemanticKernel;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;

namespace AIToolbox.DependencyInjection.SemanticKernel;

public class SemanticKernelBuilderTests
{
    private readonly SemanticKernelOptions _options = new();
    private readonly ServiceCollection _services = [];
    private readonly SemanticKernelBuilder _builder;

    public SemanticKernelBuilderTests()
    {
        _builder = new SemanticKernelBuilder(_options, _services);
    }

    public static TheoryData<Action, string, string> BuilderWithInvalidParameters =>
        new()
        {
            { () => _ = new SemanticKernelBuilder(null!, null!), "options", "No 'SemanticKernelOptions' provided.*" },
            { () => _ = new SemanticKernelBuilder(new SemanticKernelOptions(), null!), "services", "*" }
        };

    public static TheoryData<Action, string> MethodsWithInvalidParameters
    {
        get
        {
            var builder = new SemanticKernelBuilder(new SemanticKernelOptions(), new ServiceCollection());

            return new TheoryData<Action, string>
            {
                { () => builder.AddKernel((Action<KernelOptions>)null!), "optionsAction" },
                { () => builder.AddKernel((Action<IKernelBuilder>)null!), "builderAction" },
                { () => builder.AddKernel((Action<IKernelBuilder, KernelOptions>)null!), "action" },

                { () => builder.AddMemory((Action<MemoryOptions>)null!), "optionsAction" },
                { () => builder.AddMemory((Action<IMemoryBuilder>)null!), "builderAction" },
                { () => builder.AddMemory((Action<IMemoryBuilder, MemoryOptions>)null!), "action" },

                { () => builder.AddChatCompletion((Action<ChatCompletionOptions>)null!), "optionsAction" },
                { () => builder.AddChatCompletion((Action<IChatCompletionBuilder>)null!), "builderAction" },
                { () => builder.AddChatCompletion((Action<IChatCompletionBuilder, ChatCompletionOptions>)null!), "action" }
            };
        }
    }

    public static TheoryData<Action, string> MethodsWithoutDefaultOptions
    {
        get
        {
            var builder = new SemanticKernelBuilder(new SemanticKernelOptions(), new ServiceCollection());
            const string kernelOptionsMessage = "No 'KernelOptions' provided.*";
            const string memoryOptionsMessage = "No 'MemoryOptions' provided.*";
            const string chatCompletionOptionsMessage = "No 'ChatCompletionOptions' provided.*";

            return new TheoryData<Action, string>
            {
                { () => builder.AddKernel(), kernelOptionsMessage },
                { () => builder.AddKernel((IKernelBuilder _) => {}), kernelOptionsMessage },

                { () => builder.AddMemory(), memoryOptionsMessage },
                { () => builder.AddMemory((IMemoryBuilder _) => {}), memoryOptionsMessage },

                { () => builder.AddChatCompletion(), chatCompletionOptionsMessage },
                { () => builder.AddChatCompletion((IChatCompletionBuilder _) => {}), chatCompletionOptionsMessage }
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
        _ = new SemanticKernelBuilder(_options, services);
    }

    [Theory]
    [MemberData(nameof(MethodsWithInvalidParameters))]
    public void Should_Throw_On_Methods_With_Invalid_Parameters(Action act, string parameterName)
    {
        // Assert
        act.Should().Throw<ArgumentNullException>().WithParameterName(parameterName);
    }

    [Theory]
    [MemberData(nameof(MethodsWithoutDefaultOptions))]
    public void Should_Throw_On_Methods_Without_Default_Options(Action act, string message)
    {
        // Assert
        act.Should().Throw<ArgumentNullException>().WithMessage(message);
    }

    [Fact]
    public void Should_Add_Kernel_With_Default_Options()
    {
        // Arrange
        _options.Kernel = new KernelOptions();

        // Act
        var result = _builder.AddKernel();

        // Assert
        result.Should().Be(_builder);
        _options.Kernel.Should().NotBeNull();

        AssertAddKernel();
    }

    [Fact]
    public void Should_Add_Kernel_With_Custom_Options()
    {
        // Arrange
        var options = new KernelOptions();

        // Act
        var result = _builder.AddKernel(options);

        // Assert
        result.Should().Be(_builder);
        _options.Kernel.Should().Be(options);

        AssertAddKernel(options);
    }

    [Fact]
    public void Should_Add_Kernel_With_Options_Action()
    {
        // Act
        var result = _builder.AddKernel(options => { options.AddLogging = true; });

        // Assert
        result.Should().Be(_builder);
        _options.Kernel!.AddLogging.Should().BeTrue();

        AssertAddKernel();
    }

    [Fact]
    public void Should_Add_Kernel_With_Builder_And_Default_Options()
    {
        // Arrange
        _options.Kernel = new KernelOptions();

        // Act
        var result = _builder.AddKernel((IKernelBuilder builder) => { });

        // Assert
        result.Should().Be(_builder);
        _options.Kernel.Should().NotBeNull();

        AssertAddKernel();
    }

    [Fact]
    public void Should_Add_Kernel_With_Builder_And_Custom_Options()
    {
        // Arrange
        var options = new KernelOptions();

        // Act
        var result = _builder.AddKernel(builder => { }, options);

        // Assert
        result.Should().Be(_builder);
        _options.Kernel.Should().Be(options);

        AssertAddKernel(options);
    }

    [Fact]
    public void Should_Add_Kernel_With_Builder_And_Options_Action()
    {
        // Act
        var result = _builder.AddKernel((builder, options) => { options.AddLogging = true; });

        // Assert
        result.Should().Be(_builder);
        _options.Kernel!.AddLogging.Should().BeTrue();

        AssertAddKernel();
    }

    [Fact]
    public void Should_Add_Memory_With_Default_Options()
    {
        // Arrange
        _options.Memory = new MemoryOptions();

        // Act
        var result = _builder.AddMemory();

        // Assert
        result.Should().Be(_builder);
        _options.Memory.Should().NotBeNull();

        AssertAddMemory();
    }

    [Fact]
    public void Should_Add_Memory_With_Custom_Options()
    {
        // Arrange
        var options = new MemoryOptions();

        // Act
        var result = _builder.AddMemory(options);

        // Assert
        result.Should().Be(_builder);
        _options.Memory.Should().Be(options);

        AssertAddMemory(options);
    }

    [Fact]
    public void Should_Add_Memory_With_Options_Action()
    {
        // Arrange
        var memoryStoreOptions = new MemoryStoreOptions();

        // Act
        var result = _builder.AddMemory(options => { options.Store = memoryStoreOptions; });

        // Assert
        result.Should().Be(_builder);
        _options.Memory!.Store.Should().Be(memoryStoreOptions);

        AssertAddMemory();
    }

    [Fact]
    public void Should_Add_Memory_With_Builder_And_Default_Options()
    {
        // Arrange
        _options.Memory = new MemoryOptions();

        // Act
        var result = _builder.AddMemory((IMemoryBuilder builder) => { });

        // Assert
        result.Should().Be(_builder);
        _options.Memory.Should().NotBeNull();

        AssertAddMemory();
    }

    [Fact]
    public void Should_Add_Memory_With_Builder_And_Custom_Options()
    {
        // Arrange
        var options = new MemoryOptions();

        // Act
        var result = _builder.AddMemory(builder => { }, options);

        // Assert
        result.Should().Be(_builder);
        _options.Memory.Should().Be(options);

        AssertAddMemory(options);
    }

    [Fact]
    public void Should_Add_Memory_With_Builder_And_Options_Action()
    {
        // Arrange
        var memoryStoreOptions = new MemoryStoreOptions();

        // Act
        var result = _builder.AddMemory((builder, options) => { options.Store = memoryStoreOptions; });

        // Assert
        result.Should().Be(_builder);
        _options.Memory!.Store.Should().Be(memoryStoreOptions);

        AssertAddMemory();
    }

    [Fact]
    public void Should_Add_ChatCompletion_With_Default_Options()
    {
        // Arrange
        _options.Agents = new AgentOptions
        {
            ChatCompletion = new ChatCompletionOptions()
        };

        // Act
        var result = _builder.AddChatCompletion();

        // Assert
        result.Should().Be(_builder);
        _options.Agents.ChatCompletion.Should().NotBeNull();

        AssertAddChatCompletion();
    }

    [Fact]
    public void Should_Add_ChatCompletion_With_Custom_Options()
    {
        // Arrange
        var options = new ChatCompletionOptions();

        // Act
        var result = _builder.AddChatCompletion(options);

        // Assert
        result.Should().Be(_builder);
        _options.Agents!.ChatCompletion.Should().Be(options);

        AssertAddChatCompletion();
    }

    [Fact]
    public void Should_Add_ChatCompletion_With_Options_Action()
    {
        // Arrange
        var chatHistoryOptions = new ChatHistoryOptions();

        // Act
        var result = _builder.AddChatCompletion(options => { options.ChatHistory = chatHistoryOptions; });

        // Assert
        result.Should().Be(_builder);
        _options.Agents!.ChatCompletion!.ChatHistory.Should().Be(chatHistoryOptions);

        AssertAddChatCompletion();
    }

    [Fact]
    public void Should_Add_ChatCompletion_With_Builder_And_Default_Options()
    {
        // Arrange
        _options.Agents = new AgentOptions
        {
            ChatCompletion = new ChatCompletionOptions()
        };

        // Act
        var result = _builder.AddChatCompletion((IChatCompletionBuilder builder) => { });

        // Assert
        result.Should().Be(_builder);
        _options.Agents.ChatCompletion.Should().NotBeNull();

        AssertAddChatCompletion();
    }

    [Fact]
    public void Should_Add_ChatCompletion_With_Builder_And_Custom_Options()
    {
        // Arrange
        var options = new ChatCompletionOptions();

        // Act
        var result = _builder.AddChatCompletion(builder => { }, options);

        // Assert
        result.Should().Be(_builder);
        _options.Agents!.ChatCompletion.Should().Be(options);

        AssertAddChatCompletion();
    }

    [Fact]
    public void Should_Add_ChatCompletion_With_Builder_And_Options_Action()
    {
        // Arrange
        var chatHistoryOptions = new ChatHistoryOptions();

        // Act
        var result = _builder.AddChatCompletion((builder, options) => { options.ChatHistory = chatHistoryOptions; });

        // Assert
        result.Should().Be(_builder);
        _options.Agents!.ChatCompletion!.ChatHistory.Should().Be(chatHistoryOptions);

        AssertAddChatCompletion();
    }

    private void AssertAddKernel(KernelOptions? options = null)
    {
        if (options is null)
        {
            _services.Should().ContainSingle(descriptor => descriptor.ServiceType == typeof(KernelOptions) &&
                                                           descriptor.Lifetime == ServiceLifetime.Singleton &&
                                                           descriptor.ImplementationInstance != null);
        }
        else
        {
            _services.Should().ContainSingle(descriptor => descriptor.ServiceType == typeof(KernelOptions) &&
                                                           descriptor.Lifetime == ServiceLifetime.Singleton &&
                                                           descriptor.ImplementationInstance == options);
        }
    }

    private void AssertAddMemory(MemoryOptions? options = null)
    {
        if (options is null)
        {
            _services.Should().ContainSingle(descriptor => descriptor.ServiceType == typeof(MemoryOptions) &&
                                                           descriptor.Lifetime == ServiceLifetime.Singleton &&
                                                           descriptor.ImplementationInstance != null);
        }
        else
        {
            _services.Should().ContainSingle(descriptor => descriptor.ServiceType == typeof(MemoryOptions) &&
                                                           descriptor.Lifetime == ServiceLifetime.Singleton &&
                                                           descriptor.ImplementationInstance == options);
        }
    }

    private void AssertAddChatCompletion()
    {
        _services.Should().ContainSingle(descriptor => descriptor.ServiceType == typeof(IChatAgent) &&
                                                       descriptor.Lifetime == ServiceLifetime.Scoped);
    }
}
