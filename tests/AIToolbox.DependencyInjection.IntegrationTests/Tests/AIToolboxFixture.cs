using AIToolbox.Options;
using AIToolbox.TestHelper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Xunit.DependencyInjection;

namespace AIToolbox;

public class AIToolboxFixture : BaseDisposable
{
    public IHost GetHost(
        Action<HostBuilderContext, IServiceCollection> configureServices,
        bool useAppConfiguration = false) =>
        Host.CreateDefaultBuilder()
            .ConfigureAppConfiguration(config =>
            {
                if (useAppConfiguration)
                {
                    config.AddJsonStream(GetJsonMemoryStream());
                }
            })
            .ConfigureServices(configureServices)
            .Build();

    private static MemoryStream GetJsonMemoryStream()
    {
        var config = new ConfigAIToolbox { AIToolbox = TestOptions.AIToolbox };
        var options = new JsonSerializerOptions { Converters = { new JsonStringEnumConverter() } };
        var json = JsonSerializer.Serialize(config, options);

        return new MemoryStream(Encoding.UTF8.GetBytes(json));
    }

    private class ConfigAIToolbox
    {
        public AIToolboxOptions AIToolbox { get; set; } = default!;
    }
}
