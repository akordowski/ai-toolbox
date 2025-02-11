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

internal sealed class ChatCompletionBuilder : IChatCompletionBuilder
{
    private readonly ChatCompletionOptions _options;
    private readonly IServiceCollection _services;

    public ChatCompletionBuilder(
        ChatCompletionOptions options,
        IServiceCollection services)
    {
        Verify.ThrowIfNull(options, nameof(options), $"No '{nameof(ChatCompletionOptions)}' provided.");
        Verify.ThrowIfNull(services, nameof(services));

        _options = options;
        _services = services;

        _services
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

        AddService<IChatHistoryRetriever, ChatHistoryRetriever>(options.ChatHistoryRetriever);
        AddService<IPromptExecutionSettingsRetriever, PromptExecutionSettingsRetriever>(options.PromptExecutionSettingsRetriever);
    }

    public IChatCompletionBuilder WithSemanticTextMemoryRetriever()
    {
        var memoryProviderType = typeof(IMemoryProvider);
        var isRegistered = _services.Any(descriptor => descriptor.ServiceType == memoryProviderType);

        if (!isRegistered)
        {
            const string message = $"To use the '{nameof(SemanticTextMemoryRetriever)}' with the chat completion, " +
                                   $"include the memory first using the '{nameof(ISemanticKernelBuilder.AddMemory)}()' method.";

            throw new InvalidOperationException(message);
        }

        _services.AddScoped<ISemanticTextMemoryRetriever, SemanticTextMemoryRetriever>();

        return this;
    }

    public IChatCompletionBuilder WithSimpleDataStorage(SimpleDataStorageOptions? options = null)
    {
        if (options is not null)
        {
            _options.DataStorage ??= new DataStorageOptions();
            _options.DataStorage.SimpleDataStorage = options;
        }

        _services
            .AddSingleton(_options.DataStorage?.SimpleDataStorage!)
            .AddScoped<IDataStorage, SimpleDataStorage>();

        return this;
    }

    public IChatCompletionBuilder WithSimpleDataStorage(Action<SimpleDataStorageOptions> optionsAction)
    {
        Verify.ThrowIfNull(optionsAction, nameof(optionsAction));

        _options.DataStorage ??= new DataStorageOptions();
        _options.DataStorage.SimpleDataStorage ??= new SimpleDataStorageOptions();

        optionsAction(_options.DataStorage.SimpleDataStorage);

        _services
            .AddSingleton(_options.DataStorage.SimpleDataStorage)
            .AddScoped<IDataStorage, SimpleDataStorage>();

        return this;
    }

    private void AddSingleton<TService>(TService? implementationInstance) where TService : class
    {
        if (implementationInstance is not null)
        {
            _services.AddSingleton(implementationInstance);
        }
    }

    private void AddService<TService, TImplementation>(ClassOptions? options = null)
        where TService : class
        where TImplementation : class, TService
    {
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

                _services.AddScoped(_ => Activator.CreateInstance(type) as TService ??
                                         throw new InvalidOperationException($"Could not create an instance of type '{type.FullName}'."));

                return;
            }
        }

        _services.AddScoped<TService, TImplementation>();
    }
}
