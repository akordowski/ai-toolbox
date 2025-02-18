using AIToolbox.Options.Connectors;

namespace AIToolbox.DependencyInjection;

public interface IGlobalConnectorBuilder
{
    /// <summary>
    /// Adds <see cref="GlobalAzureOpenAIOptions"/>.
    /// </summary>
    /// <returns>The instance of the <see cref="IGlobalConnectorBuilder"/>.</returns>
    public IGlobalConnectorBuilder AddGlobalAzureOpenAIOptions();

    /// <summary>
    /// Adds <see cref="GlobalAzureOpenAIOptions"/>.
    /// </summary>
    /// <param name="optionsAction">A delegate that is used to configure the <see cref="GlobalAzureOpenAIOptions"/>.</param>
    /// <returns>The instance of the <see cref="IGlobalConnectorBuilder"/>.</returns>
    public IGlobalConnectorBuilder AddGlobalAzureOpenAIOptions(Action<GlobalAzureOpenAIOptions> optionsAction);

    /// <summary>
    /// Adds <see cref="GlobalGoogleOptions"/>.
    /// </summary>
    /// <returns>The instance of the <see cref="IGlobalConnectorBuilder"/>.</returns>
    public IGlobalConnectorBuilder AddGlobalGoogleOptions();

    /// <summary>
    /// Adds <see cref="GlobalGoogleOptions"/>.
    /// </summary>
    /// <param name="optionsAction">A delegate that is used to configure the <see cref="GlobalGoogleOptions"/>.</param>
    /// <returns>The instance of the <see cref="IGlobalConnectorBuilder"/>.</returns>
    public IGlobalConnectorBuilder AddGlobalGoogleOptions(Action<GlobalGoogleOptions> optionsAction);

    /// <summary>
    /// Adds <see cref="GlobalHuggingFaceOptions"/>.
    /// </summary>
    /// <returns>The instance of the <see cref="IGlobalConnectorBuilder"/>.</returns>
    public IGlobalConnectorBuilder AddGlobalHuggingFaceOptions();

    /// <summary>
    /// Adds <see cref="GlobalHuggingFaceOptions"/>.
    /// </summary>
    /// <param name="optionsAction">A delegate that is used to configure the <see cref="GlobalHuggingFaceOptions"/>.</param>
    /// <returns>The instance of the <see cref="IGlobalConnectorBuilder"/>.</returns>
    public IGlobalConnectorBuilder AddGlobalHuggingFaceOptions(Action<GlobalHuggingFaceOptions> optionsAction);

    /// <summary>
    /// Adds <see cref="GlobalMistralAIOptions"/>.
    /// </summary>
    /// <returns>The instance of the <see cref="IGlobalConnectorBuilder"/>.</returns>
    public IGlobalConnectorBuilder AddGlobalMistralAIOptions();

    /// <summary>
    /// Adds <see cref="GlobalMistralAIOptions"/>.
    /// </summary>
    /// <param name="optionsAction">A delegate that is used to configure the <see cref="GlobalMistralAIOptions"/>.</param>
    /// <returns>The instance of the <see cref="IGlobalConnectorBuilder"/>.</returns>
    public IGlobalConnectorBuilder AddGlobalMistralAIOptions(Action<GlobalMistralAIOptions> optionsAction);

    /// <summary>
    /// Adds <see cref="GlobalOllamaOptions"/>.
    /// </summary>
    /// <returns>The instance of the <see cref="IGlobalConnectorBuilder"/>.</returns>
    public IGlobalConnectorBuilder AddGlobalOllamaOptions();

    /// <summary>
    /// Adds <see cref="GlobalOllamaOptions"/>.
    /// </summary>
    /// <param name="optionsAction">A delegate that is used to configure the <see cref="GlobalOllamaOptions"/>.</param>
    /// <returns>The instance of the <see cref="IGlobalConnectorBuilder"/>.</returns>
    public IGlobalConnectorBuilder AddGlobalOllamaOptions(Action<GlobalOllamaOptions> optionsAction);

    /// <summary>
    /// Adds <see cref="GlobalOpenAIOptions"/>.
    /// </summary>
    /// <returns>The instance of the <see cref="IGlobalConnectorBuilder"/>.</returns>
    public IGlobalConnectorBuilder AddGlobalOpenAIOptions();

    /// <summary>
    /// Adds <see cref="GlobalOpenAIOptions"/>.
    /// </summary>
    /// <param name="optionsAction">A delegate that is used to configure the <see cref="GlobalOpenAIOptions"/>.</param>
    /// <returns>The instance of the <see cref="IGlobalConnectorBuilder"/>.</returns>
    public IGlobalConnectorBuilder AddGlobalOpenAIOptions(Action<GlobalOpenAIOptions> optionsAction);

    /// <summary>
    /// Adds <see cref="GlobalVertexAIOptions"/>.
    /// </summary>
    /// <returns>The instance of the <see cref="IGlobalConnectorBuilder"/>.</returns>
    public IGlobalConnectorBuilder AddGlobalVertexAIOptions();

    /// <summary>
    /// Adds <see cref="GlobalVertexAIOptions"/>.
    /// </summary>
    /// <param name="optionsAction">A delegate that is used to configure the <see cref="GlobalVertexAIOptions"/>.</param>
    /// <returns>The instance of the <see cref="IGlobalConnectorBuilder"/>.</returns>
    public IGlobalConnectorBuilder AddGlobalVertexAIOptions(Action<GlobalVertexAIOptions> optionsAction);
}
