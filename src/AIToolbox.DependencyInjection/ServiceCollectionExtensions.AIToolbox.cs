using AIToolbox.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AIToolbox.DependencyInjection;

public static partial class ServiceCollectionExtensions
{
    public static IServiceCollection AddAIToolbox(
        this IServiceCollection services,
        Action<IAIToolboxBuilder> builderAction)
    {
        ArgumentNullException.ThrowIfNull(services, nameof(services));
        ArgumentNullException.ThrowIfNull(builderAction, nameof(builderAction));

        return services.AddAIToolbox(builderAction, new AIToolboxOptions());
    }

    public static IServiceCollection AddAIToolbox(
        this IServiceCollection services,
        Action<IAIToolboxBuilder> builderAction,
        AIToolboxOptions options)
    {
        ArgumentNullException.ThrowIfNull(services, nameof(services));
        ArgumentNullException.ThrowIfNull(builderAction, nameof(builderAction));
        ArgumentNullException.ThrowIfNull(options, nameof(options));

        var builderFactory = new BuilderFactory(options, services);
        var builder = new AIToolboxBuilder(builderFactory);
        builderAction.Invoke(builder);

        return services;
    }

    public static IServiceCollection AddAIToolbox(
        this IServiceCollection services,
        Action<IAIToolboxBuilder> builderAction,
        Action<AIToolboxOptions> optionsAction)
    {
        ArgumentNullException.ThrowIfNull(services, nameof(services));
        ArgumentNullException.ThrowIfNull(builderAction, nameof(builderAction));
        ArgumentNullException.ThrowIfNull(optionsAction, nameof(optionsAction));

        var options = new AIToolboxOptions();
        optionsAction.Invoke(options);

        return services.AddAIToolbox(builderAction, options);
    }

    public static IServiceCollection AddAIToolbox(
        this IServiceCollection services,
        Action<IAIToolboxBuilder> builderAction,
        IConfiguration configuration,
        string sectionName = "AIToolbox")
    {
        ArgumentNullException.ThrowIfNull(services, nameof(services));
        ArgumentNullException.ThrowIfNull(builderAction, nameof(builderAction));
        ArgumentNullException.ThrowIfNull(configuration, nameof(configuration));
        ArgumentException.ThrowIfNullOrWhiteSpace(sectionName, nameof(sectionName));

        return services.AddAIToolbox(builderAction, options => configuration.GetRequiredSection(sectionName).Bind(options));
    }
}
