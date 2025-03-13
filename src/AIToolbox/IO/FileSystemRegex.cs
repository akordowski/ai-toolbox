using System.Text.RegularExpressions;

namespace AIToolbox.IO;

internal static partial class FileSystemRegex
{
    [GeneratedRegex(@"[\s|\||\\|/|\0|'|\`|""|:|;|,|~|!|?|*|+|=|^|@|#|$|%|&]")]
    public static partial Regex InvalidVolumeCharsRegex();

    [GeneratedRegex(@"[ \s|\||\0 |\\|:]")]
    public static partial Regex InvalidPathCharsRegex();

    [GeneratedRegex(@"[\s|""|<|>|\||\0|:|*|?|\\|\/|]")]
    public static partial Regex InvalidFileNameCharsRegex();
}
