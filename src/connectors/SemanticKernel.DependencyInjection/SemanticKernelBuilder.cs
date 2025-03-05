using AIToolbox.Options.SemanticKernel;
using Microsoft.Extensions.DependencyInjection;

namespace AIToolbox.DependencyInjection;

internal sealed class SemanticKernelBuilder : ISemanticKernelBuilder
{
    private readonly SemanticKernelOptions _options;
    private readonly IServiceCollection _services;

    public SemanticKernelBuilder(SemanticKernelOptions options, IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(options, nameof(options));
        ArgumentNullException.ThrowIfNull(services, nameof(services));

        _options = options;
        _services = services;
    }

    /// <inheritdoc />
    /// <exception cref="ArgumentNullException">Any of the arguments is <see langword="null"/>.</exception>
    public IAddKernel AddKernel(Action<IKernelBuilder> builderAction)
    {
        ArgumentNullException.ThrowIfNull(builderAction, nameof(builderAction));

        return AddKernel(builderAction, _ => { });
    }

    /// <inheritdoc />
    /// <exception cref="ArgumentNullException">Any of the arguments is <see langword="null"/>.</exception>
    public IAddKernel AddKernel(Action<IKernelBuilder> builderAction, Action<KernelOptions> optionsAction)
    {
        ArgumentNullException.ThrowIfNull(builderAction, nameof(builderAction));
        ArgumentNullException.ThrowIfNull(optionsAction, nameof(optionsAction));

        _options.Kernel ??= new KernelOptions();

        var options = _options.Kernel;
        optionsAction.Invoke(options);

        var builder = new KernelBuilder(options, _services);
        builderAction.Invoke(builder);

        return this;
    }

    /// <inheritdoc />
    /// <exception cref="ArgumentNullException">Any of the arguments is <see langword="null"/>.</exception>
    public IAddMemory AddMemory(Action<IMemoryBuilder> builderAction)
    {
        ArgumentNullException.ThrowIfNull(builderAction, nameof(builderAction));

        return AddMemory(builderAction, _ => { });
    }

    /// <inheritdoc />
    /// <exception cref="ArgumentNullException">Any of the arguments is <see langword="null"/>.</exception>
    public IAddMemory AddMemory(Action<IMemoryBuilder> builderAction, Action<MemoryOptions> optionsAction)
    {
        ArgumentNullException.ThrowIfNull(builderAction, nameof(builderAction));
        ArgumentNullException.ThrowIfNull(optionsAction, nameof(optionsAction));

        _options.Memory ??= new MemoryOptions();

        var options = _options.Memory;
        optionsAction.Invoke(options);

        var builder = new MemoryBuilder(options, _services);
        builderAction.Invoke(builder);

        return this;
    }
}
