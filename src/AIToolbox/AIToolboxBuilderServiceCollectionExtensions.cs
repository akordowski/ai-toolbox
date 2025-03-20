using AIToolbox;
using AIToolbox.Options;
using Microsoft.Extensions.Configuration;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection;

public static class AIToolboxBuilderServiceCollectionExtensions
{
    /// <summary>
    /// Adds the AIToolbox to the <see cref="IServiceCollection"/>.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/>.</param>
    /// <param name="builderAction">A delegate that is used to configure an <see cref="AIToolboxBuilder"/>.</param>
    /// <returns>The value of <paramref name="services"/>.</returns>
    /// <exception cref="ArgumentNullException">Any of the arguments is <see langword="null"/>.</exception>
    public static IServiceCollection AddAIToolbox(
        this IServiceCollection services,
        Action<AIToolboxBuilder> builderAction)
    {
        ArgumentNullException.ThrowIfNull(services, nameof(services));
        ArgumentNullException.ThrowIfNull(builderAction, nameof(builderAction));

        return services.AddAIToolbox(builderAction, _ => { });
    }

    /// <summary>
    /// Adds the AIToolbox to the <see cref="IServiceCollection"/>.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/>.</param>
    /// <param name="builderAction">A delegate that is used to configure an <see cref="AIToolboxBuilder"/>.</param>
    /// <param name="optionsAction">A delegate that is used to configure the <see cref="AIToolboxOptions"/>.</param>
    /// <returns>The value of <paramref name="services"/>.</returns>
    /// <exception cref="ArgumentNullException">Any of the arguments is <see langword="null"/>.</exception>
    public static IServiceCollection AddAIToolbox(
        this IServiceCollection services,
        Action<AIToolboxBuilder> builderAction,
        Action<AIToolboxOptions> optionsAction)
    {
        ArgumentNullException.ThrowIfNull(services, nameof(services));
        ArgumentNullException.ThrowIfNull(builderAction, nameof(builderAction));
        ArgumentNullException.ThrowIfNull(optionsAction, nameof(optionsAction));

        var options = new AIToolboxOptions();
        optionsAction.Invoke(options);

        var builder = new AIToolboxBuilder(options, services);
        builderAction.Invoke(builder);

        return services;
    }

    /// <summary>
    /// Adds the AIToolbox to the <see cref="IServiceCollection"/>.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/>.</param>
    /// <param name="builderAction">A delegate that is used to configure an <see cref="AIToolboxBuilder"/>.</param>
    /// <param name="configuration">The configuration being bound.</param>
    /// <param name="sectionName">The name of the configuration section that contains <see cref="AIToolboxOptions"/>.</param>
    /// <returns>The value of <paramref name="services"/>.</returns>
    /// <exception cref="ArgumentNullException">Any of the arguments is <see langword="null"/>.</exception>
    /// <exception cref="ArgumentException"><paramref name="sectionName"/> is empty or whitespace.</exception>
    public static IServiceCollection AddAIToolbox(
        this IServiceCollection services,
        Action<AIToolboxBuilder> builderAction,
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
