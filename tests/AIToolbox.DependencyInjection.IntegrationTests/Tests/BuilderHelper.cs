using AIToolbox.DependencyInjection;

namespace AIToolbox.Tests;

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
