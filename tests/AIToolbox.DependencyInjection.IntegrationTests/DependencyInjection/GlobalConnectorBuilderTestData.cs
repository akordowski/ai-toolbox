using AIToolbox.Options.Connectors;

namespace AIToolbox.DependencyInjection;

public class GlobalConnectorBuilderTestData
{
    public static TheoryData<Action<IAIToolboxBuilder, GlobalConnectorOptions>> ConfigureByOptionsAction =>
    [
        (aiToolbox, opt) =>
        {
            aiToolbox.ConfigureGlobalConnectorOptions(
                globalConnectors =>
                {
                    globalConnectors
                        .AddGlobalAzureOpenAIOptions()
                        .AddGlobalGoogleOptions()
                        .AddGlobalHuggingFaceOptions()
                        .AddGlobalMistralAIOptions()
                        .AddGlobalOllamaOptions()
                        .AddGlobalOpenAIOptions()
                        .AddGlobalVertexAIOptions();
                },
                options =>
                {
                    options.AzureOpenAI = opt.AzureOpenAI;
                    options.Google = opt.Google;
                    options.HuggingFace = opt.HuggingFace;
                    options.MistralAI = opt.MistralAI;
                    options.Ollama = opt.Ollama;
                    options.OpenAI = opt.OpenAI;
                    options.VertexAI = opt.VertexAI;
                });
        },
        (aiToolbox, opt) =>
        {
            aiToolbox.ConfigureGlobalConnectorOptions(
                globalConnectors =>
                {
                    globalConnectors
                        .AddGlobalAzureOpenAIOptions(options =>
                        {
                            options.Endpoint = opt.AzureOpenAI!.Endpoint;
                            options.ApiKey = opt.AzureOpenAI.ApiKey;
                            options.ServiceId = opt.AzureOpenAI.ServiceId;
                        })
                        .AddGlobalGoogleOptions(options =>
                        {
                            options.ApiKey = opt.Google!.ApiKey;
                            options.ApiVersion = opt.Google.ApiVersion;
                            options.ServiceId = opt.Google.ServiceId;
                        })
                        .AddGlobalHuggingFaceOptions(options =>
                        {
                            options.Endpoint = opt.HuggingFace!.Endpoint;
                            options.ApiKey = opt.HuggingFace.ApiKey;
                            options.ServiceId = opt.HuggingFace.ServiceId;
                        })
                        .AddGlobalMistralAIOptions(options =>
                        {
                            options.Endpoint = opt.MistralAI!.Endpoint;
                            options.ApiKey = opt.MistralAI.ApiKey;
                            options.ServiceId = opt.MistralAI.ServiceId;
                        })
                        .AddGlobalOllamaOptions(options =>
                        {
                            options.Endpoint = opt.Ollama!.Endpoint;
                            options.ServiceId = opt.Ollama.ServiceId;
                        })
                        .AddGlobalOpenAIOptions(options =>
                        {
                            options.ApiKey = opt.OpenAI!.ApiKey;
                            options.OrgId = opt.OpenAI.OrgId;
                            options.ServiceId = opt.OpenAI.ServiceId;
                        })
                        .AddGlobalVertexAIOptions(options =>
                        {
                            options.BearerKey = opt.VertexAI!.BearerKey;
                            options.Location = opt.VertexAI.Location;
                            options.ProjectId = opt.VertexAI.ProjectId;
                            options.ApiVersion = opt.VertexAI.ApiVersion;
                            options.ServiceId = opt.VertexAI.ServiceId;
                        });
                });
        }
    ];
}
