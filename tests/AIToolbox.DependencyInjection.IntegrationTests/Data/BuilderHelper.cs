using AIToolbox.DependencyInjection;
using AIToolbox.SemanticKernel;

namespace AIToolbox.Data;

public static class BuilderHelper
{
    public static void RegisterKernelMethods(IKernelBuilder builder)
    {
        builder
            .WithAzureOpenAIConnector()
            .WithGoogleConnector()
            .WithHuggingFaceConnector()
            .WithMistralAIConnector()
            .WithOllamaConnector()
            .WithOpenAIConnector()
            .WithVertexAIConnector();
    }
}
