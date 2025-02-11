using AIToolbox.Agents.ChatCompletion;
using AIToolbox.Agents.ChatCompletion.Resources;
using AIToolbox.Agents.ChatCompletion.Services;
using AIToolbox.Data;
using AIToolbox.Options;
using AIToolbox.Options.DataStorage;
using AIToolbox.Options.SemanticKernel;
using AIToolbox.SemanticKernel;
using AIToolbox.SemanticKernel.ChatCompletion;
using AIToolbox.SemanticKernel.Memory;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Memory;
using Moq;

namespace AIToolbox.DependencyInjection.SemanticKernel;

public class ChatCompletionBuilderTests
{
    private readonly ChatCompletionOptions _options = new()
    {
        ChatHistory = new ChatHistoryOptions(),
        MemorySearch = new MemorySearchOptions(),
        PromptExecution = new PromptExecutionOptions()
    };
    private readonly ServiceCollection _services = [];
    private readonly ChatCompletionBuilder _builder;

    public ChatCompletionBuilderTests()
    {
        _builder = new ChatCompletionBuilder(_options, _services);
    }

    public static TheoryData<Action, string, string> BuilderWithInvalidParameters =>
        new()
        {
            { () => _ = new ChatCompletionBuilder(null!, null!), "options", "No 'ChatCompletionOptions' provided.*" },
            { () => _ = new ChatCompletionBuilder(new ChatCompletionOptions(), null!), "services", "*" }
        };

    public static TheoryData<Action, string> MethodsWithInvalidParameters
    {
        get
        {
            var builder = new ChatCompletionBuilder(new ChatCompletionOptions(), new ServiceCollection());

            return new TheoryData<Action, string>
            {
                { () => builder.WithSimpleDataStorage((Action<SimpleDataStorageOptions>)null!), "optionsAction" }
            };
        }
    }

    public static TheoryData<ClassOptions> CustomChatHistoryRetrieverOptions =>
    [
        new() { AssemblyQualifiedName = typeof(CustomChatHistoryRetriever).AssemblyQualifiedName },
        new() { Type = typeof(CustomChatHistoryRetriever) }
    ];

    public static TheoryData<ClassOptions> CustomPromptExecutionSettingsRetrieverOptions =>
    [
        new() { AssemblyQualifiedName = typeof(CustomPromptExecutionSettingsRetriever).AssemblyQualifiedName },
        new() { Type = typeof(CustomPromptExecutionSettingsRetriever) }
    ];

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
        var builder = new ChatCompletionBuilder(_options, services);

        // Assert
        services.Should().ContainSingle(descriptor => descriptor.ServiceType == typeof(IChatResource) &&
                                                      descriptor.Lifetime == ServiceLifetime.Scoped);

        services.Should().ContainSingle(descriptor => descriptor.ServiceType == typeof(IMessageResource) &&
                                                      descriptor.Lifetime == ServiceLifetime.Scoped);

        services.Should().ContainSingle(descriptor => descriptor.ServiceType == typeof(IParticipantResource) &&
                                                      descriptor.Lifetime == ServiceLifetime.Scoped);

        services.Should().ContainSingle(descriptor => descriptor.ServiceType == typeof(ISettingsResource) &&
                                                      descriptor.Lifetime == ServiceLifetime.Scoped);

        services.Should().ContainSingle(descriptor => descriptor.ServiceType == typeof(IPersistentChatAgentService) &&
                                                      descriptor.Lifetime == ServiceLifetime.Scoped);

        services.Should().ContainSingle(descriptor => descriptor.ServiceType == typeof(IChatAgent) &&
                                                      descriptor.Lifetime == ServiceLifetime.Scoped);

        services.Should().ContainSingle(descriptor => descriptor.ServiceType == typeof(IPersistentChatAgent) &&
                                                      descriptor.Lifetime == ServiceLifetime.Scoped);

        services.Should().ContainSingle(descriptor => descriptor.ServiceType == typeof(ChatHistoryOptions) &&
                                                      descriptor.Lifetime == ServiceLifetime.Singleton);

        services.Should().ContainSingle(descriptor => descriptor.ServiceType == typeof(MemorySearchOptions) &&
                                                      descriptor.Lifetime == ServiceLifetime.Singleton);

        services.Should().ContainSingle(descriptor => descriptor.ServiceType == typeof(PromptExecutionOptions) &&
                                                      descriptor.Lifetime == ServiceLifetime.Singleton);

        services.Should().ContainSingle(descriptor => descriptor.ServiceType == typeof(IChatHistoryRetriever) &&
                                                      descriptor.Lifetime == ServiceLifetime.Scoped);

        services.Should().ContainSingle(descriptor => descriptor.ServiceType == typeof(IPromptExecutionSettingsRetriever) &&
                                                      descriptor.Lifetime == ServiceLifetime.Scoped);
    }

    [Theory]
    [MemberData(nameof(MethodsWithInvalidParameters))]
    public void Should_Throw_On_Methods_With_Invalid_Parameters(Action act, string parameterName)
    {
        // Assert
        act.Should().Throw<ArgumentNullException>().WithParameterName(parameterName);
    }

    [Theory]
    [MemberData(nameof(CustomChatHistoryRetrieverOptions))]
    public void Should_Add_Custom_ChatHistoryRetriever(ClassOptions options)
    {
        // Arrange
        _options.ChatHistoryRetriever = options;

        var services = new ServiceCollection();
        _ = new ChatCompletionAgentServiceBuilder(_options, services);

        // Act
        // No additional action needed as services are added in the constructor

        // Assert
        services.Should().ContainSingle(descriptor => descriptor.ServiceType == typeof(IChatHistoryRetriever) &&
                                                      descriptor.ImplementationFactory != null &&
                                                      descriptor.Lifetime == ServiceLifetime.Scoped);
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void Should_Add_Default_ChatHistoryRetriever_If_Options_Are_Not_Provided(bool defaultOptions)
    {
        // Arrange
        _options.ChatHistoryRetriever = defaultOptions
            ? new ClassOptions()
            : null;

        var services = new ServiceCollection();
        _ = new ChatCompletionAgentServiceBuilder(_options, services);

        // Act
        // No additional action needed as services are added in the constructor

        // Assert
        services.Should().ContainSingle(descriptor => descriptor.ServiceType == typeof(IChatHistoryRetriever) &&
                                                      descriptor.ImplementationType == typeof(ChatHistoryRetriever) &&
                                                      descriptor.Lifetime == ServiceLifetime.Scoped);
    }

    [Theory]
    [MemberData(nameof(CustomPromptExecutionSettingsRetrieverOptions))]
    public void Should_Add_Custom_PromptExecutionSettingsRetriever(ClassOptions options)
    {
        // Arrange
        _options.PromptExecutionSettingsRetriever = options;

        var services = new ServiceCollection();
        _ = new ChatCompletionAgentServiceBuilder(_options, services);

        // Act
        // No additional action needed as services are added in the constructor

        // Assert
        services.Should().ContainSingle(descriptor => descriptor.ServiceType == typeof(IPromptExecutionSettingsRetriever) &&
                                                      descriptor.ImplementationFactory != null &&
                                                      descriptor.Lifetime == ServiceLifetime.Scoped);
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void Should_Add_Default_PromptExecutionSettingsRetriever_If_Options_Are_Not_Provided(bool defaultOptions)
    {
        // Arrange
        _options.PromptExecutionSettingsRetriever = defaultOptions
            ? new ClassOptions()
            : null;

        var services = new ServiceCollection();
        _ = new ChatCompletionAgentServiceBuilder(_options, services);

        // Act
        // No additional action needed as services are added in the constructor

        // Assert
        services.Should().ContainSingle(descriptor => descriptor.ServiceType == typeof(IPromptExecutionSettingsRetriever) &&
                                                      descriptor.ImplementationType == typeof(PromptExecutionSettingsRetriever) &&
                                                      descriptor.Lifetime == ServiceLifetime.Scoped);
    }

    [Fact]
    public void Should_Throw_When_Add_SemanticTextMemoryRetriever_And_Memory_Is_Not_Registered()
    {
        // Arrange
        const string message = "To use the 'SemanticTextMemoryRetriever' with the chat completion, " +
                               "include the memory first using the 'AddMemory()' method.";

        // Act
        var act = _builder.WithSemanticTextMemoryRetriever;

        // Assert
        act.Should().Throw<InvalidOperationException>().WithMessage(message);
    }

    [Fact]
    public void Should_Add_SemanticTextMemoryRetriever_When_Memory_Is_Registered()
    {
        // Arrange
        _services.AddSingleton(Mock.Of<IMemoryProvider>());

        // Act
        var result = _builder.WithSemanticTextMemoryRetriever();

        // Assert
        result.Should().Be(_builder);

        _services.Should().ContainSingle(descriptor => descriptor.ServiceType == typeof(ISemanticTextMemoryRetriever) &&
                                                       descriptor.Lifetime == ServiceLifetime.Scoped);
    }

    [Fact]
    public void Should_Add_SimpleDataStorage_With_Default_Options()
    {
        // Arrange
        _options.DataStorage = new DataStorageOptions
        {
            SimpleDataStorage = new SimpleDataStorageOptions()
        };

        // Act
        var result = _builder.WithSimpleDataStorage();

        // Assert
        result.Should().Be(_builder);
        _options.DataStorage.SimpleDataStorage.Should().NotBeNull();

        AssertSimpleDataStorage();
    }

    [Fact]
    public void Should_Add_SimpleDataStorage_With_Custom_Options()
    {
        // Arrange
        var options = new SimpleDataStorageOptions();

        // Act
        var result = _builder.WithSimpleDataStorage(options);

        // Assert
        result.Should().Be(_builder);
        _options.DataStorage!.SimpleDataStorage.Should().Be(options);

        AssertSimpleDataStorage();
    }

    [Fact]
    public void Should_Add_SimpleDataStorage_With_Options_Action()
    {
        // Act
        var result = _builder.WithSimpleDataStorage(options => options.StorageType = StorageType.Volatile);

        // Assert
        result.Should().Be(_builder);
        _options.DataStorage!.SimpleDataStorage!.StorageType.Should().Be(StorageType.Volatile);

        AssertSimpleDataStorage();
    }

    private void AssertSimpleDataStorage()
    {
        _services.Should().ContainSingle(descriptor => descriptor.ServiceType == typeof(SimpleDataStorageOptions) &&
                                                       descriptor.Lifetime == ServiceLifetime.Singleton);

        _services.Should().ContainSingle(descriptor => descriptor.ServiceType == typeof(IDataStorage) &&
                                                       descriptor.Lifetime == ServiceLifetime.Scoped);
    }

    private class CustomChatHistoryRetriever : IChatHistoryRetriever
    {
        public ChatHistory GetChatHistory(
            IEnumerable<MemoryQueryResult> memories,
            IEnumerable<ChatMessageContent> messages,
            ChatHistoryOptions? options = null) => [];
    }

    private class CustomPromptExecutionSettingsRetriever : IPromptExecutionSettingsRetriever
    {
        public PromptExecutionSettings? GetPromptExecutionSettings(
            Kernel kernel,
            Type serviceType,
            PromptExecutionOptions? options = null) => null;
    }
}
