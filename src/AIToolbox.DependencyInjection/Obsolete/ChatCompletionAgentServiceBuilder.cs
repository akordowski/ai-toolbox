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
using Microsoft.Extensions.DependencyInjection;

namespace AIToolbox.DependencyInjection;

internal sealed class ChatCompletionAgentServiceBuilder : IChatCompletionAgentServiceBuilder
{
    public ChatCompletionOptions Options { get; }
    public IServiceCollection Services { get; }

    public ChatCompletionAgentServiceBuilder(
        ChatCompletionOptions options,
        IServiceCollection services)
    {
        Verify.ThrowIfNull(options, nameof(options), $"No '{nameof(ChatCompletionOptions)}' provided.");
        Verify.ThrowIfNull(services, nameof(services));

        Options = options;
        Services = services;

        Services
            .AddScoped<IChatResource, ChatResource>()
            .AddScoped<IMessageResource, MessageResource>()
            .AddScoped<IParticipantResource, ParticipantResource>()
            .AddScoped<ISettingsResource, SettingsResource>()
            .AddScoped<IPersistentChatAgentService, PersistentChatAgentService>()
            .AddScoped<IChatAgent, ChatAgent>()
            .AddScoped<IPersistentChatAgent, PersistentChatAgent>();

        AddSingleton(options.ChatHistory);
        AddSingleton(options.MemorySearch);
        AddSingleton(options.PromptExecution);

        AddService<IChatHistoryRetriever, ChatHistoryRetriever>(Options.ChatHistoryRetriever);
        AddService<IPromptExecutionSettingsRetriever, PromptExecutionSettingsRetriever>(Options.PromptExecutionSettingsRetriever);
    }

    public IChatCompletionAgentServiceBuilder WithSemanticTextMemoryRetriever()
    {
        var memoryProviderType = typeof(IMemoryProvider);
        var isRegistered = Services.Any(descriptor => descriptor.ServiceType == memoryProviderType);

        if (!isRegistered)
        {
            const string message = $"To use the '{nameof(SemanticTextMemoryRetriever)}' with the chat agent, " +
                                   $"include the memory first using the '{nameof(IKernelServiceBuilder.AddMemory)}()' method.";

            throw new InvalidOperationException(message);
        }

        Services.AddScoped<ISemanticTextMemoryRetriever, SemanticTextMemoryRetriever>();

        return this;
    }

    public IChatCompletionAgentServiceBuilder WithSimpleDataStorage(SimpleDataStorageOptions? options = null)
    {
        if (options is not null)
        {
            Options.DataStorage ??= new DataStorageOptions();
            Options.DataStorage.SimpleDataStorage = options;
        }

        Verify.ThrowIfOptionsNull(Options.DataStorage?.SimpleDataStorage);

        Services
            .AddSingleton(Options.DataStorage!.SimpleDataStorage!)
            .AddScoped<IDataStorage, SimpleDataStorage>();

        return this;
    }

    public IChatCompletionAgentServiceBuilder WithSimpleDataStorage(Action<SimpleDataStorageOptions> optionsAction)
    {
        Verify.ThrowIfNull(optionsAction, nameof(optionsAction));

        Options.DataStorage ??= new DataStorageOptions();
        Options.DataStorage.SimpleDataStorage ??= new SimpleDataStorageOptions();

        optionsAction(Options.DataStorage.SimpleDataStorage);

        return WithSimpleDataStorage(Options.DataStorage.SimpleDataStorage);
    }

    private void AddSingleton<TService>(TService? implementationInstance) where TService : class
    {
        if (implementationInstance is not null)
        {
            Services.AddSingleton(implementationInstance);
        }
    }

    private void AddService<TService, TImplementation>(ClassOptions? options = null)
        where TService : class
        where TImplementation : class, TService
    {
        var addImplementation = true;

        if (options is not null)
        {
            var serviceType = typeof(TService);
            Type? type = null;

            if (!string.IsNullOrWhiteSpace(options.AssemblyQualifiedName))
            {
                var assemblyQualifiedName = options.AssemblyQualifiedName;

                type = Type.GetType(assemblyQualifiedName) ??
                       throw new InvalidOperationException($"Could not find the type '{assemblyQualifiedName}'.");
            }
            else if (options.Type is not null)
            {
                type = options.Type;
            }

            if (type is not null)
            {
                if (!type.IsAssignableTo(serviceType))
                {
                    throw new InvalidOperationException($"The '{type.FullName}' must implement the '{serviceType.FullName}' interface.");
                }

                Services.AddScoped(_ => Activator.CreateInstance(type) as TService ??
                                        throw new InvalidOperationException($"Could not create an instance of type '{type.FullName}'."));

                addImplementation = false;
            }
        }

        if (addImplementation)
        {
            Services.AddScoped<TService, TImplementation>();
        }
    }
}
