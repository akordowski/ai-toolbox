using AIToolbox.Options.SemanticKernel;
using AIToolbox.SemanticKernel;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.SemanticKernel;

namespace AIToolbox.DependencyInjection;

internal sealed class KernelBuilder : IKernelBuilder
{
    /// <inheritdoc />
    public KernelOptions Options { get; }

    /// <inheritdoc />
    public IServiceCollection Services { get; }

    public KernelBuilder(
        KernelOptions options,
        IServiceCollection services)
    {
        Verify.ThrowIfNull(options, nameof(options), $"No '{nameof(KernelOptions)}' provided.");
        Verify.ThrowIfNull(services, nameof(services));

        Options = options;
        Services = services;

        Services
            .AddSingleton(options)
            .AddSingleton<IKernelProvider, KernelProvider>();
    }

    public IKernelBuilder WithCustomAIServiceSelector(Func<IServiceProvider, IAIServiceSelector> factory)
    {
        Verify.ThrowIfNull(factory, nameof(factory));

        Services.AddSingleton<IKernelBuilderConfigurator>(new KernelBuilderConfigurator<IAIServiceSelector>(factory));
        return this;
    }

    public IKernelBuilder WithCustomAIServiceSelector(IAIServiceSelector instance)
    {
        Verify.ThrowIfNull(instance, nameof(instance));

        Services.AddSingleton<IKernelBuilderConfigurator>(new KernelBuilderConfigurator<IAIServiceSelector>(instance));
        return this;
    }

    public IKernelBuilder WithCustomFunctionInvocationFilter(Func<IServiceProvider, IFunctionInvocationFilter> factory)
    {
        Verify.ThrowIfNull(factory, nameof(factory));

        Services.AddSingleton<IKernelBuilderConfigurator>(new KernelBuilderConfigurator<IFunctionInvocationFilter>(factory));
        return this;
    }

    public IKernelBuilder WithCustomFunctionInvocationFilter(IFunctionInvocationFilter instance)
    {
        Verify.ThrowIfNull(instance, nameof(instance));

        Services.AddSingleton<IKernelBuilderConfigurator>(new KernelBuilderConfigurator<IFunctionInvocationFilter>(instance));
        return this;
    }
}
