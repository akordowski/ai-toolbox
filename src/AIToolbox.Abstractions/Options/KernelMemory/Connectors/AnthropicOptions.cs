namespace AIToolbox.Options.KernelMemory;

public sealed class AnthropicOptions
{
    public string Endpoint { get; set; } = "https://api.anthropic.com";
    public string EndpointVersion { get; set; } = "2023-06-01";
    public string ApiKey { get; set; } = default!;
    public string TextModelName { get; set; } = "claude-3-sonnet-20240229";
    public int MaxTokenIn { get; set; } = 200_000;
    public int MaxTokenOut { get; set; } = 4096;
    public string DefaultSystemPrompt { get; set; } = "You are an assistant that will answer user query based on a context";
    public string HttpClientName { get; set; } = default!;
}
