using Microsoft.Extensions.Logging;

namespace AIToolbox.IO;

public sealed class FileSystemFactory : IFileSystemFactory
{
    private readonly ILoggerFactory? _loggerFactory;

    public FileSystemFactory(ILoggerFactory? loggerFactory = null)
    {
        _loggerFactory = loggerFactory;
    }

    /// <inheritdoc />
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    public IFileSystem CreateFileSystem(FileSystemOptions options)
    {
        ArgumentNullException.ThrowIfNull(options, nameof(options));

        return options.Type switch
        {
            FileSystemType.Volatile => new VolatileFileSystem(_loggerFactory),
            FileSystemType.Disk => new DiskFileSystem(options.Directory, _loggerFactory),
            _ => throw new ArgumentOutOfRangeException($"Invalid file system type '{options.Type}'")
        };
    }
}
