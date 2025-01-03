using AIToolbox.Options.SemanticKernel;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Connectors.OpenAI;

namespace AIToolbox.SemanticKernel;

public sealed class AzureOpenAIAudioToTextExecutionSettingsMapper : IPromptExecutionSettingsMapper
{
    public PromptExecutionSettings? Map(PromptExecutionOptions options)
    {
        ArgumentNullException.ThrowIfNull(options, nameof(options));

        if (options.OpenAIAudioToText is null)
        {
            return null;
        }

        var opt = options.OpenAIAudioToText;

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
