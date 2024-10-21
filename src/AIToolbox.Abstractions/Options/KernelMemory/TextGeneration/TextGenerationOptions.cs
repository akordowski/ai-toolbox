namespace AIToolbox.Options.KernelMemory;

public sealed class TextGenerationOptions
{
    public AnthropicOptions? Anthropic { get; set; }
    public AzureOpenAIOptions? AzureOpenAI { get; set; }
    public LlamaOptions? Llama { get; set; }
    public OpenAIOptions? OpenAI { get; set; }
    public SemanticKernelOptions? SemanticKernel { get; set; }
}
