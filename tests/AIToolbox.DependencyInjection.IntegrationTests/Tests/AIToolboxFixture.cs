using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Xunit.DependencyInjection;

namespace AIToolbox.Tests;

public class AIToolboxFixture : BaseDisposable
{
    public IHost GetHost(
        Action<HostBuilderContext, IServiceCollection> configureServices,
        string? configFileName = null) =>
        Host.CreateDefaultBuilder()
            .ConfigureAppConfiguration(config =>
            {
                if (!string.IsNullOrWhiteSpace(configFileName))
                {
                    config.AddJsonFile("Config/" + configFileName, false);
                }
            })
            .ConfigureServices(configureServices)
            .Build();
}
