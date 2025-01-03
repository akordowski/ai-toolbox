using AIToolbox.Options.SemanticKernel;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Connectors.OpenAI;

namespace AIToolbox.SemanticKernel;

public sealed class OpenAITextToAudioExecutionSettingsMapper : IPromptExecutionSettingsMapper
{
    public PromptExecutionSettings? Map(PromptExecutionOptions options)
    {
        ArgumentNullException.ThrowIfNull(options, nameof(options));

        if (options.OpenAITextToAudio is null)
        {
            return null;
        }

        var opt = options.OpenAITextToAudio;

        return new OpenAITextToAudioExecutionSettings
        {
            ModelId = opt.ModelId,
            ResponseFormat = opt.ResponseFormat,
            ServiceId = opt.ServiceId,
            Speed = opt.Speed,
            Voice = opt.Voice
        };
    }
}
