using AIToolbox.Options.SemanticKernel;
using Microsoft.SemanticKernel;

namespace AIToolbox.SemanticKernel;

internal sealed class KernelProvider : IKernelProvider
{
    private readonly KernelOptions _options;
    private readonly IEnumerable<IKernelBuilderConfigurator> _configurators;

    public KernelProvider(
        KernelOptions options,
        IEnumerable<IKernelBuilderConfigurator> configurators)
    {
        ArgumentNullException.ThrowIfNull(options, nameof(options));
        ArgumentNullException.ThrowIfNull(configurators, nameof(configurators));

        _options = options;
        _configurators = configurators;
    }

    /// <inheritdoc />
    public Kernel GetKernel()
    {
        var builder = Kernel.CreateBuilder();

        InvokeConfigurators(builder);

        var kernel = builder.Build();

        ImportPlugins(kernel);

        return kernel;
    }

    private void InvokeConfigurators(IKernelBuilder builder)
    {
        foreach (var configurator in _configurators)
        {
            configurator.Configure(builder);
        }
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
