namespace AIToolbox.Options.SemanticKernel;

public sealed class PromptExecutionOptions
{
    /// <summary>
    /// Default prompt execution options.
    /// </summary>
    public DefaultPromptExecutionOptions? Default { get; set; }

    /// <summary>
    /// Prompt execution options for an AzureOpenAI completion request.
    /// </summary>
    public AzureOpenAIPromptExecutionOptions? AzureOpenAI { get; set; }

    /// <summary>
    /// Prompt execution options for  Gemini.
    /// </summary>
    public GeminiPromptExecutionOptions? Gemini { get; set; }

    /// <summary>
    /// Prompt execution options for HuggingFace.
    /// </summary>
    public HuggingFacePromptExecutionOptions? HuggingFace { get; set; }

    /// <summary>
    /// Prompt execution options for MistralAI.
    /// </summary>
    public MistralAIPromptExecutionOptions? MistralAI { get; set; }

    /// <summary>
    /// Prompt execution options for Ollama.
    /// </summary>
    public OllamaPromptExecutionOptions? Ollama { get; set; }

    /// <summary>
    /// Prompt execution options for an OpenAI completion request.
    /// </summary>
    public OpenAIPromptExecutionOptions? OpenAI { get; set; }

    /// <summary>
    /// Prompt execution options for OpenAI audio-to-text request.
    /// </summary>
    public OpenAIAudioToTextExecutionOptions? OpenAIAudioToText { get; set; }

    /// <summary>
    /// Prompt execution options for OpenAI text-to-audio request.
    /// </summary>
    public OpenAITextToAudioExecutionOptions? OpenAITextToAudio { get; set; }
}
