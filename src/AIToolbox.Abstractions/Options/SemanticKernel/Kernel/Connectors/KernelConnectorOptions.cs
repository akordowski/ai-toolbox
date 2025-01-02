namespace AIToolbox.Options.SemanticKernel;

public class KernelConnectorOptions
{
    public AzureOpenAIOptions? AzureOpenAI { get; set; }
    public GoogleAIOptions? GoogleAI { get; set; }
    public HuggingFaceOptions? HuggingFace { get; set; }
    public MistralOptions? Mistral { get; set; }
    public OllamaOptions? Ollama { get; set; }
    public OpenAIOptions? OpenAI { get; set; }
    public VertexAIOptions? VertexAI { get; set; }
}
