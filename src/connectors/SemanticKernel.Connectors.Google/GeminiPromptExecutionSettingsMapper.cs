using AIToolbox.Options.SemanticKernel;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Connectors.Google;
using GeminiSafetyCategory = AIToolbox.Options.SemanticKernel.GeminiSafetyCategory;
using GeminiSafetyThreshold = AIToolbox.Options.SemanticKernel.GeminiSafetyThreshold;
using GeminiToolCallBehavior = AIToolbox.Options.SemanticKernel.GeminiToolCallBehavior;

namespace AIToolbox.SemanticKernel;

public sealed class GeminiPromptExecutionSettingsMapper : IPromptExecutionSettingsMapper
{
    public PromptExecutionSettings? Map(PromptExecutionOptions options)
    {
        ArgumentNullException.ThrowIfNull(options, nameof(options));

        if (options.Gemini is null)
        {
            return null;
        }

        var opt = options.Gemini;

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

    private static Microsoft.SemanticKernel.Connectors.Google.GeminiToolCallBehavior? GetToolCallBehavior(GeminiToolCallBehavior? toolCallBehavior) =>
        toolCallBehavior switch
        {
            GeminiToolCallBehavior.AutoInvokeKernelFunctions => Microsoft.SemanticKernel.Connectors.Google.GeminiToolCallBehavior.AutoInvokeKernelFunctions,
            GeminiToolCallBehavior.EnableKernelFunctions => Microsoft.SemanticKernel.Connectors.Google.GeminiToolCallBehavior.EnableKernelFunctions,
            null => null,
            _ => throw new ArgumentOutOfRangeException($"Invalid tool call behavior '{toolCallBehavior}'")
        };

    private static Microsoft.SemanticKernel.Connectors.Google.GeminiSafetyCategory GetSafetyCategory(GeminiSafetyCategory category) =>
        category switch
        {
            GeminiSafetyCategory.Dangerous => Microsoft.SemanticKernel.Connectors.Google.GeminiSafetyCategory.Dangerous,
            GeminiSafetyCategory.DangerousContent => Microsoft.SemanticKernel.Connectors.Google.GeminiSafetyCategory.DangerousContent,
            GeminiSafetyCategory.Derogatory => Microsoft.SemanticKernel.Connectors.Google.GeminiSafetyCategory.Derogatory,
            GeminiSafetyCategory.Harassment => Microsoft.SemanticKernel.Connectors.Google.GeminiSafetyCategory.Harassment,
            GeminiSafetyCategory.Medical => Microsoft.SemanticKernel.Connectors.Google.GeminiSafetyCategory.Medical,
            GeminiSafetyCategory.Sexual => Microsoft.SemanticKernel.Connectors.Google.GeminiSafetyCategory.Sexual,
            GeminiSafetyCategory.SexuallyExplicit => Microsoft.SemanticKernel.Connectors.Google.GeminiSafetyCategory.SexuallyExplicit,
            GeminiSafetyCategory.Toxicity => Microsoft.SemanticKernel.Connectors.Google.GeminiSafetyCategory.Toxicity,
            GeminiSafetyCategory.Unspecified => Microsoft.SemanticKernel.Connectors.Google.GeminiSafetyCategory.Unspecified,
            GeminiSafetyCategory.Violence => Microsoft.SemanticKernel.Connectors.Google.GeminiSafetyCategory.Violence,
            _ => throw new ArgumentOutOfRangeException($"Invalid safety category '{category}'")
        };

    private static Microsoft.SemanticKernel.Connectors.Google.GeminiSafetyThreshold GetSafetyThreshold(GeminiSafetyThreshold threshold) =>
        threshold switch
        {
            GeminiSafetyThreshold.BlockLowAndAbove => Microsoft.SemanticKernel.Connectors.Google.GeminiSafetyThreshold.BlockLowAndAbove,
            GeminiSafetyThreshold.BlockMediumAndAbove => Microsoft.SemanticKernel.Connectors.Google.GeminiSafetyThreshold.BlockMediumAndAbove,
            GeminiSafetyThreshold.BlockNone => Microsoft.SemanticKernel.Connectors.Google.GeminiSafetyThreshold.BlockNone,
            GeminiSafetyThreshold.BlockOnlyHigh => Microsoft.SemanticKernel.Connectors.Google.GeminiSafetyThreshold.BlockOnlyHigh,
            GeminiSafetyThreshold.Unspecified => Microsoft.SemanticKernel.Connectors.Google.GeminiSafetyThreshold.Unspecified,
            _ => throw new ArgumentOutOfRangeException($"Invalid safety threshold '{threshold}'")
        };
}
