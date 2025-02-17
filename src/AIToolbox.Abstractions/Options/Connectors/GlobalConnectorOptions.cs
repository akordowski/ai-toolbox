namespace AIToolbox.Options.Connectors;

public sealed class GlobalConnectorOptions
{
    public GlobalAzureOpenAIOptions? AzureOpenAI { get; set; }
    public GlobalGoogleOptions? Google { get; set; }
    public GlobalHuggingFaceOptions? HuggingFace { get; set; }
    public GlobalMistralAIOptions? MistralAI { get; set; }
    public GlobalOllamaOptions? Ollama { get; set; }
    public GlobalOpenAIOptions? OpenAI { get; set; }
}
