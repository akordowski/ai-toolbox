using AIToolbox.Options.SemanticKernel;
using Microsoft.Extensions.Logging;
using Microsoft.SemanticKernel.Connectors.AzureAISearch;
using Microsoft.SemanticKernel.Memory;

namespace AIToolbox.SemanticKernel.Memory;

public sealed class AzureAISearchMemoryStoreFactory : IMemoryStoreFactory
{
    public IMemoryStore GetMemoryStore(
        MemoryStoreOptions options,
        ILoggerFactory? loggerFactory = null,
        HttpClient? httpClient = null)
    {
        ArgumentNullException.ThrowIfNull(options, nameof(options));

        var opt = options.AzureAISearch;

        Verify.ThrowIfNull(opt, nameof(MemoryStoreOptions.AzureAISearch),
            $"No '{nameof(AzureAISearchMemoryStoreOptions)}' provided.");

        return new AzureAISearchMemoryStore(opt.Endpoint, opt.ApiKey);
    }
}
