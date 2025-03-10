using AIToolbox.Options.SemanticKernel;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Connectors.MistralAI;
using MistralAIToolCallBehavior = AIToolbox.Options.SemanticKernel.MistralAIToolCallBehavior;
using SKMistralAIToolCallBehavior = Microsoft.SemanticKernel.Connectors.MistralAI.MistralAIToolCallBehavior;

namespace AIToolbox.SemanticKernel;

public sealed class MistralAIPromptExecutionSettingsMapper : IPromptExecutionSettingsMapper
{
    public PromptExecutionSettings Map(PromptExecutionOptions options)
    {
        ArgumentNullException.ThrowIfNull(options, nameof(options));
        Verify.ThrowIfNotType<MistralAIPromptExecutionOptions>(options);

        var opt = (MistralAIPromptExecutionOptions)options;

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

    private static SKMistralAIToolCallBehavior? GetToolCallBehavior(MistralAIToolCallBehavior? toolCallBehavior) =>
        toolCallBehavior switch
        {
            MistralAIToolCallBehavior.AutoInvokeKernelFunctions => SKMistralAIToolCallBehavior.AutoInvokeKernelFunctions,
            MistralAIToolCallBehavior.EnableKernelFunctions => SKMistralAIToolCallBehavior.EnableKernelFunctions,
            MistralAIToolCallBehavior.NoKernelFunctions => SKMistralAIToolCallBehavior.NoKernelFunctions,
            null => null,
            _ => throw new ArgumentOutOfRangeException($"Invalid tool call behavior '{toolCallBehavior}'")
        };
}
