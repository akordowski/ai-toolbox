using AIToolbox.Options.SemanticKernel;
using Microsoft.Extensions.Logging;
using Microsoft.SemanticKernel.Connectors.AzureAISearch;
using Microsoft.SemanticKernel.Memory;

namespace AIToolbox.SemanticKernel.Memory;

internal sealed class AzureAISearchMemoryStoreFactory : IMemoryStoreFactory
{
    private readonly AzureAISearchMemoryStoreOptions _options;

    public AzureAISearchMemoryStoreFactory(AzureAISearchMemoryStoreOptions options)
    {
        ArgumentNullException.ThrowIfNull(options, nameof(options));

        _options = options;
    }

    public IMemoryStore GetMemoryStore(ILoggerFactory? loggerFactory = null, HttpClient? httpClient = null) =>
        new AzureAISearchMemoryStore(_options.Endpoint, _options.ApiKey);
}
