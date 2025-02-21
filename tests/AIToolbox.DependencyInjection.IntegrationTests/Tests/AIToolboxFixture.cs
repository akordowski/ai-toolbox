using AIToolbox.Options;
using AIToolbox.Options.Connectors;
using AIToolbox.Options.Enums;
using AIToolbox.Options.SemanticKernel;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Xunit.DependencyInjection;

namespace AIToolbox.Tests;

public class AIToolboxFixture : BaseDisposable
{
    public IHost GetHost(
        Action<HostBuilderContext, IServiceCollection> configureServices,
        bool useAppConfiguration = false) =>
        Host.CreateDefaultBuilder()
            .ConfigureAppConfiguration(config =>
            {
                if (useAppConfiguration)
                {
                    config.AddJsonStream(GetJsonMemoryStream());
                }
            })
            .ConfigureServices(configureServices)
            .Build();

    private MemoryStream GetJsonMemoryStream()
    {
        var config = new ConfigAIToolbox { AIToolbox = Options };
        var options = new JsonSerializerOptions { Converters = { new JsonStringEnumConverter() } };
        var json = JsonSerializer.Serialize(config, options);

        return new MemoryStream(Encoding.UTF8.GetBytes(json));
    }

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
        },
        SemanticKernel = new SemanticKernelOptions
        {
            Kernel = new KernelOptions
            {
                Connectors = new ConnectorOptions
                {
                    AzureOpenAI = new AzureOpenAIOptions
                    {
                        AudioToText = new AzureOpenAIAudioToTextOptions
                        {
                            Endpoint = "AzureOpenAIAudioToTextOptionsEndpoint",
                            ApiKey = "AzureOpenAIAudioToTextOptionsApiKey",
                            DeploymentName = "AzureOpenAIAudioToTextOptionsDeploymentName",
                            ServiceId = "AzureOpenAIAudioToTextOptionsServiceId",
                            ModelId = "AzureOpenAIAudioToTextOptionsModelId"
                        },
                        ChatCompletion = new AzureOpenAIChatCompletionOptions
                        {
                            Endpoint = "AzureOpenAIChatCompletionOptionsEndpoint",
                            ApiKey = "AzureOpenAIChatCompletionOptionsApiKey",
                            DeploymentName = "AzureOpenAIChatCompletionOptionsDeploymentName",
                            ServiceId = "AzureOpenAIChatCompletionOptionsServiceId",
                            ModelId = "AzureOpenAIChatCompletionOptionsModelId"
                        },
                        Files = new AzureOpenAIFilesOptions
                        {
                            Endpoint = "AzureOpenAIFilesOptionsEndpoint",
                            ApiKey = "AzureOpenAIFilesOptionsApiKey",
                            OrgId = "AzureOpenAIFilesOptionsOrgId",
                            Version = "AzureOpenAIFilesOptionsVersion",
                            ServiceId = "AzureOpenAIFilesOptionsServiceId"
                        },
                        TextEmbeddingGeneration = new AzureOpenAITextEmbeddingGenerationOptions
                        {
                            Endpoint = "AzureOpenAITextEmbeddingGenerationOptionsEndpoint",
                            ApiKey = "AzureOpenAITextEmbeddingGenerationOptionsApiKey",
                            DeploymentName = "AzureOpenAITextEmbeddingGenerationOptionsDeploymentName",
                            ServiceId = "AzureOpenAITextEmbeddingGenerationOptionsServiceId",
                            ModelId = "AzureOpenAITextEmbeddingGenerationOptionsModelId",
                            Dimensions = 1024
                        },
                        TextGeneration = new AzureOpenAITextGenerationOptions
                        {
                            Endpoint = "AzureOpenAITextGenerationOptionsEndpoint",
                            ApiKey = "AzureOpenAITextGenerationOptionsApiKey",
                            DeploymentName = "AzureOpenAITextGenerationOptionsDeploymentName",
                            ServiceId = "AzureOpenAITextGenerationOptionsServiceId",
                            ModelId = "AzureOpenAITextGenerationOptionsModelId"
                        },
                        TextToAudio = new AzureOpenAITextToAudioOptions
                        {
                            Endpoint = "AzureOpenAITextToAudioOptionsEndpoint",
                            ApiKey = "AzureOpenAITextToAudioOptionsApiKey",
                            DeploymentName = "AzureOpenAITextToAudioOptionsDeploymentName",
                            ServiceId = "AzureOpenAITextToAudioOptionsServiceId",
                            ModelId = "AzureOpenAITextToAudioOptionsModelId"
                        },
                        TextToImage = new AzureOpenAITextToImageOptions
                        {
                            Endpoint = "AzureOpenAITextToImageOptionsEndpoint",
                            ApiKey = "AzureOpenAITextToImageOptionsApiKey",
                            DeploymentName = "AzureOpenAITextToImageOptionsDeploymentName",
                            ServiceId = "AzureOpenAITextToImageOptionsServiceId",
                            ModelId = "AzureOpenAITextToImageOptionsModelId"
                        }
                    },
                    Google = new GoogleOptions
                    {
                        ChatCompletion = new GoogleAIChatCompletionOptions
                        {
                            ModelId = "GoogleAIChatCompletionOptionsModelId",
                            ApiKey = "GoogleAIChatCompletionOptionsApiKey",
                            ApiVersion = GoogleAIVersion.V1,
                            ServiceId = "GoogleAIChatCompletionOptionsServiceId"
                        },
                        EmbeddingGeneration = new GoogleAIEmbeddingGenerationOptions
                        {
                            ModelId = "GoogleAIEmbeddingGenerationOptionsModelId",
                            ApiKey = "GoogleAIEmbeddingGenerationOptionsApiKey",
                            ApiVersion = GoogleAIVersion.V1,
                            ServiceId = "GoogleAIEmbeddingGenerationOptionsServiceId"
                        }
                    },
                    HuggingFace = new HuggingFaceOptions
                    {
                        ChatCompletion = new HuggingFaceChatCompletionOptions
                        {
                            Model = "HuggingFaceChatCompletionOptionsModel",
                            Endpoint = "HuggingFaceChatCompletionOptionsEndpoint",
                            ApiKey = "HuggingFaceChatCompletionOptionsApiKey",
                            ServiceId = "HuggingFaceChatCompletionOptionsServiceId"
                        },
                        ImageToText = new HuggingFaceImageToTextOptions
                        {
                            Model = "HuggingFaceImageToTextOptionsModel",
                            Endpoint = "HuggingFaceImageToTextOptionsEndpoint",
                            ApiKey = "HuggingFaceImageToTextOptionsApiKey",
                            ServiceId = "HuggingFaceImageToTextOptionsServiceId"
                        },
                        TextEmbeddingGeneration = new HuggingFaceTextEmbeddingGenerationOptions
                        {
                            Model = "HuggingFaceTextEmbeddingGenerationOptionsModel",
                            Endpoint = "HuggingFaceTextEmbeddingGenerationOptionsEndpoint",
                            ApiKey = "HuggingFaceTextEmbeddingGenerationOptionsApiKey",
                            ServiceId = "HuggingFaceTextEmbeddingGenerationOptionsServiceId"
                        },
                        TextGeneration = new HuggingFaceTextGenerationOptions
                        {
                            Model = "HuggingFaceTextGenerationOptionsModel",
                            Endpoint = "HuggingFaceTextGenerationOptionsEndpoint",
                            ApiKey = "HuggingFaceTextGenerationOptionsApiKey",
                            ServiceId = "HuggingFaceTextGenerationOptionsServiceId"
                        }
                    },
                    MistralAI = new MistralAIOptions
                    {
                        ChatCompletion = new MistralAIChatCompletionOptions
                        {
                            Model = "MistralAIChatCompletionOptionsModel",
                            Endpoint = "MistralAIChatCompletionOptionsEndpoint",
                            ApiKey = "MistralAIChatCompletionOptionsApiKey",
                            ServiceId = "MistralAIChatCompletionOptionsServiceId"
                        },
                        TextEmbeddingGeneration = new MistralAITextEmbeddingGenerationOptions
                        {
                            Model = "MistralAITextEmbeddingGenerationOptionsModel",
                            Endpoint = "MistralAITextEmbeddingGenerationOptionsEndpoint",
                            ApiKey = "MistralAITextEmbeddingGenerationOptionsApiKey",
                            ServiceId = "MistralAITextEmbeddingGenerationOptionsServiceId"
                        }
                    },
                    Ollama = new OllamaOptions
                    {
                        ChatCompletion = new OllamaChatCompletionOptions
                        {
                            ModelId = "OllamaChatCompletionOptionsModelId",
                            Endpoint = "OllamaChatCompletionOptionsEndpoint",
                            ServiceId = "OllamaChatCompletionOptionsServiceId"
                        },
                        TextEmbeddingGeneration = new OllamaTextEmbeddingGenerationOptions
                        {
                            ModelId = "OllamaTextEmbeddingGenerationOptionsModelId",
                            Endpoint = "OllamaTextEmbeddingGenerationOptionsEndpoint",
                            ServiceId = "OllamaTextEmbeddingGenerationOptionsServiceId"
                        },
                        TextGeneration = new OllamaTextGenerationOptions
                        {
                            ModelId = "OllamaTextGenerationOptionsModelId",
                            Endpoint = "OllamaTextGenerationOptionsEndpoint",
                            ServiceId = "OllamaTextGenerationOptionsServiceId"
                        }
                    },
                    OpenAI = new OpenAIOptions
                    {
                        AudioToText = new OpenAIAudioToTextOptions
                        {
                            ModelId = "OpenAIAudioToTextOptionsModelId",
                            ApiKey = "OpenAIAudioToTextOptionsApiKey",
                            OrgId = "OpenAIAudioToTextOptionsOrgId",
                            ServiceId = "OpenAIAudioToTextOptionsServiceId"
                        },
                        ChatCompletion = new OpenAIChatCompletionOptions
                        {
                            ModelId = "OpenAIChatCompletionOptionsModelId",
                            ApiKey = "OpenAIChatCompletionOptionsApiKey",
                            OrgId = "OpenAIChatCompletionOptionsOrgId",
                            ServiceId = "OpenAIChatCompletionOptionsServiceId"
                        },
                        Files = new OpenAIFilesOptions
                        {
                            ApiKey = "OpenAIFilesOptionsApiKey",
                            OrgId = "OpenAIFilesOptionsOrgId",
                            ServiceId = "OpenAIFilesOptionsServiceId"
                        },
                        TextEmbeddingGeneration = new OpenAITextEmbeddingGenerationOptions
                        {
                            ModelId = "OpenAITextEmbeddingGenerationOptionsModelId",
                            ApiKey = "OpenAITextEmbeddingGenerationOptionsApiKey",
                            OrgId = "OpenAITextEmbeddingGenerationOptionsOrgId",
                            ServiceId = "OpenAITextEmbeddingGenerationOptionsServiceId",
                            Dimensions = 1024
                        },
                        TextToAudio = new OpenAITextToAudioOptions
                        {
                            ModelId = "OpenAITextToAudioOptionsModelId",
                            ApiKey = "OpenAITextToAudioOptionsApiKey",
                            OrgId = "OpenAITextToAudioOptionsOrgId",
                            ServiceId = "OpenAITextToAudioOptionsServiceId"
                        },
                        TextToImage = new OpenAITextToImageOptions
                        {
                            ApiKey = "OpenAITextToImageOptionsApiKey",
                            OrgId = "OpenAITextToImageOptionsOrgId",
                            ServiceId = "OpenAITextToImageOptionsServiceId"
                        }
                    },
                    VertexAI = new VertexAIOptions
                    {
                        ChatCompletion = new VertexAIChatCompletionOptions
                        {
                            ModelId = "VertexAIChatCompletionOptionsModelId",
                            BearerKey = "VertexAIChatCompletionOptionsBearerKey",
                            Location = "VertexAIChatCompletionOptionsLocation",
                            ProjectId = "VertexAIChatCompletionOptionsProjectId",
                            ApiVersion = VertexAIVersion.V1,
                            ServiceId = "VertexAIChatCompletionOptionsServiceId"
                        },
                        EmbeddingGeneration = new VertexAIEmbeddingGenerationOptions
                        {
                            ModelId = "VertexAIEmbeddingGenerationOptionsModelId",
                            BearerKey = "VertexAIEmbeddingGenerationOptionsBearerKey",
                            Location = "VertexAIEmbeddingGenerationOptionsLocation",
                            ProjectId = "VertexAIEmbeddingGenerationOptionsProjectId",
                            ApiVersion = VertexAIVersion.V1,
                            ServiceId = "VertexAIEmbeddingGenerationOptionsServiceId"
                        }
                    }
                }
            }
        }
    };

    private class ConfigAIToolbox
    {
        public AIToolboxOptions AIToolbox { get; set; } = default!;
    }
}
