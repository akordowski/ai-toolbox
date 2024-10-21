using AIToolbox.Options.KernelMemory;
using Microsoft.KernelMemory;

namespace AIToolbox.KernelMemory;

public class KernelMemoryProvider : IKernelMemoryProvider
{
    private readonly KernelMemoryOptions _options;
    private readonly IEnumerable<IKernelMemoryBuilderConfigurator> _configurators;

    public KernelMemoryProvider(
        KernelMemoryOptions options,
        IEnumerable<IKernelMemoryBuilderConfigurator> configurators)
    {
        ArgumentNullException.ThrowIfNull(options, nameof(options));
        ArgumentNullException.ThrowIfNull(configurators, nameof(configurators));

        _options = options;
        _configurators = configurators;
    }

    public IKernelMemory GetKernelMemory()
    {
        var builder = new KernelMemoryBuilder();

        foreach (var configurator in _configurators)
        {
            configurator.Configure(builder);
        }

        return builder.Build();
    }
}
