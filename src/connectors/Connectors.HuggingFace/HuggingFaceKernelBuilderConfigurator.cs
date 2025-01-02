using AIToolbox.Options.Connectors;
using AIToolbox.Options.SemanticKernel;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Connectors.HuggingFace;

namespace AIToolbox.SemanticKernel;

public sealed class HuggingFaceKernelBuilderConfigurator : IKernelBuilderConfigurator
{
    private readonly KernelOptions _kernelOptions;
    private readonly HuggingFaceConnectorOptions? _huggingFaceConnectorOptions;
    private readonly IHttpClientFactory? _httpClientFactory;

    public HuggingFaceKernelBuilderConfigurator(
        KernelOptions kernelOptions,
        HuggingFaceConnectorOptions? huggingFaceConnectorOptions = null,
        IHttpClientFactory? httpClientFactory = null)
    {
        ArgumentNullException.ThrowIfNull(kernelOptions, nameof(kernelOptions));

        _kernelOptions = kernelOptions;
        _huggingFaceConnectorOptions = huggingFaceConnectorOptions;
        _httpClientFactory = httpClientFactory;
    }

    public void Configure(IKernelBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(builder, nameof(builder));

        ConfigureChatCompletion(builder);
        ConfigureImageToText(builder);
        ConfigureTextEmbeddingGeneration(builder);
        ConfigureTextGeneration(builder);
        ConfigurePromptExecutionSettingsMapper(builder);
    }

    private void ConfigureChatCompletion(IKernelBuilder builder)
    {
        var options = _kernelOptions.Connectors?.HuggingFace?.ChatCompletion;

        if (options is null)
        {
            return;
        }

        if (_huggingFaceConnectorOptions is not null)
        {
            options.Endpoint ??= _huggingFaceConnectorOptions.Endpoint;
            options.ApiKey ??= _huggingFaceConnectorOptions.ApiKey;
            options.ServiceId ??= _huggingFaceConnectorOptions.ServiceId;
        }

#pragma warning disable CA2000 // Dispose objects before losing scope
        builder.AddHuggingFaceChatCompletion(
            model: options.Model,
            endpoint: new Uri(options.Endpoint!),
            apiKey: options.ApiKey,
            serviceId: options.ServiceId,
            httpClient: _httpClientFactory?.CreateClient());
#pragma warning restore CA2000 // Dispose objects before losing scope
    }

    private void ConfigureImageToText(IKernelBuilder builder)
    {
        var options = _kernelOptions.Connectors?.HuggingFace?.ImageToText;

        if (options is null)
        {
            return;
        }

        if (_huggingFaceConnectorOptions is not null)
        {
            options.Endpoint ??= _huggingFaceConnectorOptions.Endpoint;
            options.ApiKey ??= _huggingFaceConnectorOptions.ApiKey;
            options.ServiceId ??= _huggingFaceConnectorOptions.ServiceId;
        }

#pragma warning disable CA2000 // Dispose objects before losing scope
        builder.AddHuggingFaceImageToText(
            model: options.Model,
            endpoint: new Uri(options.Endpoint!),
            apiKey: options.ApiKey,
            serviceId: options.ServiceId,
            httpClient: _httpClientFactory?.CreateClient());
#pragma warning restore CA2000 // Dispose objects before losing scope
    }

    private void ConfigureTextEmbeddingGeneration(IKernelBuilder builder)
    {
        var options = _kernelOptions.Connectors?.HuggingFace?.TextEmbeddingGeneration;

        if (options is null)
        {
            return;
        }

        if (_huggingFaceConnectorOptions is not null)
        {
            options.Endpoint ??= _huggingFaceConnectorOptions.Endpoint;
            options.ApiKey ??= _huggingFaceConnectorOptions.ApiKey;
            options.ServiceId ??= _huggingFaceConnectorOptions.ServiceId;
        }

#pragma warning disable CA2000 // Dispose objects before losing scope
        builder.AddHuggingFaceTextEmbeddingGeneration(
            model: options.Model,
            endpoint: new Uri(options.Endpoint!),
            apiKey: options.ApiKey,
            serviceId: options.ServiceId,
            httpClient: _httpClientFactory?.CreateClient());
#pragma warning restore CA2000 // Dispose objects before losing scope
    }

    private void ConfigureTextGeneration(IKernelBuilder builder)
    {
        var options = _kernelOptions.Connectors?.HuggingFace?.TextGeneration;

        if (options is null)
        {
            return;
        }

        if (_huggingFaceConnectorOptions is not null)
        {
            options.Endpoint ??= _huggingFaceConnectorOptions.Endpoint;
            options.ApiKey ??= _huggingFaceConnectorOptions.ApiKey;
            options.ServiceId ??= _huggingFaceConnectorOptions.ServiceId;
        }

#pragma warning disable CA2000 // Dispose objects before losing scope
        builder.AddHuggingFaceTextGeneration(
            model: options.Model,
            endpoint: new Uri(options.Endpoint!),
            apiKey: options.ApiKey,
            serviceId: options.ServiceId,
            httpClient: _httpClientFactory?.CreateClient());
#pragma warning restore CA2000 // Dispose objects before losing scope
    }

    private static void ConfigurePromptExecutionSettingsMapper(IKernelBuilder builder)
    {
        // HuggingFacePromptExecutionSettings
        builder.Services.AddKeyedSingleton<IPromptExecutionSettingsMapper, HuggingFacePromptExecutionSettingsMapper>(typeof(HuggingFaceChatCompletionService));
    }
}
