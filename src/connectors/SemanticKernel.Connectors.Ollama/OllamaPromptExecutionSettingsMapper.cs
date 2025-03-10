using AIToolbox.Options.SemanticKernel;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Connectors.Ollama;

namespace AIToolbox.SemanticKernel;

public sealed class OllamaPromptExecutionSettingsMapper : IPromptExecutionSettingsMapper
{
    public PromptExecutionSettings Map(PromptExecutionOptions options)
    {
        ArgumentNullException.ThrowIfNull(options, nameof(options));
        Verify.ThrowIfNotType<OllamaPromptExecutionOptions>(options);

        var opt = (OllamaPromptExecutionOptions)options;

        return new OllamaPromptExecutionSettings
        {
            ModelId = opt.ModelId,
            ServiceId = opt.ServiceId,
            Stop = opt.Stop,
            Temperature = opt.Temperature,
            TopK = opt.TopK,
            TopP = opt.TopP
        };
    }
}
