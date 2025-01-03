using AIToolbox.Options.SemanticKernel;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Connectors.HuggingFace;

namespace AIToolbox.SemanticKernel;

public sealed class HuggingFacePromptExecutionSettingsMapper : IPromptExecutionSettingsMapper
{
    public PromptExecutionSettings? Map(PromptExecutionOptions options)
    {
        ArgumentNullException.ThrowIfNull(options, nameof(options));

        if (options.HuggingFace is null)
        {
            return null;
        }

        var opt = options.HuggingFace;

        return new HuggingFacePromptExecutionSettings
        {
            Details = opt.Details,
            LogProbs = opt.LogProbs,
            MaxNewTokens = opt.MaxNewTokens,
            MaxTime = opt.MaxTime,
            MaxTokens = opt.MaxTokens,
            ModelId = opt.ModelId,
            PresencePenalty = opt.PresencePenalty,
            RepetitionPenalty = opt.RepetitionPenalty,
            ResultsPerPrompt = opt.ResultsPerPrompt,
            Seed = opt.Seed,
            ServiceId = opt.ServiceId,
            Stop = opt.Stop,
            Temperature = opt.Temperature,
            TopK = opt.TopK,
            TopLogProbs = opt.TopLogProbs,
            TopP = opt.TopP,
            UseCache = opt.UseCache,
            WaitForModel = opt.WaitForModel
        };
    }
}
