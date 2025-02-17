using AIToolbox.Options.Connectors;
using Microsoft.Extensions.DependencyInjection;

namespace AIToolbox.DependencyInjection;

internal sealed class GlobalConnectorBuilder : IGlobalConnectorBuilder
{
    private readonly GlobalConnectorOptions _options;
    private readonly IServiceCollection _services;

    public GlobalConnectorBuilder(GlobalConnectorOptions options, IServiceCollection services)
    {
        Verify.ThrowIfNull(options, nameof(options));
        Verify.ThrowIfNull(services, nameof(services));

        _options = options;
        _services = services;
    }

    /// <inheritdoc />
    public IGlobalConnectorBuilder AddAzureOpenAIOptions(GlobalAzureOpenAIOptions? options = null)
    {
        if (options is not null)
        {
            _options.AzureOpenAI = options;
        }

        Verify.ThrowIfOptionsNull(_options.AzureOpenAI);

        _services.AddSingleton(_options.AzureOpenAI!);

        return this;
    }

    /// <inheritdoc />
    public IGlobalConnectorBuilder AddAzureOpenAIOptions(Action<GlobalAzureOpenAIOptions> optionsAction)
    {
        Verify.ThrowIfNull(optionsAction, nameof(optionsAction));

        _options.AzureOpenAI ??= new GlobalAzureOpenAIOptions();
        optionsAction(_options.AzureOpenAI);

        return AddAzureOpenAIOptions(_options.AzureOpenAI);
    }

    /// <inheritdoc />
    public IGlobalConnectorBuilder AddGoogleOptions(GlobalGoogleOptions? options = null)
    {
        if (options is not null)
        {
            _options.Google = options;
        }

        Verify.ThrowIfOptionsNull(_options.Google);

        _services.AddSingleton(_options.Google!);

        return this;
    }

    /// <inheritdoc />
    public IGlobalConnectorBuilder AddGoogleOptions(Action<GlobalGoogleOptions> optionsAction)
    {
        Verify.ThrowIfNull(optionsAction, nameof(optionsAction));

        _options.Google ??= new GlobalGoogleOptions();
        optionsAction(_options.Google);

        return AddGoogleOptions(_options.Google);
    }

    /// <inheritdoc />
    public IGlobalConnectorBuilder AddHuggingFaceOptions(GlobalHuggingFaceOptions? options = null)
    {
        if (options is not null)
        {
            _options.HuggingFace = options;
        }

        Verify.ThrowIfOptionsNull(_options.HuggingFace);

        _services.AddSingleton(_options.HuggingFace!);

        return this;
    }

    /// <inheritdoc />
    public IGlobalConnectorBuilder AddHuggingFaceOptions(Action<GlobalHuggingFaceOptions> optionsAction)
    {
        Verify.ThrowIfNull(optionsAction, nameof(optionsAction));

        _options.HuggingFace ??= new GlobalHuggingFaceOptions();
        optionsAction(_options.HuggingFace);

        return AddHuggingFaceOptions(_options.HuggingFace);
    }

    /// <inheritdoc />
    public IGlobalConnectorBuilder AddMistralAIOptions(GlobalMistralAIOptions? options = null)
    {
        if (options is not null)
        {
            _options.MistralAI = options;
        }

        Verify.ThrowIfOptionsNull(_options.MistralAI);

        _services.AddSingleton(_options.MistralAI!);

        return this;
    }

    /// <inheritdoc />
    public IGlobalConnectorBuilder AddMistralAIOptions(Action<GlobalMistralAIOptions> optionsAction)
    {
        Verify.ThrowIfNull(optionsAction, nameof(optionsAction));

        _options.MistralAI ??= new GlobalMistralAIOptions();
        optionsAction(_options.MistralAI);

        return AddMistralAIOptions(_options.MistralAI);
    }

    /// <inheritdoc />
    public IGlobalConnectorBuilder AddOllamaOptions(GlobalOllamaOptions? options = null)
    {
        if (options is not null)
        {
            _options.Ollama = options;
        }

        Verify.ThrowIfOptionsNull(_options.Ollama);

        _services.AddSingleton(_options.Ollama!);

        return this;
    }

    /// <inheritdoc />
    public IGlobalConnectorBuilder AddOllamaOptions(Action<GlobalOllamaOptions> optionsAction)
    {
        Verify.ThrowIfNull(optionsAction, nameof(optionsAction));

        _options.Ollama ??= new GlobalOllamaOptions();
        optionsAction(_options.Ollama);

        return AddOllamaOptions(_options.Ollama);
    }

    /// <inheritdoc />
    public IGlobalConnectorBuilder AddOpenAIOptions(GlobalOpenAIOptions? options = null)
    {
        if (options is not null)
        {
            _options.OpenAI = options;
        }

        Verify.ThrowIfOptionsNull(_options.OpenAI);

        _services.AddSingleton(_options.OpenAI!);

        return this;
    }

    /// <inheritdoc />
    public IGlobalConnectorBuilder AddOpenAIOptions(Action<GlobalOpenAIOptions> optionsAction)
    {
        Verify.ThrowIfNull(optionsAction, nameof(optionsAction));

        _options.OpenAI ??= new GlobalOpenAIOptions();
        optionsAction(_options.OpenAI);

        return AddOpenAIOptions(_options.OpenAI);
    }

    /// <inheritdoc />
    public IGlobalConnectorBuilder AddVertexAIOptions(GlobalVertexAIOptions? options = null)
    {
        if (options is not null)
        {
            _options.VertexAI = options;
        }

        Verify.ThrowIfOptionsNull(_options.VertexAI);

        _services.AddSingleton(_options.VertexAI!);

        return this;
    }

    /// <inheritdoc />
    public IGlobalConnectorBuilder AddVertexAIOptions(Action<GlobalVertexAIOptions> optionsAction)
    {
        Verify.ThrowIfNull(optionsAction, nameof(optionsAction));

        _options.VertexAI ??= new GlobalVertexAIOptions();
        optionsAction(_options.VertexAI);

        return AddVertexAIOptions(_options.VertexAI);
    }
}
