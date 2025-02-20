using AIToolbox.Options;
using AIToolbox.Options.Connectors;
using AIToolbox.Options.Enums;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Xunit.DependencyInjection;

namespace AIToolbox.Tests;

public class AIToolboxFixture : BaseDisposable
{
    public AIToolboxOptions Options { get; } = new()
    {
        GlobalConnectors = new GlobalConnectorOptions
        {
            AzureOpenAI = new GlobalAzureOpenAIOptions
            {
                Endpoint = "GlobalAzureOpenAIOptionsEndpoint",
                ApiKey = "GlobalAzureOpenAIOptionsApiKey",
                ServiceId = "GlobalAzureOpenAIOptionsServiceId"
            },
            Google = new GlobalGoogleOptions
            {
                ApiKey = "GlobalGoogleOptionsApiKey",
                ApiVersion = GoogleAIVersion.V1,
                ServiceId = "GlobalGoogleOptionsServiceId"
            },
            HuggingFace = new GlobalHuggingFaceOptions
            {
                Endpoint = "GlobalHuggingFaceOptionsEndpoint",
                ApiKey = "GlobalHuggingFaceOptionsApiKey",
                ServiceId = "GlobalHuggingFaceOptionsServiceId"
            },
            MistralAI = new GlobalMistralAIOptions
            {
                Endpoint = "GlobalMistralAIOptionsEndpoint",
                ApiKey = "GlobalMistralAIOptionsApiKey",
                ServiceId = "GlobalMistralAIOptionsServiceId"
            },
            Ollama = new GlobalOllamaOptions
            {
                Endpoint = "GlobalOllamaOptionsEndpoint",
                ServiceId = "GlobalOllamaOptionsServiceId"
            },
            OpenAI = new GlobalOpenAIOptions
            {
                ApiKey = "GlobalOpenAIOptionsApiKey",
                OrgId = "GlobalOpenAIOptionsOrgId",
                ServiceId = "GlobalOpenAIOptionsServiceId"
            },
            VertexAI = new GlobalVertexAIOptions
            {
                BearerKey = "GlobalVertexAIOptionsBearerKey",
                Location = "GlobalVertexAIOptionsLocation",
                ProjectId = "GlobalVertexAIOptionsProjectId",
                ApiVersion = VertexAIVersion.V1,
                ServiceId = "GlobalVertexAIOptionsServiceId"
            }
        }
    };

    public IHost GetHost(
        Action<HostBuilderContext, IServiceCollection> configureServices,
        string? configFileName = null) =>
        Host.CreateDefaultBuilder()
            .ConfigureAppConfiguration(config =>
            {
                if (!string.IsNullOrWhiteSpace(configFileName))
                {
                    config.AddJsonFile("Config/" + configFileName, false);
                }
            })
            .ConfigureServices(configureServices)
            .Build();
}
