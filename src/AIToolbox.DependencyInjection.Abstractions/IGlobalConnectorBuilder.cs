using AIToolbox.Options.Connectors;

namespace AIToolbox.DependencyInjection;

public interface IGlobalConnectorBuilder
{
    public IGlobalConnectorBuilder AddAzureOpenAIOptions(GlobalAzureOpenAIOptions? options = null);

    public IGlobalConnectorBuilder AddAzureOpenAIOptions(Action<GlobalAzureOpenAIOptions> optionsAction);

    public IGlobalConnectorBuilder AddGoogleOptions(GlobalGoogleOptions? options = null);

    public IGlobalConnectorBuilder AddGoogleOptions(Action<GlobalGoogleOptions> optionsAction);

    public IGlobalConnectorBuilder AddHuggingFaceOptions(GlobalHuggingFaceOptions? options = null);

    public IGlobalConnectorBuilder AddHuggingFaceOptions(Action<GlobalHuggingFaceOptions> optionsAction);

    public IGlobalConnectorBuilder AddMistralAIOptions(GlobalMistralAIOptions? options = null);

    public IGlobalConnectorBuilder AddMistralAIOptions(Action<GlobalMistralAIOptions> optionsAction);

    public IGlobalConnectorBuilder AddOllamaOptions(GlobalOllamaOptions? options = null);

    public IGlobalConnectorBuilder AddOllamaOptions(Action<GlobalOllamaOptions> optionsAction);

    public IGlobalConnectorBuilder AddOpenAIOptions(GlobalOpenAIOptions? options = null);

    public IGlobalConnectorBuilder AddOpenAIOptions(Action<GlobalOpenAIOptions> optionsAction);

    public IGlobalConnectorBuilder AddVertexAIOptions(GlobalVertexAIOptions? options = null);

    public IGlobalConnectorBuilder AddVertexAIOptions(Action<GlobalVertexAIOptions> optionsAction);
}
