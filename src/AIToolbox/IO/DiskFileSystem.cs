using Microsoft.Extensions.Logging;

namespace AIToolbox.IO;

public class DiskFileSystem : FileSystem
{
    private readonly string _directory;

    public DiskFileSystem(
        string directory,
        ILoggerFactory? loggerFactory = null)
        : base(loggerFactory?.CreateLogger<DiskFileSystem>())
    {
        _directory = directory;
    }

    protected override void CreateDirectory(string path)
    {
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
    }

    protected override void DeleteDirectory(string path) => Directory.Delete(path, true);

    protected override void DeleteFile(string path) => File.Delete(path);

    protected override bool DirectoryExists(string path) => Directory.Exists(path);

    protected override bool FileExists(string path) => File.Exists(path);

    protected override string[] GetFiles(string path) => Directory.GetFiles(path);

    protected override IEnumerable<string> GetVolumes() =>
        DirectoryExists(_directory)
            ? Directory.GetDirectories(_directory).Select(GetFileName)
            : [];

    protected override string GetFileName(string path) => Path.GetFileName(path);

    protected override async Task<BinaryData> ReadFileAsync(string path, CancellationToken cancellationToken)
    {
        var bytes = await File.ReadAllBytesAsync(path, cancellationToken).ConfigureAwait(false);
        return BinaryData.FromBytes(bytes);
    }

    protected override Task WriteFileAsync(string path, byte[] bytes, CancellationToken cancellationToken) =>
        File.WriteAllBytesAsync(path, bytes, cancellationToken);

    protected override string CombinePaths(params string[] paths)
    {
        paths = new List<string>(paths).Prepend(_directory).ToArray();
        return Path.Combine(paths);
    }
}
