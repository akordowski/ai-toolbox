namespace AIToolbox.IO;

public sealed class FileSystemOptions
{
    public FileSystemType Type { get; set; } = FileSystemType.Volatile;
    public string Directory { get; set; } = string.Empty;
}
