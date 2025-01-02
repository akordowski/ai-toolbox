using AIToolbox.Agents.ChatCompletion;
using AIToolbox.Agents.ChatCompletion.Models;
using AIToolbox.Agents.ChatCompletion.Services;
using AIToolbox.DependencyInjection;
using AIToolbox.Options;
using AIToolbox.Options.SemanticKernel;
using AIToolbox.SemanticKernel.Memory;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

// This example shows how you can set up a persistent chat agent using the AIToolbox library.

using var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((_, services) =>
    {
        const string modelId = "llama3";

        services
            .AddAIToolbox()
            .AddConnectors()
            .IncludeOllamaConnector(options => options.Endpoint = "http://localhost:11434")
            .AddKernel(options =>
            {
                options.Connectors = new KernelConnectorOptions
                {
                    Ollama = new OllamaOptions
                    {
                        ChatCompletion = new OllamaChatCompletionOptions { ModelId = modelId }
                    }
                };
            })
            .AddMemory(options =>
            {
                options.TextEmbeddingGeneration = new TextEmbeddingGenerationOptions
                {
                    Ollama = new OllamaTextEmbeddingGenerationMemoryOptions
                    {
                        ModelId = modelId
                    }
                };
            })
            .IncludeSimpleMemoryStore(options => options.StorageType = StorageType.Persistent)
            .AddAgents()
            .IncludeChatCompletionAgent(options =>
            {
                // IMPORTANT!
                // When asking questions regarding the memories used in this example, the memory
                // provider of your choice may return different amount of results with different
                // relevance scores. So you have to play with the Limit / MinRelevanceScore values.

                options.MemorySearch = new MemorySearchOptions
                {
                    Limit = 2,
                    MinRelevanceScore = 0.1
                };
            })
            .WithSemanticTextMemoryRetriever()
            .WithSimpleDataStorage(options => options.StorageType = StorageType.Persistent);
    })
    .Build();

var agent = await GetChatAgentAsync(host.Services);
var userId = agent.Participants[0].UserId;

await AddMemoriesAsync(host.Services, agent.Chat!.Id);

Console.WriteLine();
Console.WriteLine("Chat agent");
Console.WriteLine("-----------------------");
Console.WriteLine("Press Ctrl+C to quit the conversation");
Console.WriteLine();

// Show previous messages
foreach (var message in agent.Messages)
{
    var role = message.Role == AuthorRole.User ? "User" : "Assistant";

    Console.WriteLine($"{role} > {message.Content}");

    if (message.Role == AuthorRole.Assistant)
    {
        Console.WriteLine();
    }
}

while (true)
{
    Console.Write("User > ");

    var message = Console.ReadLine()!;
    var first = true;

    await foreach (var response in agent.SendMessageAsStreamAsync(userId, message))
    {
        if (first)
        {
            Console.Write("Assistant > ");
            first = false;
        }

        Console.Write(response.Content);
    }

    Console.WriteLine();
    Console.WriteLine();
}

async Task AddMemoriesAsync(IServiceProvider provider, string collection)
{
    var memoryProvider = provider.GetRequiredService<IMemoryProvider>();
    var memory = memoryProvider.GetMemory();
    var collections = await memory.GetCollectionsAsync();

    if (collections.Contains(collection))
    {
        return;
    }

    await memory.SaveInformationAsync(collection, id: "info1", text: "My name is Andrea");
    await memory.SaveInformationAsync(collection, id: "info2", text: "I currently work as a tourist operator");
    await memory.SaveInformationAsync(collection, id: "info3", text: "I currently live in Seattle and have been living there since 2005");
    await memory.SaveInformationAsync(collection, id: "info4", text: "I visited France and Italy five times since 2015");
    await memory.SaveInformationAsync(collection, id: "info5", text: "My family is from New York");
}

async Task<IPersistentChatAgent> GetChatAgentAsync(IServiceProvider provider)
{
    // The PersistentChatAgentService takes care of handling the chat agents data persistence
    var chatAgentService = provider.GetRequiredService<IPersistentChatAgentService>();

    // Get an instance of a persistent chat agent
    var chatAgent = provider.GetRequiredService<IPersistentChatAgent>();

    // If no previous chat exists create one
    if (!await chatAgentService.Chats.AnyAsync())
    {
        const string chatTitle = "Persistent Chat Agent Demo";
        const string userId = "1";
        const string userName = "Andrea";

        await chatAgent.CreateChatAsync(chatTitle, userId, userName);
    }

    // If a previous chat exists load it
    else
    {
        var chat = (await chatAgentService.Chats.GetAllAsync()).First();
        await chatAgent.LoadChatAsync(chat.Id);
    }

    return chatAgent;
}
