using AIToolbox.Agents.ChatCompletion;
using AIToolbox.DependencyInjection;
using AIToolbox.Options.SemanticKernel;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

// This example shows how you can set up a simple chat agent using the AIToolbox library.

using var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((_, services) =>
    {
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
                        ChatCompletion = new OllamaChatCompletionOptions { ModelId = "llama3" }
                    }
                };
            })
            .AddAgents()
            .IncludeChatCompletionAgent();
    })
    .Build();

var agent = host.Services.GetRequiredService<IChatAgent>();

Console.WriteLine();
Console.WriteLine("Chat agent");
Console.WriteLine("-----------------------");
Console.WriteLine("Press Ctrl+C to quit the conversation");
Console.WriteLine();

while (true)
{
    Console.Write("User > ");

    var message = Console.ReadLine()!;
    var first = true;

    await foreach (var response in agent.SendMessageAsStreamAsync(message))
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
