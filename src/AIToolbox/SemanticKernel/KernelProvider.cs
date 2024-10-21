using AIToolbox.Options.SemanticKernel;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.SemanticKernel;

namespace AIToolbox.SemanticKernel;

public sealed class KernelProvider : IKernelProvider
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

    public Kernel GetKernel()
    {
        var builder = Kernel.CreateBuilder();

        ConfigureLogging(builder);

        foreach (var configurator in _configurators)
        {
            configurator.Configure(builder);
        }

        var kernel = builder.Build();

        ImportPlugins(kernel);

        return kernel;
    }

    private void ConfigureLogging(IKernelBuilder builder)
    {
        if (_options.AddLogging)
        {
            builder.Services.AddLogging();
        }
    }

    private void ImportPlugins(Kernel kernel)
    {
        if (_options.Plugins is null)
        {
            return;
        }

        kernel.ImportPluginsFromOptions(_options.Plugins);
    }
}
