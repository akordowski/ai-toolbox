using Microsoft.Extensions.Logging;
using Microsoft.SemanticKernel.Memory;

namespace AIToolbox.SemanticKernel.Memory;

public interface IMemoryStoreFactory
{
    /// <summary>
    /// Gets a <see cref="IMemoryStore"/>.
    /// </summary>
    /// <returns>An instance of <see cref="IMemoryStore"/>.</returns>
    IMemoryStore GetMemoryStore(
        ILoggerFactory? loggerFactory = null,
        HttpClient? httpClient = null);
}
