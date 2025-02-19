using AIToolbox.Options.SemanticKernel;
using Microsoft.SemanticKernel;

namespace AIToolbox.SemanticKernel;

internal sealed class KernelProvider : IKernelProvider
{
    private readonly KernelOptions _options;

    public KernelProvider(KernelOptions options)
    {
        ArgumentNullException.ThrowIfNull(options, nameof(options));

        _options = options;
    }

    /// <inheritdoc />
    public Kernel GetKernel()
    {
        var builder = Kernel.CreateBuilder();
        var kernel = builder.Build();

        ImportPlugins(kernel);

        return kernel;
    }

    private void ImportPlugins(Kernel kernel)
    {
        var options = _options.Plugins;

        if (options is null)
        {
            return;
        }

        kernel.ImportPluginsFromOptions(options);
    }
}
