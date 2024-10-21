using AIToolbox.Options.KernelMemory;

namespace AIToolbox.DependencyInjection;

public interface IKernelMemoryServiceBuilder : IServiceBuilder<KernelMemoryOptions>, IAddAgents, IAddMemory;
