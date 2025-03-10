using AIToolbox.Options.SemanticKernel;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Connectors.OpenAI;

namespace AIToolbox.SemanticKernel;

public sealed class OpenAIAudioToTextExecutionSettingsMapper : IPromptExecutionSettingsMapper
{
    public PromptExecutionSettings Map(PromptExecutionOptions options)
    {
        ArgumentNullException.ThrowIfNull(options, nameof(options));
        Verify.ThrowIfNotType<OpenAIAudioToTextExecutionOptions>(options);

        var opt = (OpenAIAudioToTextExecutionOptions)options;

        return new OpenAIAudioToTextExecutionSettings
        {
            Filename = opt.Filename,
            Language = opt.Language,
            ModelId = opt.ModelId,
            Prompt = opt.Prompt,
            ResponseFormat = opt.ResponseFormat,
            ServiceId = opt.ServiceId,
            Temperature = opt.Temperature
        };
    }
}
