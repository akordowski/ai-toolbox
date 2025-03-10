using AIToolbox.Options.SemanticKernel;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Connectors.Google;
using GeminiSafetyCategory = AIToolbox.Options.SemanticKernel.GeminiSafetyCategory;
using GeminiSafetyThreshold = AIToolbox.Options.SemanticKernel.GeminiSafetyThreshold;
using GeminiToolCallBehavior = AIToolbox.Options.SemanticKernel.GeminiToolCallBehavior;
using SKGeminiSafetyCategory = Microsoft.SemanticKernel.Connectors.Google.GeminiSafetyCategory;
using SKGeminiSafetyThreshold = Microsoft.SemanticKernel.Connectors.Google.GeminiSafetyThreshold;
using SKGeminiToolCallBehavior = Microsoft.SemanticKernel.Connectors.Google.GeminiToolCallBehavior;

namespace AIToolbox.SemanticKernel;

public sealed class GeminiPromptExecutionSettingsMapper : IPromptExecutionSettingsMapper
{
    public PromptExecutionSettings Map(PromptExecutionOptions options)
    {
        ArgumentNullException.ThrowIfNull(options, nameof(options));
        Verify.ThrowIfNotType<GeminiPromptExecutionOptions>(options);

        var opt = (GeminiPromptExecutionOptions)options;

        return new GeminiPromptExecutionSettings
        {
            CandidateCount = opt.CandidateCount,
            MaxTokens = opt.MaxTokens,
            ModelId = opt.ModelId,
            SafetySettings = GetSafetySettings(opt.SafetySettings),
            ServiceId = opt.ServiceId,
            StopSequences = opt.StopSequences,
            Temperature = opt.Temperature,
            ToolCallBehavior = GetToolCallBehavior(opt.ToolCallBehavior),
            TopK = opt.TopK,
            TopP = opt.TopP
        };
    }

    private static List<GeminiSafetySetting>? GetSafetySettings(IList<GeminiSafetyOptions>? safetySettings) =>
        safetySettings?
            .Select(o => new GeminiSafetySetting(
                GetSafetyCategory(o.Category),
                GetSafetyThreshold(o.Threshold)))
            .ToList();

    private static SKGeminiToolCallBehavior? GetToolCallBehavior(GeminiToolCallBehavior? toolCallBehavior) =>
        toolCallBehavior switch
        {
            GeminiToolCallBehavior.AutoInvokeKernelFunctions => SKGeminiToolCallBehavior.AutoInvokeKernelFunctions,
            GeminiToolCallBehavior.EnableKernelFunctions => SKGeminiToolCallBehavior.EnableKernelFunctions,
            null => null,
            _ => throw new ArgumentOutOfRangeException($"Invalid tool call behavior '{toolCallBehavior}'")
        };

    private static SKGeminiSafetyCategory GetSafetyCategory(GeminiSafetyCategory category) =>
        category switch
        {
            GeminiSafetyCategory.Dangerous => SKGeminiSafetyCategory.Dangerous,
            GeminiSafetyCategory.DangerousContent => SKGeminiSafetyCategory.DangerousContent,
            GeminiSafetyCategory.Derogatory => SKGeminiSafetyCategory.Derogatory,
            GeminiSafetyCategory.Harassment => SKGeminiSafetyCategory.Harassment,
            GeminiSafetyCategory.Medical => SKGeminiSafetyCategory.Medical,
            GeminiSafetyCategory.Sexual => SKGeminiSafetyCategory.Sexual,
            GeminiSafetyCategory.SexuallyExplicit => SKGeminiSafetyCategory.SexuallyExplicit,
            GeminiSafetyCategory.Toxicity => SKGeminiSafetyCategory.Toxicity,
            GeminiSafetyCategory.Unspecified => SKGeminiSafetyCategory.Unspecified,
            GeminiSafetyCategory.Violence => SKGeminiSafetyCategory.Violence,
            _ => throw new ArgumentOutOfRangeException($"Invalid safety category '{category}'")
        };

    private static SKGeminiSafetyThreshold GetSafetyThreshold(GeminiSafetyThreshold threshold) =>
        threshold switch
        {
            GeminiSafetyThreshold.BlockLowAndAbove => SKGeminiSafetyThreshold.BlockLowAndAbove,
            GeminiSafetyThreshold.BlockMediumAndAbove => SKGeminiSafetyThreshold.BlockMediumAndAbove,
            GeminiSafetyThreshold.BlockNone => SKGeminiSafetyThreshold.BlockNone,
            GeminiSafetyThreshold.BlockOnlyHigh => SKGeminiSafetyThreshold.BlockOnlyHigh,
            GeminiSafetyThreshold.Unspecified => SKGeminiSafetyThreshold.Unspecified,
            _ => throw new ArgumentOutOfRangeException($"Invalid safety threshold '{threshold}'")
        };
}
