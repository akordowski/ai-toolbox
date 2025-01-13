using Microsoft.Extensions.DependencyInjection;

namespace AIToolbox.DependencyInjection;

public interface IServiceBuilder<out TOptions>
{
    TOptions Options { get; }
    IServiceCollection Services { get; }
}
