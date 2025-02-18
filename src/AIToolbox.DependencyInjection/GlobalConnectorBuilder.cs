using AIToolbox.Options.Connectors;
using Microsoft.Extensions.DependencyInjection;

namespace AIToolbox.DependencyInjection;

internal sealed class GlobalConnectorBuilder : IGlobalConnectorBuilder
{
    private readonly GlobalConnectorOptions _options;
    private readonly IServiceCollection _services;

    public GlobalConnectorBuilder(GlobalConnectorOptions options, IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(options, nameof(options));
        ArgumentNullException.ThrowIfNull(services, nameof(services));

        _options = options;
        _services = services;
    }

    /// <inheritdoc />
    public IGlobalConnectorBuilder AddGlobalAzureOpenAIOptions()
    {
        Verify.ThrowIfOptionsNull(_options.AzureOpenAI);

        _services.AddSingleton(_options.AzureOpenAI!);

        return this;
    }

    /// <inheritdoc />
    public IGlobalConnectorBuilder AddGlobalAzureOpenAIOptions(Action<GlobalAzureOpenAIOptions> optionsAction)
    {
        ArgumentNullException.ThrowIfNull(optionsAction, nameof(optionsAction));

        _options.AzureOpenAI ??= new GlobalAzureOpenAIOptions();
        optionsAction(_options.AzureOpenAI);

        return AddGlobalAzureOpenAIOptions();
    }

    /// <inheritdoc />
    public IGlobalConnectorBuilder AddGlobalGoogleOptions()
    {
        Verify.ThrowIfOptionsNull(_options.Google);

        _services.AddSingleton(_options.Google!);

        return this;
    }

    /// <inheritdoc />
    public IGlobalConnectorBuilder AddGlobalGoogleOptions(Action<GlobalGoogleOptions> optionsAction)
    {
        ArgumentNullException.ThrowIfNull(optionsAction, nameof(optionsAction));

        _options.Google ??= new GlobalGoogleOptions();
        optionsAction(_options.Google);

        return AddGlobalGoogleOptions();
    }

    /// <inheritdoc />
    public IGlobalConnectorBuilder AddGlobalHuggingFaceOptions()
    {
        Verify.ThrowIfOptionsNull(_options.HuggingFace);

        _services.AddSingleton(_options.HuggingFace!);

        return this;
    }

    /// <inheritdoc />
    public IGlobalConnectorBuilder AddGlobalHuggingFaceOptions(Action<GlobalHuggingFaceOptions> optionsAction)
    {
        ArgumentNullException.ThrowIfNull(optionsAction, nameof(optionsAction));

        _options.HuggingFace ??= new GlobalHuggingFaceOptions();
        optionsAction(_options.HuggingFace);

        return AddGlobalHuggingFaceOptions();
    }

    /// <inheritdoc />
    public IGlobalConnectorBuilder AddGlobalMistralAIOptions()
    {
        Verify.ThrowIfOptionsNull(_options.MistralAI);

        _services.AddSingleton(_options.MistralAI!);

        return this;
    }

    /// <inheritdoc />
    public IGlobalConnectorBuilder AddGlobalMistralAIOptions(Action<GlobalMistralAIOptions> optionsAction)
    {
        ArgumentNullException.ThrowIfNull(optionsAction, nameof(optionsAction));

        _options.MistralAI ??= new GlobalMistralAIOptions();
        optionsAction(_options.MistralAI);

        return AddGlobalMistralAIOptions();
    }

    /// <inheritdoc />
    public IGlobalConnectorBuilder AddGlobalOllamaOptions()
    {
        Verify.ThrowIfOptionsNull(_options.Ollama);

        _services.AddSingleton(_options.Ollama!);

        return this;
    }

    /// <inheritdoc />
    public IGlobalConnectorBuilder AddGlobalOllamaOptions(Action<GlobalOllamaOptions> optionsAction)
    {
        ArgumentNullException.ThrowIfNull(optionsAction, nameof(optionsAction));

        _options.Ollama ??= new GlobalOllamaOptions();
        optionsAction(_options.Ollama);

        return AddGlobalOllamaOptions();
    }

    /// <inheritdoc />
    public IGlobalConnectorBuilder AddGlobalOpenAIOptions()
    {
        Verify.ThrowIfOptionsNull(_options.OpenAI);

        _services.AddSingleton(_options.OpenAI!);

        return this;
    }

    /// <inheritdoc />
    public IGlobalConnectorBuilder AddGlobalOpenAIOptions(Action<GlobalOpenAIOptions> optionsAction)
    {
        ArgumentNullException.ThrowIfNull(optionsAction, nameof(optionsAction));

        _options.OpenAI ??= new GlobalOpenAIOptions();
        optionsAction(_options.OpenAI);

        return AddGlobalOpenAIOptions();
    }

    /// <inheritdoc />
    public IGlobalConnectorBuilder AddGlobalVertexAIOptions()
    {
        Verify.ThrowIfOptionsNull(_options.VertexAI);

        _services.AddSingleton(_options.VertexAI!);

        return this;
    }

    /// <inheritdoc />
    public IGlobalConnectorBuilder AddGlobalVertexAIOptions(Action<GlobalVertexAIOptions> optionsAction)
    {
        ArgumentNullException.ThrowIfNull(optionsAction, nameof(optionsAction));

        _options.VertexAI ??= new GlobalVertexAIOptions();
        optionsAction(_options.VertexAI);

        return AddGlobalVertexAIOptions();
    }
}
