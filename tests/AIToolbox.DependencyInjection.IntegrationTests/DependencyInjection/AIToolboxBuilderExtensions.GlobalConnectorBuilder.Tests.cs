using AIToolbox.Options.Connectors;
using AIToolbox.Options.Enums;
using AIToolbox.Tests;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Xunit.DependencyInjection;

namespace AIToolbox.DependencyInjection;

public class AIToolboxBuilderExtensionsGlobalConnectorBuilderTests : BaseTestWithFixture<AIToolboxFixture>
{
    private readonly GlobalAzureOpenAIOptions _azureOpenAIOptions = new()
    {
        Endpoint = "GlobalAzureOpenAIOptionsEndpoint",
        ApiKey = "GlobalAzureOpenAIOptionsApiKey",
        ServiceId = "GlobalAzureOpenAIOptionsServiceId"
    };
    private readonly GlobalGoogleOptions _googleOptions = new()
    {
        ApiKey = "GlobalGoogleOptionsApiKey",
        ApiVersion = GoogleAIVersion.V1,
        ServiceId = "GlobalGoogleOptionsServiceId"
    };
    private readonly GlobalHuggingFaceOptions _huggingFaceOptions = new()
    {
        Endpoint = "GlobalHuggingFaceOptionsEndpoint",
        ApiKey = "GlobalHuggingFaceOptionsApiKey",
        ServiceId = "GlobalHuggingFaceOptionsServiceId"
    };
    private readonly GlobalMistralAIOptions _mistralAIOptions = new()
    {
        Endpoint = "GlobalMistralAIOptionsEndpoint",
        ApiKey = "GlobalMistralAIOptionsApiKey",
        ServiceId = "GlobalMistralAIOptionsServiceId"
    };
    private readonly GlobalOllamaOptions _ollamaOptions = new()
    {
        Endpoint = "GlobalOllamaOptionsEndpoint",
        ServiceId = "GlobalOllamaOptionsServiceId"
    };
    private readonly GlobalOpenAIOptions _openAIOptions = new()
    {
        ApiKey = "GlobalOpenAIOptionsApiKey",
        OrgId = "GlobalOpenAIOptionsOrgId",
        ServiceId = "GlobalOpenAIOptionsServiceId"
    };
    private readonly GlobalVertexAIOptions _vertexAIOptions = new()
    {
        BearerKey = "GlobalVertexAIOptionsBearerKey",
        Location = "GlobalVertexAIOptionsLocation",
        ProjectId = "GlobalVertexAIOptionsProjectId",
        ApiVersion = VertexAIVersion.V1,
        ServiceId = "GlobalVertexAIOptionsServiceId"
    };

    public AIToolboxBuilderExtensionsGlobalConnectorBuilderTests(AIToolboxFixture fixture)
        : base(fixture)
    {
    }

    [Fact]
    public void Should_Get_Configured_Options_With_Default_Options()
    {
        var host = Fixture.GetHost((_, services) =>
        {
            services
                .AddAIToolbox(
                    builder =>
                    {
                        builder.ConfigureGlobalConnectorOptions(bldr =>
                        {
                            bldr.AddGlobalAzureOpenAIOptions()
                                .AddGlobalGoogleOptions()
                                .AddGlobalHuggingFaceOptions()
                                .AddGlobalMistralAIOptions()
                                .AddGlobalOllamaOptions()
                                .AddGlobalOpenAIOptions()
                                .AddGlobalVertexAIOptions();
                        });
                    },
                    options =>
                    {
                        options.GlobalConnectors = new GlobalConnectorOptions
                        {
                            AzureOpenAI = _azureOpenAIOptions,
                            Google = _googleOptions,
                            HuggingFace = _huggingFaceOptions,
                            MistralAI = _mistralAIOptions,
                            Ollama = _ollamaOptions,
                            OpenAI = _openAIOptions,
                            VertexAI = _vertexAIOptions
                        };
                    });
        });

        // Assert
        host.Services.GetService<GlobalAzureOpenAIOptions>().Should().Be(_azureOpenAIOptions);
        host.Services.GetService<GlobalGoogleOptions>().Should().Be(_googleOptions);
        host.Services.GetService<GlobalHuggingFaceOptions>().Should().Be(_huggingFaceOptions);
        host.Services.GetService<GlobalMistralAIOptions>().Should().Be(_mistralAIOptions);
        host.Services.GetService<GlobalOllamaOptions>().Should().Be(_ollamaOptions);
        host.Services.GetService<GlobalOpenAIOptions>().Should().Be(_openAIOptions);
        host.Services.GetService<GlobalVertexAIOptions>().Should().Be(_vertexAIOptions);

        AssertOptions(host.Services);
    }

    [Fact]
    public void Should_Get_Configured_Options_With_Options_Action()
    {
        var host = Fixture.GetHost((_, services) =>
        {
            services
                .AddAIToolbox(builder =>
                {
                    builder.ConfigureGlobalConnectorOptions(bldr =>
                    {
                        bldr.AddGlobalAzureOpenAIOptions(options =>
                            {
                                options.Endpoint = _azureOpenAIOptions.Endpoint;
                                options.ApiKey = _azureOpenAIOptions.ApiKey;
                                options.ServiceId = _azureOpenAIOptions.ServiceId;
                            })
                            .AddGlobalGoogleOptions(options =>
                            {
                                options.ApiKey = _googleOptions.ApiKey;
                                options.ApiVersion = _googleOptions.ApiVersion;
                                options.ServiceId = _googleOptions.ServiceId;
                            })
                            .AddGlobalHuggingFaceOptions(options =>
                            {
                                options.Endpoint = _huggingFaceOptions.Endpoint;
                                options.ApiKey = _huggingFaceOptions.ApiKey;
                                options.ServiceId = _huggingFaceOptions.ServiceId;
                            })
                            .AddGlobalMistralAIOptions(options =>
                            {
                                options.Endpoint = _mistralAIOptions.Endpoint;
                                options.ApiKey = _mistralAIOptions.ApiKey;
                                options.ServiceId = _mistralAIOptions.ServiceId;
                            })
                            .AddGlobalOllamaOptions(options =>
                            {
                                options.Endpoint = _ollamaOptions.Endpoint;
                                options.ServiceId = _ollamaOptions.ServiceId;
                            })
                            .AddGlobalOpenAIOptions(options =>
                            {
                                options.ApiKey = _openAIOptions.ApiKey;
                                options.OrgId = _openAIOptions.OrgId;
                                options.ServiceId = _openAIOptions.ServiceId;
                            })
                            .AddGlobalVertexAIOptions(options =>
                            {
                                options.BearerKey = _vertexAIOptions.BearerKey;
                                options.Location = _vertexAIOptions.Location;
                                options.ProjectId = _vertexAIOptions.ProjectId;
                                options.ApiVersion = _vertexAIOptions.ApiVersion;
                                options.ServiceId = _vertexAIOptions.ServiceId;
                            });
                    });
                });
        });

        // Assert
        AssertOptions(host.Services);
    }

    [Fact]
    public void Should_Get_Configured_Options_With_Config_File()
    {
        var host = Fixture.GetHost(
            (context, services) =>
            {
                services
                    .AddAIToolbox(
                        builder =>
                        {
                            builder.ConfigureGlobalConnectorOptions(bldr =>
                            {
                                bldr.AddGlobalAzureOpenAIOptions()
                                    .AddGlobalGoogleOptions()
                                    .AddGlobalHuggingFaceOptions()
                                    .AddGlobalMistralAIOptions()
                                    .AddGlobalOllamaOptions()
                                    .AddGlobalOpenAIOptions()
                                    .AddGlobalVertexAIOptions();
                            });
                        },
                        context.Configuration);
            },
            "ConfigAIToolboxBuilderExtensionsGlobalConnectorBuilderTests.json");

        // Assert
        AssertOptions(host.Services);
    }

    private void AssertOptions(IServiceProvider services)
    {
        var resultAzureOpenAIOptions = services.GetRequiredService<GlobalAzureOpenAIOptions>();
        resultAzureOpenAIOptions.Endpoint.Should().Be(_azureOpenAIOptions.Endpoint);
        resultAzureOpenAIOptions.ApiKey.Should().Be(_azureOpenAIOptions.ApiKey);
        resultAzureOpenAIOptions.ServiceId.Should().Be(_azureOpenAIOptions.ServiceId);

        var resultGoogleOptions = services.GetRequiredService<GlobalGoogleOptions>();
        resultGoogleOptions.ApiKey.Should().Be(_googleOptions.ApiKey);
        resultGoogleOptions.ApiVersion.Should().Be(_googleOptions.ApiVersion);
        resultGoogleOptions.ServiceId.Should().Be(_googleOptions.ServiceId);

        var resultHuggingFaceOptions = services.GetRequiredService<GlobalHuggingFaceOptions>();
        resultHuggingFaceOptions.Endpoint.Should().Be(_huggingFaceOptions.Endpoint);
        resultHuggingFaceOptions.ApiKey.Should().Be(_huggingFaceOptions.ApiKey);
        resultHuggingFaceOptions.ServiceId.Should().Be(_huggingFaceOptions.ServiceId);

        var resultMistralAIOptions = services.GetRequiredService<GlobalMistralAIOptions>();
        resultMistralAIOptions.Endpoint.Should().Be(_mistralAIOptions.Endpoint);
        resultMistralAIOptions.ApiKey.Should().Be(_mistralAIOptions.ApiKey);
        resultMistralAIOptions.ServiceId.Should().Be(_mistralAIOptions.ServiceId);

        var resultOllamaOptions = services.GetRequiredService<GlobalOllamaOptions>();
        resultOllamaOptions.Endpoint.Should().Be(_ollamaOptions.Endpoint);
        resultOllamaOptions.ServiceId.Should().Be(_ollamaOptions.ServiceId);

        var resultOpenAIOptions = services.GetRequiredService<GlobalOpenAIOptions>();
        resultOpenAIOptions.ApiKey.Should().Be(_openAIOptions.ApiKey);
        resultOpenAIOptions.OrgId.Should().Be(_openAIOptions.OrgId);
        resultOpenAIOptions.ServiceId.Should().Be(_openAIOptions.ServiceId);

        var resultVertexAIOptions = services.GetRequiredService<GlobalVertexAIOptions>();
        resultVertexAIOptions.BearerKey.Should().Be(_vertexAIOptions.BearerKey);
        resultVertexAIOptions.Location.Should().Be(_vertexAIOptions.Location);
        resultVertexAIOptions.ProjectId.Should().Be(_vertexAIOptions.ProjectId);
        resultVertexAIOptions.ApiVersion.Should().Be(_vertexAIOptions.ApiVersion);
        resultVertexAIOptions.ServiceId.Should().Be(_vertexAIOptions.ServiceId);
    }
}
