using System.Text.RegularExpressions;

namespace AIToolbox.SemanticKernel.Memory;

internal static partial class SimpleMemoryStoreRegex
{
    [GeneratedRegex(@"[\s|\\|/|.|_|:]")]
    public static partial Regex ReplaceVolumeCharsRegex();
}
