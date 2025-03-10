using AIToolbox.Options.SemanticKernel;
using Microsoft.SemanticKernel;

namespace AIToolbox.SemanticKernel;

public interface IPromptExecutionSettingsMapper
{
    PromptExecutionSettings Map(PromptExecutionOptions options);
}
