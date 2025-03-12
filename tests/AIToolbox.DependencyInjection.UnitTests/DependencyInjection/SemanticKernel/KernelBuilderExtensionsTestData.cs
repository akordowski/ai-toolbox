using AIToolbox.Options.SemanticKernel;
using AIToolbox.SemanticKernel;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.SemanticKernel.Connectors.AzureOpenAI;
using Microsoft.SemanticKernel.Connectors.Google;
using Microsoft.SemanticKernel.Connectors.HuggingFace;
using Microsoft.SemanticKernel.Connectors.MistralAI;
using Microsoft.SemanticKernel.Connectors.Ollama;
using Microsoft.SemanticKernel.Connectors.OpenAI;

namespace AIToolbox.DependencyInjection.SemanticKernel;

public class KernelBuilderExtensionsTestData
{
    public static TheoryData<Action> AddConnectorWithNullBuilder
    {
        get
        {
            IKernelBuilder builder = null!;

            return
            [
                () => builder.WithAzureOpenAIConnector(),
                () => builder.WithAzureOpenAIConnector(_ => { }),

                () => builder.WithGoogleConnector(),
                () => builder.WithGoogleConnector(_ => { }),

                () => builder.WithHuggingFaceConnector(),
                () => builder.WithHuggingFaceConnector(_ => { }),

                () => builder.WithMistralAIConnector(),
                () => builder.WithMistralAIConnector(_ => { }),

                () => builder.WithOllamaConnector(),
                () => builder.WithOllamaConnector(_ => { }),

                () => builder.WithOpenAIConnector(),
                () => builder.WithOpenAIConnector(_ => { }),

                () => builder.WithVertexAIConnector(),
                () => builder.WithVertexAIConnector(_ => { })
            ];
        }
    }

    public static TheoryData<Action, string> AddConnectorWithNullParameters
    {
        get
        {
            var builder = new KernelBuilder(new KernelOptions(), new ServiceCollection());

            return new TheoryData<Action, string>
            {
                { () => builder.WithAzureOpenAIConnector(null!), "optionsAction" },
                { () => builder.WithGoogleConnector(null!), "optionsAction" },
                { () => builder.WithHuggingFaceConnector(null!), "optionsAction" },
                { () => builder.WithMistralAIConnector(null!), "optionsAction" },
                { () => builder.WithOllamaConnector(null!), "optionsAction" },
                { () => builder.WithOpenAIConnector(null!), "optionsAction" },
                { () => builder.WithVertexAIConnector(null!), "optionsAction" }
            };
        }
    }

    public static TheoryData<Action, string> AddConnectorWithNoDefaultOptions
    {
        get
        {
            var builder = new KernelBuilder(new KernelOptions(), new ServiceCollection());

            return new TheoryData<Action, string>
            {
                { () => builder.WithAzureOpenAIConnector(), "No 'AzureOpenAIOptions' provided.*" },
                { () => builder.WithGoogleConnector(), "No 'GoogleOptions' provided.*" },
                { () => builder.WithHuggingFaceConnector(), "No 'HuggingFaceOptions' provided.*" },
                { () => builder.WithMistralAIConnector(), "No 'MistralAIOptions' provided.*" },
                { () => builder.WithOllamaConnector(), "No 'OllamaOptions' provided.*" },
                { () => builder.WithOpenAIConnector(), "No 'OpenAIOptions' provided.*" },
                { () => builder.WithVertexAIConnector(), "No 'VertexAIOptions' provided.*" }
            };
        }
    }

    public static TheoryData<Func<IKernelBuilder>, IKernelBuilder, Action> AddConnectorWithDefaultOptions
    {
        get
        {
            var options = new KernelOptions
            {
                Connectors = new ConnectorOptions
                {
                    AzureOpenAI = new AzureOpenAIOptions(),
                    Google = new GoogleOptions(),
                    HuggingFace = new HuggingFaceOptions(),
                    MistralAI = new MistralAIOptions(),
                    Ollama = new OllamaOptions(),
                    OpenAI = new OpenAIOptions(),
                    VertexAI = new VertexAIOptions()
                }
            };
            var services = new ServiceCollection();
            var builder = new KernelBuilder(options, services);

            return new TheoryData<Func<IKernelBuilder>, IKernelBuilder, Action>
            {
                {
                    builder.WithAzureOpenAIConnector,
                    builder,
                    () =>
                    {
                        // Assert
                        options.Connectors!.AzureOpenAI.Should().NotBeNull();

                        services.Should().ContainSingle(descriptor => descriptor.Lifetime == ServiceLifetime.Singleton &&
                                                                      descriptor.ServiceType == typeof(AzureOpenAIOptions) &&
                                                                      descriptor.ImplementationInstance == options.Connectors!.AzureOpenAI);

                        AssertAzureOpenAIServices(services);
                    }
                },
                {
                    builder.WithGoogleConnector,
                    builder,
                    () =>
                    {
                        // Assert
                        options.Connectors!.Google.Should().NotBeNull();

                        services.Should().ContainSingle(descriptor => descriptor.Lifetime == ServiceLifetime.Singleton &&
                                                                      descriptor.ServiceType == typeof(GoogleOptions) &&
                                                                      descriptor.ImplementationInstance == options.Connectors!.Google);

                        AssertGoogleServices(services);
                    }
                },
                {
                    builder.WithHuggingFaceConnector,
                    builder,
                    () =>
                    {
                        // Assert
                        options.Connectors!.HuggingFace.Should().NotBeNull();

                        services.Should().ContainSingle(descriptor => descriptor.Lifetime == ServiceLifetime.Singleton &&
                                                                      descriptor.ServiceType == typeof(HuggingFaceOptions) &&
                                                                      descriptor.ImplementationInstance == options.Connectors!.HuggingFace);

                        AssertHuggingFaceServices(services);
                    }
                },
                {
                    builder.WithMistralAIConnector,
                    builder,
                    () =>
                    {
                        // Assert
                        options.Connectors!.MistralAI.Should().NotBeNull();

                        services.Should().ContainSingle(descriptor => descriptor.Lifetime == ServiceLifetime.Singleton &&
                                                                      descriptor.ServiceType == typeof(MistralAIOptions) &&
                                                                      descriptor.ImplementationInstance == options.Connectors!.MistralAI);

                        AssertMistralAIServices(services);
                    }
                },
                {
                    builder.WithOllamaConnector,
                    builder,
                    () =>
                    {
                        // Assert
                        options.Connectors!.Ollama.Should().NotBeNull();

                        services.Should().ContainSingle(descriptor => descriptor.Lifetime == ServiceLifetime.Singleton &&
                                                                      descriptor.ServiceType == typeof(OllamaOptions) &&
                                                                      descriptor.ImplementationInstance == options.Connectors!.Ollama);

                        AssertOllamaServices(services);
                    }
                },
                {
                    builder.WithOpenAIConnector,
                    builder,
                    () =>
                    {
                        // Assert
                        options.Connectors!.OpenAI.Should().NotBeNull();

                        services.Should().ContainSingle(descriptor => descriptor.Lifetime == ServiceLifetime.Singleton &&
                                                                      descriptor.ServiceType == typeof(OpenAIOptions) &&
                                                                      descriptor.ImplementationInstance == options.Connectors!.OpenAI);

                        AssertOpenAIServices(services);
                    }
                },
                {
                    builder.WithVertexAIConnector,
                    builder,
                    () =>
                    {
                        // Assert
                        options.Connectors!.VertexAI.Should().NotBeNull();

                        services.Should().ContainSingle(descriptor => descriptor.Lifetime == ServiceLifetime.Singleton &&
                                                                      descriptor.ServiceType == typeof(VertexAIOptions) &&
                                                                      descriptor.ImplementationInstance == options.Connectors!.VertexAI);

                        AssertVertexAIServices(services);
                    }
                }
            };
        }
    }

    public static TheoryData<Func<object, IKernelBuilder>, IKernelBuilder, object, Action> AddConnectorWithCustomOptions
    {
        get
        {
            var azureOpenAIOptions = new AzureOpenAIOptions();
            var googleOptions = new GoogleOptions();
            var huggingFaceOptions = new HuggingFaceOptions();
            var mistralAIOptions = new MistralAIOptions();
            var ollamaOptions = new OllamaOptions();
            var openAIOptions = new OpenAIOptions();
            var vertexAIOptions = new VertexAIOptions();

            var options = new KernelOptions { Connectors = new ConnectorOptions() };
            var services = new ServiceCollection();
            var builder = new KernelBuilder(options, services);

            return new TheoryData<Func<object, IKernelBuilder>, IKernelBuilder, object, Action>
            {
                {
                    opt =>
                    {
                        // Act
                        options.Connectors.AzureOpenAI = opt as AzureOpenAIOptions;
                        return builder.WithAzureOpenAIConnector();
                    },
                    builder,
                    azureOpenAIOptions,
                    () =>
                    {
                        // Assert
                        options.Connectors!.AzureOpenAI.Should().Be(azureOpenAIOptions);

                        services.Should().ContainSingle(descriptor => descriptor.Lifetime == ServiceLifetime.Singleton &&
                                                                      descriptor.ServiceType == typeof(AzureOpenAIOptions) &&
                                                                      descriptor.ImplementationInstance == azureOpenAIOptions);

                        AssertAzureOpenAIServices(services);
                    }
                },
                {
                    opt =>
                    {
                        // Act
                        options.Connectors.Google = opt as GoogleOptions;
                        return builder.WithGoogleConnector();
                    },
                    builder,
                    googleOptions,
                    () =>
                    {
                        // Assert
                        options.Connectors!.Google.Should().Be(googleOptions);

                        services.Should().ContainSingle(descriptor => descriptor.Lifetime == ServiceLifetime.Singleton &&
                                                                      descriptor.ServiceType == typeof(GoogleOptions) &&
                                                                      descriptor.ImplementationInstance == googleOptions);

                        AssertGoogleServices(services);
                    }
                },
                {
                    opt =>
                    {
                        // Act
                        options.Connectors.HuggingFace = opt as HuggingFaceOptions;
                        return builder.WithHuggingFaceConnector();
                    },
                    builder,
                    huggingFaceOptions,
                    () =>
                    {
                        // Assert
                        options.Connectors!.HuggingFace.Should().Be(huggingFaceOptions);

                        services.Should().ContainSingle(descriptor => descriptor.Lifetime == ServiceLifetime.Singleton &&
                                                                      descriptor.ServiceType == typeof(HuggingFaceOptions) &&
                                                                      descriptor.ImplementationInstance == huggingFaceOptions);

                        AssertHuggingFaceServices(services);
                    }
                },
                {
                    opt =>
                    {
                        // Act
                        options.Connectors.MistralAI = opt as MistralAIOptions;
                        return builder.WithMistralAIConnector();
                    },
                    builder,
                    mistralAIOptions,
                    () =>
                    {
                        // Assert
                        options.Connectors!.MistralAI.Should().Be(mistralAIOptions);

                        services.Should().ContainSingle(descriptor => descriptor.Lifetime == ServiceLifetime.Singleton &&
                                                                      descriptor.ServiceType == typeof(MistralAIOptions) &&
                                                                      descriptor.ImplementationInstance == mistralAIOptions);

                        AssertMistralAIServices(services);
                    }
                },
                {
                    opt =>
                    {
                        // Act
                        options.Connectors.Ollama = opt as OllamaOptions;
                        return builder.WithOllamaConnector();
                    },
                    builder,
                    ollamaOptions,
                    () =>
                    {
                        // Assert
                        options.Connectors!.Ollama.Should().Be(ollamaOptions);

                        services.Should().ContainSingle(descriptor => descriptor.Lifetime == ServiceLifetime.Singleton &&
                                                                      descriptor.ServiceType == typeof(OllamaOptions) &&
                                                                      descriptor.ImplementationInstance == ollamaOptions);

                        AssertOllamaServices(services);
                    }
                },
                {
                    opt =>
                    {
                        // Act
                        options.Connectors.OpenAI = opt as OpenAIOptions;
                        return builder.WithOpenAIConnector();
                    },
                    builder,
                    openAIOptions,
                    () =>
                    {
                        // Assert
                        options.Connectors!.OpenAI.Should().Be(openAIOptions);

                        services.Should().ContainSingle(descriptor => descriptor.Lifetime == ServiceLifetime.Singleton &&
                                                                      descriptor.ServiceType == typeof(OpenAIOptions) &&
                                                                      descriptor.ImplementationInstance == openAIOptions);

                        AssertOpenAIServices(services);
                    }
                },
                {
                    opt =>
                    {
                        // Act
                        options.Connectors.VertexAI = opt as VertexAIOptions;
                        return builder.WithVertexAIConnector();
                    },
                    builder,
                    vertexAIOptions,
                    () =>
                    {
                        // Assert
                        options.Connectors!.VertexAI.Should().Be(vertexAIOptions);

                        services.Should().ContainSingle(descriptor => descriptor.Lifetime == ServiceLifetime.Singleton &&
                                                                      descriptor.ServiceType == typeof(VertexAIOptions) &&
                                                                      descriptor.ImplementationInstance == vertexAIOptions);

                        AssertVertexAIServices(services);
                    }
                }
            };
        }
    }

    public static TheoryData<Func<IKernelBuilder>, IKernelBuilder, Action> AddConnectorWithOptionsAction
    {
        get
        {
            var azureOpenAIOptions = new AzureOpenAIOptions
            {
                AudioToText = new AzureOpenAIAudioToTextOptions(),
                ChatCompletion = new AzureOpenAIChatCompletionOptions(),
                Files = new AzureOpenAIFilesOptions(),
                TextEmbeddingGeneration = new AzureOpenAITextEmbeddingGenerationOptions(),
                TextGeneration = new AzureOpenAITextGenerationOptions(),
                TextToAudio = new AzureOpenAITextToAudioOptions(),
                TextToImage = new AzureOpenAITextToImageOptions()
            };
            var googleOptions = new GoogleOptions
            {
                ChatCompletion = new GoogleAIChatCompletionOptions(),
                EmbeddingGeneration = new GoogleAIEmbeddingGenerationOptions()
            };
            var huggingFaceOptions = new HuggingFaceOptions
            {
                ChatCompletion = new HuggingFaceChatCompletionOptions(),
                ImageToText = new HuggingFaceImageToTextOptions(),
                TextEmbeddingGeneration = new HuggingFaceTextEmbeddingGenerationOptions(),
                TextGeneration = new HuggingFaceTextGenerationOptions()
            };
            var mistralAIOptions = new MistralAIOptions
            {
                ChatCompletion = new MistralAIChatCompletionOptions(),
                TextEmbeddingGeneration = new MistralAITextEmbeddingGenerationOptions()
            };
            var ollamaOptions = new OllamaOptions
            {
                ChatCompletion = new OllamaChatCompletionOptions(),
                TextEmbeddingGeneration = new OllamaTextEmbeddingGenerationOptions(),
                TextGeneration = new OllamaTextGenerationOptions()
            };
            var openAIOptions = new OpenAIOptions
            {
                AudioToText = new OpenAIAudioToTextOptions(),
                ChatCompletion = new OpenAIChatCompletionOptions(),
                Files = new OpenAIFilesOptions(),
                TextEmbeddingGeneration = new OpenAITextEmbeddingGenerationOptions(),
                TextToAudio = new OpenAITextToAudioOptions(),
                TextToImage = new OpenAITextToImageOptions()
            };
            var vertexAIOptions = new VertexAIOptions
            {
                ChatCompletion = new VertexAIChatCompletionOptions(),
                EmbeddingGeneration = new VertexAIEmbeddingGenerationOptions()
            };

            var options = new KernelOptions();
            var services = new ServiceCollection();
            var builder = new KernelBuilder(options, services);

            return new TheoryData<Func<IKernelBuilder>, IKernelBuilder, Action>
            {
                {
                    // Act
                    () => builder.WithAzureOpenAIConnector(opt =>
                    {
                        opt.AudioToText = azureOpenAIOptions.AudioToText;
                        opt.ChatCompletion = azureOpenAIOptions.ChatCompletion;
                        opt.Files = azureOpenAIOptions.Files;
                        opt.TextEmbeddingGeneration = azureOpenAIOptions.TextEmbeddingGeneration;
                        opt.TextGeneration = azureOpenAIOptions.TextGeneration;
                        opt.TextToAudio = azureOpenAIOptions.TextToAudio;
                        opt.TextToImage = azureOpenAIOptions.TextToImage;
                    }),
                    builder,
                    () =>
                    {
                        // Assert
                        options.Connectors!.AzureOpenAI.Should().NotBeNull();
                        options.Connectors!.AzureOpenAI!.AudioToText.Should().Be(azureOpenAIOptions.AudioToText);
                        options.Connectors!.AzureOpenAI!.ChatCompletion.Should().Be(azureOpenAIOptions.ChatCompletion);
                        options.Connectors!.AzureOpenAI!.Files.Should().Be(azureOpenAIOptions.Files);
                        options.Connectors!.AzureOpenAI!.TextEmbeddingGeneration.Should().Be(azureOpenAIOptions.TextEmbeddingGeneration);
                        options.Connectors!.AzureOpenAI!.TextGeneration.Should().Be(azureOpenAIOptions.TextGeneration);
                        options.Connectors!.AzureOpenAI!.TextToAudio.Should().Be(azureOpenAIOptions.TextToAudio);
                        options.Connectors!.AzureOpenAI!.TextToImage.Should().Be(azureOpenAIOptions.TextToImage);

                        services.Should().ContainSingle(descriptor => descriptor.Lifetime == ServiceLifetime.Singleton &&
                                                                      descriptor.ServiceType == typeof(AzureOpenAIOptions) &&
                                                                      descriptor.ImplementationInstance != null);

                        AssertAzureOpenAIServices(services);
                    }
                },
                {
                    // Act
                    () => builder.WithGoogleConnector(opt =>
                    {
                        opt.ChatCompletion = googleOptions.ChatCompletion;
                        opt.EmbeddingGeneration = googleOptions.EmbeddingGeneration;
                    }),
                    builder,
                    () =>
                    {
                        // Assert
                        options.Connectors!.Google.Should().NotBeNull();
                        options.Connectors!.Google!.ChatCompletion.Should().Be(googleOptions.ChatCompletion);
                        options.Connectors!.Google!.EmbeddingGeneration.Should().Be(googleOptions.EmbeddingGeneration);

                        services.Should().ContainSingle(descriptor => descriptor.Lifetime == ServiceLifetime.Singleton &&
                                                                      descriptor.ServiceType == typeof(GoogleOptions) &&
                                                                      descriptor.ImplementationInstance != null);

                        AssertGoogleServices(services);
                    }
                },
                {
                    // Act
                    () => builder.WithHuggingFaceConnector(opt =>
                    {
                        opt.ChatCompletion = huggingFaceOptions.ChatCompletion;
                        opt.ImageToText = huggingFaceOptions.ImageToText;
                        opt.TextEmbeddingGeneration = huggingFaceOptions.TextEmbeddingGeneration;
                        opt.TextGeneration = huggingFaceOptions.TextGeneration;
                    }),
                    builder,
                    () =>
                    {
                        // Assert
                        options.Connectors!.HuggingFace.Should().NotBeNull();
                        options.Connectors!.HuggingFace!.ChatCompletion.Should().Be(huggingFaceOptions.ChatCompletion);
                        options.Connectors!.HuggingFace!.ImageToText.Should().Be(huggingFaceOptions.ImageToText);
                        options.Connectors!.HuggingFace!.TextEmbeddingGeneration.Should().Be(huggingFaceOptions.TextEmbeddingGeneration);
                        options.Connectors!.HuggingFace!.TextGeneration.Should().Be(huggingFaceOptions.TextGeneration);

                        services.Should().ContainSingle(descriptor => descriptor.Lifetime == ServiceLifetime.Singleton &&
                                                                      descriptor.ServiceType == typeof(HuggingFaceOptions) &&
                                                                      descriptor.ImplementationInstance != null);

                        AssertHuggingFaceServices(services);
                    }
                },
                {
                    // Act
                    () => builder.WithMistralAIConnector(opt =>
                    {
                        opt.ChatCompletion = mistralAIOptions.ChatCompletion;
                        opt.TextEmbeddingGeneration = mistralAIOptions.TextEmbeddingGeneration;
                    }),
                    builder,
                    () =>
                    {
                        // Assert
                        options.Connectors!.MistralAI.Should().NotBeNull();
                        options.Connectors!.MistralAI!.ChatCompletion.Should().Be(mistralAIOptions.ChatCompletion);
                        options.Connectors!.MistralAI!.TextEmbeddingGeneration.Should().Be(mistralAIOptions.TextEmbeddingGeneration);

                        services.Should().ContainSingle(descriptor => descriptor.Lifetime == ServiceLifetime.Singleton &&
                                                                      descriptor.ServiceType == typeof(MistralAIOptions) &&
                                                                      descriptor.ImplementationInstance != null);

                        AssertMistralAIServices(services);
                    }
                },
                {
                    // Act
                    () => builder.WithOllamaConnector(opt =>
                    {
                        opt.ChatCompletion = ollamaOptions.ChatCompletion;
                        opt.TextEmbeddingGeneration = ollamaOptions.TextEmbeddingGeneration;
                        opt.TextGeneration = ollamaOptions.TextGeneration;
                    }),
                    builder,
                    () =>
                    {
                        // Assert
                        options.Connectors!.Ollama.Should().NotBeNull();
                        options.Connectors!.Ollama!.ChatCompletion.Should().Be(ollamaOptions.ChatCompletion);
                        options.Connectors!.Ollama!.TextEmbeddingGeneration.Should().Be(ollamaOptions.TextEmbeddingGeneration);
                        options.Connectors!.Ollama!.TextGeneration.Should().Be(ollamaOptions.TextGeneration);

                        services.Should().ContainSingle(descriptor => descriptor.Lifetime == ServiceLifetime.Singleton &&
                                                                      descriptor.ServiceType == typeof(OllamaOptions) &&
                                                                      descriptor.ImplementationInstance != null);

                        AssertOllamaServices(services);
                    }
                },
                {
                    // Act
                    () => builder.WithOpenAIConnector(opt =>
                    {
                        opt.AudioToText = openAIOptions.AudioToText;
                        opt.ChatCompletion = openAIOptions.ChatCompletion;
                        opt.Files = openAIOptions.Files;
                        opt.TextEmbeddingGeneration = openAIOptions.TextEmbeddingGeneration;
                        opt.TextToAudio = openAIOptions.TextToAudio;
                        opt.TextToImage = openAIOptions.TextToImage;
                    }),
                    builder,
                    () =>
                    {
                        // Assert
                        options.Connectors!.OpenAI.Should().NotBeNull();
                        options.Connectors!.OpenAI!.AudioToText.Should().Be(openAIOptions.AudioToText);
                        options.Connectors!.OpenAI!.ChatCompletion.Should().Be(openAIOptions.ChatCompletion);
                        options.Connectors!.OpenAI!.Files.Should().Be(openAIOptions.Files);
                        options.Connectors!.OpenAI!.TextEmbeddingGeneration.Should().Be(openAIOptions.TextEmbeddingGeneration);
                        options.Connectors!.OpenAI!.TextToAudio.Should().Be(openAIOptions.TextToAudio);
                        options.Connectors!.OpenAI!.TextToImage.Should().Be(openAIOptions.TextToImage);

                        services.Should().ContainSingle(descriptor => descriptor.Lifetime == ServiceLifetime.Singleton &&
                                                                      descriptor.ServiceType == typeof(OpenAIOptions) &&
                                                                      descriptor.ImplementationInstance != null);

                        AssertOpenAIServices(services);
                    }
                },
                {
                    // Act
                    () => builder.WithVertexAIConnector(opt =>
                    {
                        opt.ChatCompletion = vertexAIOptions.ChatCompletion;
                        opt.EmbeddingGeneration = vertexAIOptions.EmbeddingGeneration;
                    }),
                    builder,
                    () =>
                    {
                        // Assert
                        options.Connectors!.VertexAI.Should().NotBeNull();
                        options.Connectors!.VertexAI!.ChatCompletion.Should().Be(vertexAIOptions.ChatCompletion);
                        options.Connectors!.VertexAI!.EmbeddingGeneration.Should().Be(vertexAIOptions.EmbeddingGeneration);

                        services.Should().ContainSingle(descriptor => descriptor.Lifetime == ServiceLifetime.Singleton &&
                                                                      descriptor.ServiceType == typeof(VertexAIOptions) &&
                                                                      descriptor.ImplementationInstance != null);

                        AssertVertexAIServices(services);
                    }
                }
            };
        }
    }

    private static void AssertAzureOpenAIServices(IServiceCollection services)
    {
        services.Should().ContainSingle(descriptor => descriptor.Lifetime == ServiceLifetime.Singleton &&
                                                      descriptor.ServiceType == typeof(IKernelBuilderConfigurator) &&
                                                      descriptor.ImplementationType == typeof(AzureOpenAIKernelBuilderConfigurator));

        services.Should().ContainSingle(descriptor => descriptor.Lifetime == ServiceLifetime.Singleton &&
                                                      descriptor.ServiceType == typeof(IMemoryBuilderConfigurator) &&
                                                      descriptor.ImplementationType == typeof(AzureOpenAIMemoryBuilderConfigurator));

        services.Should().ContainSingle(descriptor => descriptor.Lifetime == ServiceLifetime.Singleton &&
                                                      descriptor.IsKeyedService == true &&
                                                      descriptor.ServiceKey == typeof(AzureOpenAIChatCompletionService) &&
                                                      descriptor.ServiceType == typeof(IPromptExecutionSettingsMapper) &&
                                                      descriptor.KeyedImplementationType == typeof(AzureOpenAIPromptExecutionSettingsMapper));
    }

    private static void AssertGoogleServices(IServiceCollection services)
    {
        services.Should().ContainSingle(descriptor => descriptor.Lifetime == ServiceLifetime.Singleton &&
                                                      descriptor.ServiceType == typeof(IKernelBuilderConfigurator) &&
                                                      descriptor.ImplementationType == typeof(GoogleKernelBuilderConfigurator));

        services.Should().ContainSingle(descriptor => descriptor.Lifetime == ServiceLifetime.Singleton &&
                                                      descriptor.IsKeyedService == true &&
                                                      descriptor.ServiceKey == typeof(GoogleAIGeminiChatCompletionService) &&
                                                      descriptor.ServiceType == typeof(IPromptExecutionSettingsMapper) &&
                                                      descriptor.KeyedImplementationType == typeof(GeminiPromptExecutionSettingsMapper));
    }

    private static void AssertHuggingFaceServices(IServiceCollection services)
    {
        services.Should().ContainSingle(descriptor => descriptor.Lifetime == ServiceLifetime.Singleton &&
                                                      descriptor.ServiceType == typeof(IKernelBuilderConfigurator) &&
                                                      descriptor.ImplementationType == typeof(HuggingFaceKernelBuilderConfigurator));

        services.Should().ContainSingle(descriptor => descriptor.Lifetime == ServiceLifetime.Singleton &&
                                                      descriptor.IsKeyedService == true &&
                                                      descriptor.ServiceKey == typeof(HuggingFaceChatCompletionService) &&
                                                      descriptor.ServiceType == typeof(IPromptExecutionSettingsMapper) &&
                                                      descriptor.KeyedImplementationType == typeof(HuggingFacePromptExecutionSettingsMapper));
    }

    private static void AssertMistralAIServices(IServiceCollection services)
    {
        services.Should().ContainSingle(descriptor => descriptor.Lifetime == ServiceLifetime.Singleton &&
                                                      descriptor.ServiceType == typeof(IKernelBuilderConfigurator) &&
                                                      descriptor.ImplementationType == typeof(MistralAIKernelBuilderConfigurator));

        services.Should().ContainSingle(descriptor => descriptor.Lifetime == ServiceLifetime.Singleton &&
                                                      descriptor.IsKeyedService == true &&
                                                      descriptor.ServiceKey == typeof(MistralAIChatCompletionService) &&
                                                      descriptor.ServiceType == typeof(IPromptExecutionSettingsMapper) &&
                                                      descriptor.KeyedImplementationType == typeof(MistralAIPromptExecutionSettingsMapper));
    }

    private static void AssertOllamaServices(IServiceCollection services)
    {
        services.Should().ContainSingle(descriptor => descriptor.Lifetime == ServiceLifetime.Singleton &&
                                                      descriptor.ServiceType == typeof(IKernelBuilderConfigurator) &&
                                                      descriptor.ImplementationType == typeof(OllamaKernelBuilderConfigurator));

        services.Should().ContainSingle(descriptor => descriptor.Lifetime == ServiceLifetime.Singleton &&
                                                      descriptor.ServiceType == typeof(IMemoryBuilderConfigurator) &&
                                                      descriptor.ImplementationType == typeof(OllamaMemoryBuilderConfigurator));

        services.Should().ContainSingle(descriptor => descriptor.Lifetime == ServiceLifetime.Singleton &&
                                                      descriptor.IsKeyedService == true &&
                                                      descriptor.ServiceKey == typeof(OllamaChatCompletionService) &&
                                                      descriptor.ServiceType == typeof(IPromptExecutionSettingsMapper) &&
                                                      descriptor.KeyedImplementationType == typeof(OllamaPromptExecutionSettingsMapper));
    }

    private static void AssertOpenAIServices(IServiceCollection services)
    {
        services.Should().ContainSingle(descriptor => descriptor.Lifetime == ServiceLifetime.Singleton &&
                                                      descriptor.ServiceType == typeof(IKernelBuilderConfigurator) &&
                                                      descriptor.ImplementationType == typeof(OpenAIKernelBuilderConfigurator));

        services.Should().ContainSingle(descriptor => descriptor.Lifetime == ServiceLifetime.Singleton &&
                                                      descriptor.ServiceType == typeof(IMemoryBuilderConfigurator) &&
                                                      descriptor.ImplementationType == typeof(OpenAIMemoryBuilderConfigurator));

        services.Should().ContainSingle(descriptor => descriptor.Lifetime == ServiceLifetime.Singleton &&
                                                      descriptor.IsKeyedService == true &&
                                                      descriptor.ServiceKey == typeof(OpenAIChatCompletionService) &&
                                                      descriptor.ServiceType == typeof(IPromptExecutionSettingsMapper) &&
                                                      descriptor.KeyedImplementationType == typeof(OpenAIPromptExecutionSettingsMapper));

        services.Should().ContainSingle(descriptor => descriptor.Lifetime == ServiceLifetime.Singleton &&
                                                      descriptor.IsKeyedService == true &&
                                                      descriptor.ServiceKey == typeof(OpenAIAudioToTextService) &&
                                                      descriptor.ServiceType == typeof(IPromptExecutionSettingsMapper) &&
                                                      descriptor.KeyedImplementationType == typeof(OpenAIAudioToTextExecutionSettingsMapper));

        services.Should().ContainSingle(descriptor => descriptor.Lifetime == ServiceLifetime.Singleton &&
                                                      descriptor.IsKeyedService == true &&
                                                      descriptor.ServiceKey == typeof(OpenAITextToAudioService) &&
                                                      descriptor.ServiceType == typeof(IPromptExecutionSettingsMapper) &&
                                                      descriptor.KeyedImplementationType == typeof(OpenAITextToAudioExecutionSettingsMapper));
    }

    private static void AssertVertexAIServices(IServiceCollection services)
    {
        services.Should().ContainSingle(descriptor => descriptor.Lifetime == ServiceLifetime.Singleton &&
                                                      descriptor.ServiceType == typeof(IKernelBuilderConfigurator) &&
                                                      descriptor.ImplementationType == typeof(VertexAIKernelBuilderConfigurator));

        services.Should().ContainSingle(descriptor => descriptor.Lifetime == ServiceLifetime.Singleton &&
                                                      descriptor.IsKeyedService == true &&
                                                      descriptor.ServiceKey == typeof(VertexAIGeminiChatCompletionService) &&
                                                      descriptor.ServiceType == typeof(IPromptExecutionSettingsMapper) &&
                                                      descriptor.KeyedImplementationType == typeof(GeminiPromptExecutionSettingsMapper));
    }
}
