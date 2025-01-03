using AIToolbox.Options.SemanticKernel;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Connectors.MistralAI;
using MistralAIToolCallBehavior = AIToolbox.Options.SemanticKernel.MistralAIToolCallBehavior;

namespace AIToolbox.SemanticKernel;

public sealed class MistralAIPromptExecutionSettingsMapper : IPromptExecutionSettingsMapper
{
    public PromptExecutionSettings? Map(PromptExecutionOptions options)
    {
        ArgumentNullException.ThrowIfNull(options, nameof(options));

        if (options.MistralAI is null)
        {
            return null;
        }

        var opt = options.MistralAI;

        return new MistralAIPromptExecutionSettings
        {
            ApiVersion = opt.ApiVersion,
            MaxTokens = opt.MaxTokens,
            ModelId = opt.ModelId,
            RandomSeed = opt.RandomSeed,
            SafePrompt = opt.SafePrompt,
            ServiceId = opt.ServiceId,
            Temperature = opt.Temperature,
            ToolCallBehavior = GetToolCallBehavior(opt.ToolCallBehavior),
            TopP = opt.TopP
        };
    }

    private static Microsoft.SemanticKernel.Connectors.MistralAI.MistralAIToolCallBehavior? GetToolCallBehavior(MistralAIToolCallBehavior? toolCallBehavior) =>
        toolCallBehavior switch
        {
            MistralAIToolCallBehavior.AutoInvokeKernelFunctions => Microsoft.SemanticKernel.Connectors.MistralAI.MistralAIToolCallBehavior.AutoInvokeKernelFunctions,
            MistralAIToolCallBehavior.EnableKernelFunctions => Microsoft.SemanticKernel.Connectors.MistralAI.MistralAIToolCallBehavior.EnableKernelFunctions,
            MistralAIToolCallBehavior.NoKernelFunctions => Microsoft.SemanticKernel.Connectors.MistralAI.MistralAIToolCallBehavior.NoKernelFunctions,
            null => null,
            _ => throw new ArgumentOutOfRangeException($"Invalid tool call behavior '{toolCallBehavior}'")
        };
}
