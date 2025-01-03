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

public class ChatCompletionAgentServiceBuilderTests
{
    private readonly ChatCompletionOptions _options = new()
    {
        ChatHistory = new ChatHistoryOptions(),
        MemorySearch = new MemorySearchOptions(),
        PromptExecution = new PromptExecutionOptions()
    };
    private readonly ServiceCollection _services = [];
    private readonly ChatCompletionAgentServiceBuilder _builder;

    public ChatCompletionAgentServiceBuilderTests()
    {
        _builder = new ChatCompletionAgentServiceBuilder(_options, _services);
    }

    [Fact]
    public void Should_Construct_With_Valid_Parameters()
    {
        // Arrange
        var services = new ServiceCollection();

        // Act
        var builder = new ChatCompletionAgentServiceBuilder(_options, services);

        // Assert
        builder.Options.Should().Be(_options);
        builder.Services.Should().BeEquivalentTo(services);

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

    [Fact]
    public void Should_Throw_Exception_When_Constructed_With_Null_Options()
    {
        // Act
        var act = () => new ChatCompletionAgentServiceBuilder(null!, _services);

        // Assert
        act.Should().Throw<ArgumentNullException>().WithMessage("*options*");
    }

    [Fact]
    public void Should_Throw_Exception_When_Constructed_With_Null_Services()
    {
        // Act
        var act = () => new ChatCompletionAgentServiceBuilder(_options, null!);

        // Assert
        act.Should().Throw<ArgumentNullException>().WithMessage("*services*");
    }

    [Fact]
    public void Should_Add_Custom_ChatHistoryRetriever_By_AssemblyQualifiedName()
    {
        // Arrange
        _options.ChatHistoryRetriever = new ClassOptions
        {
            AssemblyQualifiedName = typeof(CustomChatHistoryRetriever).AssemblyQualifiedName
        };

        var services = new ServiceCollection();
        var builder = new ChatCompletionAgentServiceBuilder(_options, services);

        // Act
        // No additional action needed as services are added in the constructor

        // Assert
        services.Should().ContainSingle(descriptor => descriptor.ServiceType == typeof(IChatHistoryRetriever) &&
                                                      descriptor.ImplementationFactory != null &&
                                                      descriptor.Lifetime == ServiceLifetime.Scoped);
    }

    [Fact]
    public void Should_Add_Custom_ChatHistoryRetriever_By_Type()
    {
        // Arrange
        _options.ChatHistoryRetriever = new ClassOptions
        {
            Type = typeof(CustomChatHistoryRetriever)
        };

        var services = new ServiceCollection();
        var builder = new ChatCompletionAgentServiceBuilder(_options, services);

        // Act
        // No additional action needed as services are added in the constructor

        // Assert
        services.Should().ContainSingle(descriptor => descriptor.ServiceType == typeof(IChatHistoryRetriever) &&
                                                      descriptor.ImplementationFactory != null &&
                                                      descriptor.Lifetime == ServiceLifetime.Scoped);
    }

    [Fact]
    public void Should_Add_Default_ChatHistoryRetriever_If_Options_Are_Null()
    {
        // Arrange
        _options.ChatHistoryRetriever = new ClassOptions();

        var services = new ServiceCollection();
        var builder = new ChatCompletionAgentServiceBuilder(_options, services);

        // Act
        // No additional action needed as services are added in the constructor

        // Assert
        services.Should().ContainSingle(descriptor => descriptor.ServiceType == typeof(IChatHistoryRetriever) &&
                                                      descriptor.ImplementationType == typeof(ChatHistoryRetriever) &&
                                                      descriptor.Lifetime == ServiceLifetime.Scoped);
    }

    [Fact]
    public void Should_Add_Custom_PromptExecutionSettingsRetriever_By_AssemblyQualifiedName()
    {
        // Arrange
        _options.PromptExecutionSettingsRetriever = new ClassOptions
        {
            AssemblyQualifiedName = typeof(CustomPromptExecutionSettingsRetriever).AssemblyQualifiedName
        };

        var services = new ServiceCollection();
        var builder = new ChatCompletionAgentServiceBuilder(_options, services);

        // Act
        // No additional action needed as services are added in the constructor

        // Assert
        services.Should().ContainSingle(descriptor => descriptor.ServiceType == typeof(IPromptExecutionSettingsRetriever) &&
                                                      descriptor.ImplementationFactory != null &&
                                                      descriptor.Lifetime == ServiceLifetime.Scoped);
    }

    [Fact]
    public void Should_Add_Custom_PromptExecutionSettingsRetriever_By_Type()
    {
        // Arrange
        _options.PromptExecutionSettingsRetriever = new ClassOptions
        {
            Type = typeof(CustomPromptExecutionSettingsRetriever)
        };

        var services = new ServiceCollection();
        var builder = new ChatCompletionAgentServiceBuilder(_options, services);

        // Act
        // No additional action needed as services are added in the constructor

        // Assert
        services.Should().ContainSingle(descriptor => descriptor.ServiceType == typeof(IPromptExecutionSettingsRetriever) &&
                                                      descriptor.ImplementationFactory != null &&
                                                      descriptor.Lifetime == ServiceLifetime.Scoped);
    }

    [Fact]
    public void Should_Add_Default_PromptExecutionSettingsRetriever_If_Options_Are_Null()
    {
        // Arrange
        _options.PromptExecutionSettingsRetriever = new ClassOptions();

        var services = new ServiceCollection();
        var builder = new ChatCompletionAgentServiceBuilder(_options, services);

        // Act
        // No additional action needed as services are added in the constructor

        // Assert
        services.Should().ContainSingle(descriptor => descriptor.ServiceType == typeof(IPromptExecutionSettingsRetriever) &&
                                                      descriptor.ImplementationType == typeof(PromptExecutionSettingsRetriever) &&
                                                      descriptor.Lifetime == ServiceLifetime.Scoped);
    }

    [Fact]
    public void Should_Throw_Exception_When_Add_SemanticTextMemoryRetriever_And_Memory_Is_Not_Registered()
    {
        // Arrange
        const string message = "To use the 'SemanticTextMemoryRetriever' with the chat agent, " +
                               "include the memory first using the 'AddMemory()' method.";

        // Act
        var act = () => _builder.WithSemanticTextMemoryRetriever();

        // Assert
        act.Should().Throw<InvalidOperationException>().WithMessage(message);
    }

    [Fact]
    public void Should_Add_SemanticTextMemoryRetriever_When_Memory_Is_Registered()
    {
        // Arrange
        _services.AddSingleton(Mock.Of<IMemoryProvider>());

        // Act
        _builder.WithSemanticTextMemoryRetriever();

        // Assert
        _services.Should().ContainSingle(descriptor => descriptor.ServiceType == typeof(ISemanticTextMemoryRetriever) &&
                                                       descriptor.Lifetime == ServiceLifetime.Scoped);
    }

    [Fact]
    public void Should_Add_SimpleDataStorage_With_Options()
    {
        // Act
        _builder.WithSimpleDataStorage(new SimpleDataStorageOptions());

        // Assert
        _services.Should().ContainSingle(descriptor => descriptor.ServiceType == typeof(IDataStorage) &&
                                                       descriptor.Lifetime == ServiceLifetime.Scoped);
    }

    [Fact]
    public void Should_Add_SimpleDataStorage_With_Null_Options()
    {
        // Arrange
        _options.DataStorage = new DataStorageOptions
        {
            SimpleDataStorage = new SimpleDataStorageOptions()
        };

        // Act
        _builder.WithSimpleDataStorage();

        // Assert
        _services.Should().ContainSingle(descriptor => descriptor.ServiceType == typeof(IDataStorage) &&
                                                       descriptor.Lifetime == ServiceLifetime.Scoped);
    }

    [Fact]
    public void Should_Throw_Exception_When_SimpleDataStorageOptions_Not_Provided()
    {
        // Arrange
        const string message = "No 'SimpleDataStorageOptions' provided.";

        // Act
        var act = () => _builder.WithSimpleDataStorage();

        // Assert
        act.Should().Throw<InvalidOperationException>().WithMessage(message);
    }

    [Fact]
    public void Should_Add_SimpleDataStorage_With_Options_Action()
    {
        // Act
        _builder.WithSimpleDataStorage(options => options.StorageType = StorageType.Volatile);

        // Assert
        _services.Should().ContainSingle(descriptor => descriptor.ServiceType == typeof(IDataStorage) &&
                                                       descriptor.Lifetime == ServiceLifetime.Scoped);
    }

    [Fact]
    public void Should_Throw_Exception_When_Add_SimpleDataStorage_With_Null_Options_Action()
    {
        // Act
        var act = () => _builder.WithSimpleDataStorage((Action<SimpleDataStorageOptions>)null!);

        // Assert
        act.Should().Throw<ArgumentNullException>().WithMessage("*optionsAction*");
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
