using AIToolbox.DependencyInjection;
using AIToolbox.Options.SemanticKernel;
using AIToolbox.SemanticKernel;

namespace AIToolbox.Data;

public class SemanticKernelBuilderTestData
{
    public static TheoryData<Action<IAIToolboxBuilder, SemanticKernelOptions>> ConfigureByOptionsAction =>
    [
        (aiToolbox, opt) =>
        {
            aiToolbox.AddSemanticKernel(
                semanticKernel => semanticKernel.AddKernel(BuilderHelper.RegisterKernelMethods),
                options => options.Kernel = opt.Kernel);
        },
        (aiToolbox, opt) =>
        {
            aiToolbox.AddSemanticKernel(semanticKernel =>
                semanticKernel.AddKernel(
                    BuilderHelper.RegisterKernelMethods,
                    options => options.Connectors = opt.Kernel!.Connectors));
        },
        (aiToolbox, opt) =>
        {
            aiToolbox.AddSemanticKernel(
                semanticKernel =>
                {
                    semanticKernel.AddKernel(
                        kernel =>
                        {
                            var connectors = opt.Kernel!.Connectors!;
                            var azureOpenAI = connectors.AzureOpenAI!;
                            var google = connectors.Google!;
                            var huggingFace = connectors.HuggingFace!;
                            var mistralAI = connectors.MistralAI!;
                            var ollama = connectors.Ollama!;
                            var openAI = connectors.OpenAI!;
                            var vertexAI = connectors.VertexAI!;

                            kernel
                                .WithAzureOpenAIConnector(options =>
                                {
                                    options.AudioToText = azureOpenAI.AudioToText;
                                    options.ChatCompletion = azureOpenAI.ChatCompletion;
                                    options.Files = azureOpenAI.Files;
                                    options.TextEmbeddingGeneration = azureOpenAI.TextEmbeddingGeneration;
                                    options.TextGeneration = azureOpenAI.TextGeneration;
                                    options.TextToAudio = azureOpenAI.TextToAudio;
                                    options.TextToImage = azureOpenAI.TextToImage;
                                })
                                .WithGoogleConnector(options =>
                                {
                                    options.ChatCompletion = google.ChatCompletion;
                                    options.EmbeddingGeneration = google.EmbeddingGeneration;
                                })
                                .WithHuggingFaceConnector(options =>
                                {
                                    options.ChatCompletion = huggingFace.ChatCompletion;
                                    options.ImageToText = huggingFace.ImageToText;
                                    options.TextEmbeddingGeneration = huggingFace.TextEmbeddingGeneration;
                                    options.TextGeneration = huggingFace.TextGeneration;
                                })
                                .WithMistralAIConnector(options =>
                                {
                                    options.ChatCompletion = mistralAI.ChatCompletion;
                                    options.TextEmbeddingGeneration = mistralAI.TextEmbeddingGeneration;
                                })
                                .WithOllamaConnector(options =>
                                {
                                    options.ChatCompletion = ollama.ChatCompletion;
                                    options.TextEmbeddingGeneration = ollama.TextEmbeddingGeneration;
                                    options.TextGeneration = ollama.TextGeneration;
                                })
                                .WithOpenAIConnector(options =>
                                {
                                    options.AudioToText = openAI.AudioToText;
                                    options.ChatCompletion = openAI.ChatCompletion;
                                    options.Files = openAI.Files;
                                    options.TextEmbeddingGeneration = openAI.TextEmbeddingGeneration;
                                    options.TextToAudio = openAI.TextToAudio;
                                    options.TextToImage = openAI.TextToImage;
                                })
                                .WithVertexAIConnector(options =>
                                {
                                    options.ChatCompletion = vertexAI.ChatCompletion;
                                    options.EmbeddingGeneration = vertexAI.EmbeddingGeneration;
                                });
                        });
                });
        }
    ];
}
