using AIToolbox.Options.SemanticKernel;
using Microsoft.SemanticKernel;

namespace AIToolbox.SemanticKernel;

public static class KernelExtensions
{
    public static void ImportPluginsFromOptions(this Kernel kernel, PluginOptions options)
    {
        ArgumentNullException.ThrowIfNull(options, nameof(options));

        const string exceptionMessage = "The value of {0} cannot be null, empty or whitespace.";

        if (options.AssemblyPlugins.Any())
        {
            foreach (var plugin in options.AssemblyPlugins)
            {
                if (string.IsNullOrWhiteSpace(plugin.AssemblyQualifiedName))
                {
                    throw new InvalidOperationException(string.Format(exceptionMessage, nameof(plugin.AssemblyQualifiedName)));
                }

                var type = Type.GetType(plugin.AssemblyQualifiedName) ??
                           throw new InvalidOperationException($"Could not find the type '{plugin.AssemblyQualifiedName}'.");

                var target = Activator.CreateInstance(type) ??
                             throw new InvalidOperationException($"Could not create an instance of type '{plugin.AssemblyQualifiedName}'.");

                kernel.ImportPluginFromObject(target, plugin.PluginName);
            }
        }

        if (options.DirectoryPlugins.Any())
        {
            foreach (var plugin in options.DirectoryPlugins)
            {
                if (string.IsNullOrWhiteSpace(plugin.PluginDirectory))
                {
                    throw new InvalidOperationException(string.Format(exceptionMessage, nameof(plugin.PluginDirectory)));
                }

                kernel.ImportPluginFromPromptDirectory(plugin.PluginDirectory, plugin.PluginName);
            }
        }

        if (options.TypePlugins.Any())
        {
            foreach (var plugin in options.TypePlugins)
            {
                var target = Activator.CreateInstance(plugin.PluginType) ??
                             throw new InvalidOperationException($"Could not create an instance of type '{plugin.PluginType.FullName}'.");

                kernel.ImportPluginFromObject(target, plugin.PluginName);
            }
        }
    }
}
