using AIToolbox.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AIToolbox.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IAIToolboxServiceBuilder AddAIToolbox(this IServiceCollection services) =>
        services.AddAIToolbox(new AIToolboxOptions());

    public static IAIToolboxServiceBuilder AddAIToolbox(
        this IServiceCollection services,
        AIToolboxOptions options)
    {
        ArgumentNullException.ThrowIfNull(options, nameof(options));
        return new AIToolboxServiceBuilder(new ServiceBuilderService(options, services));
    }

    public static IAIToolboxServiceBuilder AddAIToolbox(
        this IServiceCollection services,
        Action<AIToolboxOptions> optionsAction)
    {
        ArgumentNullException.ThrowIfNull(optionsAction, nameof(optionsAction));

        var options = new AIToolboxOptions();
        optionsAction.Invoke(options);

        return services.AddAIToolbox(options);
    }

    public static IAIToolboxServiceBuilder AddAIToolbox(
        this IServiceCollection services,
        IConfiguration configuration,
        string sectionName = "AIToolbox") =>
        services.AddAIToolbox(options => configuration.GetRequiredSection(sectionName).Bind(options));
}
