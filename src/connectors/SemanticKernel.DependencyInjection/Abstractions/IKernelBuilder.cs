using AIToolbox.Options;
using AIToolbox.Options.SemanticKernel;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.SemanticKernel;

namespace AIToolbox.DependencyInjection;

public interface IKernelBuilder
{
    /// <summary>
    /// Gets the <see cref="AIToolboxOptions"/>.
    /// </summary>
    KernelOptions Options { get; }

    /// <summary>
    /// Gets the <see cref="IServiceCollection"/> where services are configured.
    /// </summary>
    IServiceCollection Services { get; }

    IKernelBuilder WithCustomAIServiceSelector(Func<IServiceProvider, IAIServiceSelector> factory);
    IKernelBuilder WithCustomAIServiceSelector(IAIServiceSelector instance);
    IKernelBuilder WithCustomFunctionInvocationFilter(Func<IServiceProvider, IFunctionInvocationFilter> factory);
    IKernelBuilder WithCustomFunctionInvocationFilter(IFunctionInvocationFilter instance);
}
