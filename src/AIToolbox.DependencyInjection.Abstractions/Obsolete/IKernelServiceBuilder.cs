using AIToolbox.Options.SemanticKernel;
using Microsoft.SemanticKernel;

namespace AIToolbox.DependencyInjection;

public interface IKernelServiceBuilder : IServiceBuilder<KernelOptions>, IAddAgents, IAddMemory
{
    IKernelServiceBuilder WithCustomAIServiceSelector(Func<IServiceProvider, IAIServiceSelector> factory);
    IKernelServiceBuilder WithCustomAIServiceSelector(IAIServiceSelector instance);
    IKernelServiceBuilder WithCustomFunctionInvocationFilter(Func<IServiceProvider, IFunctionInvocationFilter> factory);
    IKernelServiceBuilder WithCustomFunctionInvocationFilter(IFunctionInvocationFilter instance);
}
