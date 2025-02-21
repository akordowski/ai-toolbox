namespace AIToolbox.Options.SemanticKernel;

public class ConnectorOptions
{
    public AzureOpenAIOptions? AzureOpenAI { get; set; }
    public GoogleOptions? Google { get; set; }
    public HuggingFaceOptions? HuggingFace { get; set; }
    public MistralAIOptions? MistralAI { get; set; }
    public OllamaOptions? Ollama { get; set; }
    public OpenAIOptions? OpenAI { get; set; }
    public VertexAIOptions? VertexAI { get; set; }
}
