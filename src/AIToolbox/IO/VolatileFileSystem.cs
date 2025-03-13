using Microsoft.Extensions.Logging;
using System.Collections.Concurrent;

namespace AIToolbox.IO;

public class VolatileFileSystem : FileSystem
{
    private const char PathSeparator = '/';
    private readonly ConcurrentDictionary<string, ConcurrentDictionary<string, BinaryData>> _volumes = new();

    public VolatileFileSystem(ILoggerFactory? loggerFactory = null)
        : base(loggerFactory?.CreateLogger<VolatileFileSystem>())
    {
    }

    protected override void CreateDirectory(string path)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(path, nameof(path));

        var volumeKey = GetVolumeKey(path);

        if (!_volumes.ContainsKey(volumeKey))
        {
            _volumes.TryAdd(volumeKey, new ConcurrentDictionary<string, BinaryData>());
        }
    }

    protected override void DeleteDirectory(string path)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(path, nameof(path));

        var volumeKey = GetVolumeKey(path);
        _volumes.TryRemove(volumeKey, out _);
    }

    protected override void DeleteFile(string path)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(path, nameof(path));

        var (volumeKey, fileKey) = GetKeys(path);
        _volumes[volumeKey].TryRemove(fileKey, out _);
    }

    protected override bool DirectoryExists(string path)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(path, nameof(path));

        var volumeKey = GetVolumeKey(path);
        return _volumes.ContainsKey(volumeKey);
    }

    protected override bool FileExists(string path)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(path, nameof(path));

        var (volumeKey, fileKey) = GetKeys(path);
        return _volumes.TryGetValue(volumeKey, out var volume) && volume.ContainsKey(fileKey);
    }

    protected override string[] GetFiles(string path)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(path, nameof(path));

        var volumeKey = GetVolumeKey(path);
        return _volumes[volumeKey].Keys.Select(key => CombinePaths(volumeKey, key)).ToArray();
    }

    protected override IEnumerable<string> GetVolumes() => _volumes.Keys;

    protected override string GetFileName(string path)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(path, nameof(path));
        return path.Split(PathSeparator).Last();
    }

    protected override Task<BinaryData> ReadFileAsync(string path, CancellationToken cancellationToken)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(path, nameof(path));

        var (volumeKey, fileKey) = GetKeys(path);
        var data = _volumes[volumeKey][fileKey];

        return Task.FromResult(data);
    }

    protected override Task WriteFileAsync(string path, byte[] bytes, CancellationToken cancellationToken)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(path, nameof(path));

        var (volumeKey, fileKey) = GetKeys(path);
        _volumes[volumeKey][fileKey] = BinaryData.FromBytes(bytes);

        return Task.CompletedTask;
    }

    protected override string CombinePaths(params string[] paths)
    {
        var pathValues = paths
            .Where(p => !string.IsNullOrWhiteSpace(p))
            .Select(p => p.Trim('/').Trim('\\'));

        return string.Join(PathSeparator, pathValues);
    }

    private static string GetVolumeKey(string path) => path.Split(PathSeparator).First();

    private (string VolumeKey, string FileKey) GetKeys(string path)
    {
        var pathSegments = path.Split(PathSeparator);
        var pathSegmentsWithoutVolume = pathSegments.ToList();
        pathSegmentsWithoutVolume.RemoveAt(0);

        var volumeKey = pathSegments.First();
        var fileKey = CombinePaths([.. pathSegmentsWithoutVolume]);

        return (volumeKey, fileKey);
    }
}
