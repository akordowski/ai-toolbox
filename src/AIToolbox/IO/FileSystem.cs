using Microsoft.Extensions.Logging;
using System.Text.RegularExpressions;

namespace AIToolbox.IO;

public abstract class FileSystem : IFileSystem
{
    private readonly ILogger? _logger;

    protected FileSystem(ILogger? logger = null)
    {
        _logger = logger;
    }

    /// <inheritdoc />
    public Task CreateVolumeAsync(string volume, CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(volume, nameof(volume));

        var path = GetPath(volume);
        _logger?.LogDebug("Creating volume: {Volume}", volume);
        CreateDirectory(path);

        return Task.CompletedTask;
    }

    /// <inheritdoc />
    public async Task DeleteVolumeAsync(string volume, CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(volume, nameof(volume));

        var path = GetPath(volume);

        for (var attempt = 1; attempt <= 5; attempt++)
        {
            if (!DirectoryExists(path))
            {
                return;
            }

            try
            {
                _logger?.LogDebug("Deleting volume: {Volume}", volume);
                DeleteDirectory(path);
                return;
            }
            catch (IOException ex) when (ex.Message.Contains("not empty", StringComparison.OrdinalIgnoreCase))
            {
                await Task.Delay(TimeSpan.FromMilliseconds(attempt * 75), cancellationToken).ConfigureAwait(false);
            }
        }
    }

    /// <inheritdoc />
    public Task<IEnumerable<string>> GetVolumesAsync(CancellationToken cancellationToken = default) =>
        Task.FromResult(GetVolumes());

    /// <inheritdoc />
    public Task<bool> VolumeExistsAsync(string volume, CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(volume, nameof(volume));

        var path = GetPath(volume);
        var exists = DirectoryExists(path);

        return Task.FromResult(exists);
    }

    /// <inheritdoc />
    public Task DeleteFileAsync(
        string volume,
        string? relPath,
        string fileName,
        CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(volume, nameof(volume));
        ThrowIfWhiteSpace(relPath, nameof(relPath));
        ArgumentException.ThrowIfNullOrWhiteSpace(fileName, nameof(fileName));

        var path = GetPath(volume, relPath, fileName);

        if (FileExists(path))
        {
            _logger?.LogDebug("Deleting file: {Path}", path);
            DeleteFile(path);
        }

        return Task.CompletedTask;
    }

    /// <inheritdoc />
    public Task<bool> FileExistsAsync(
        string volume,
        string? relPath,
        string fileName,
        CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(volume, nameof(volume));
        ThrowIfWhiteSpace(relPath, nameof(relPath));
        ArgumentException.ThrowIfNullOrWhiteSpace(fileName, nameof(fileName));

        var path = GetPath(volume, relPath, fileName);
        var exists = FileExists(path);

        return Task.FromResult(exists);
    }

    /// <inheritdoc />
    public Task<IEnumerable<string>> GetFileNamesAsync(
        string volume,
        string? relPath,
        CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(volume, nameof(volume));
        ThrowIfWhiteSpace(relPath, nameof(relPath));

        var path = GetPath(volume, relPath);
        var fileNames = new List<string>();

        if (!DirectoryExists(path))
        {
            throw new DirectoryNotFoundException($"Directory not found: {path}");
        }

        var files = GetFiles(path);

        foreach (var file in files)
        {
            var fileName = file;

            if (fileName.StartsWith(path, StringComparison.OrdinalIgnoreCase))
            {
                fileName = fileName[path.Length..].Trim('/').Trim('\\');
            }

            fileNames.Add(fileName);
        }

        return Task.FromResult((IEnumerable<string>)fileNames);
    }

    /// <inheritdoc />
    public async Task<BinaryData> ReadFileAsBinaryAsync(
        string volume,
        string? relPath,
        string fileName,
        CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(volume, nameof(volume));
        ThrowIfWhiteSpace(relPath, nameof(relPath));
        ArgumentException.ThrowIfNullOrWhiteSpace(fileName, nameof(fileName));

        var dirPath = GetPath(volume, relPath);

        if (!DirectoryExists(dirPath))
        {
            throw new DirectoryNotFoundException($"Directory not found: {dirPath}");
        }

        var filePath = GetPath(volume, relPath, fileName);

        if (!FileExists(filePath))
        {
            throw new FileNotFoundException($"File not found: {filePath}");
        }

        return await ReadFileAsync(filePath, cancellationToken).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task<string> ReadFileAsTextAsync(
        string volume,
        string? relPath,
        string fileName,
        CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(volume, nameof(volume));
        ThrowIfWhiteSpace(relPath, nameof(relPath));
        ArgumentException.ThrowIfNullOrWhiteSpace(fileName, nameof(fileName));

        return (await ReadFileAsBinaryAsync(volume, relPath, fileName, cancellationToken).ConfigureAwait(false))
            .ToString();
    }

    /// <inheritdoc />
    public async Task<IDictionary<string, string>> ReadFilesAsTextAsync(
        string volume,
        string? relPath,
        CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(volume, nameof(volume));
        ThrowIfWhiteSpace(relPath, nameof(relPath));

        var path = GetPath(volume, relPath);

        if (!DirectoryExists(path))
        {
            throw new DirectoryNotFoundException($"Directory not found: {path}");
        }

        var dict = new Dictionary<string, string>();
        var files = GetFiles(path);

        foreach (var file in files)
        {
            var fileName = file;

            if (fileName.StartsWith(path, StringComparison.OrdinalIgnoreCase))
            {
                fileName = fileName[path.Length..].Trim('/').Trim('\\');
            }

            dict[fileName] = (await ReadFileAsync(file, cancellationToken).ConfigureAwait(false)).ToString();
        }

        return dict;
    }

    /// <inheritdoc />
    public async Task WriteFileAsync(
        string volume,
        string? relPath,
        string fileName,
        Stream stream,
        CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(volume, nameof(volume));
        ThrowIfWhiteSpace(relPath, nameof(relPath));
        ArgumentException.ThrowIfNullOrWhiteSpace(fileName, nameof(fileName));
        ArgumentNullException.ThrowIfNull(stream, nameof(stream));

        var binaryData = await BinaryData.FromStreamAsync(stream, cancellationToken).ConfigureAwait(false);
        await WriteFileAsync(volume, relPath, fileName, binaryData, cancellationToken).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task WriteFileAsync(
        string volume,
        string? relPath,
        string fileName,
        string data,
        CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(volume, nameof(volume));
        ThrowIfWhiteSpace(relPath, nameof(relPath));
        ArgumentException.ThrowIfNullOrWhiteSpace(fileName, nameof(fileName));
        ArgumentException.ThrowIfNullOrWhiteSpace(data, nameof(data));

        var binaryData = BinaryData.FromString(data);
        await WriteFileAsync(volume, relPath, fileName, binaryData, cancellationToken).ConfigureAwait(false);
    }

    /// <inheritdoc />
    private async Task WriteFileAsync(
        string volume,
        string? relPath,
        string fileName,
        BinaryData data,
        CancellationToken cancellationToken = default)
    {
        var dirPath = GetPath(volume, relPath);
        CreateDirectory(dirPath);

        var filePath = GetPath(volume, relPath, fileName);
        _logger?.LogDebug("Writing file: {Path}", filePath);
        await WriteFileAsync(filePath, data.ToArray(), cancellationToken).ConfigureAwait(false);
    }

    protected abstract void CreateDirectory(string path);

    protected abstract void DeleteDirectory(string path);

    protected abstract void DeleteFile(string path);

    protected abstract bool DirectoryExists(string path);

    protected abstract bool FileExists(string path);

    protected abstract string[] GetFiles(string path);

    protected abstract IEnumerable<string> GetVolumes();

    protected abstract string GetFileName(string path);

    protected abstract Task<BinaryData> ReadFileAsync(string path, CancellationToken cancellationToken);

    protected abstract Task WriteFileAsync(string path, byte[] bytes, CancellationToken cancellationToken);

    protected abstract string CombinePaths(params string[] paths);

    private string GetPath(string volume)
    {
        ValidateVolume(volume);
        return CombinePaths(volume);
    }

    private string GetPath(string volume, string? relPath)
    {
        ValidateVolume(volume);
        ValidateRelPath(relPath);
        return CombinePaths(volume, relPath ?? "");
    }

    private string GetPath(string volume, string? relPath, string fileName)
    {
        ValidateVolume(volume);
        ValidateRelPath(relPath);
        ValidateFileName(fileName);
        return CombinePaths(volume, relPath ?? "", fileName);
    }

    private static void ValidateVolume(string volume)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(volume, nameof(volume));

        if (!TryValidateRegEx(FileSystemRegex.InvalidVolumeCharsRegex(), volume, out var invalidChars))
        {
            throw new ArgumentException($"Volume contains invalid characters: {invalidChars}", nameof(volume));
        }
    }

    private static void ValidateRelPath(string? relPath)
    {
        if (relPath is not null && string.IsNullOrWhiteSpace(relPath))
        {
            throw new ArgumentException("Path cannot contain whitespace characters.", nameof(relPath));
        }

        if (!TryValidateRegEx(FileSystemRegex.InvalidPathCharsRegex(), relPath ?? "", out var invalidChars))
        {
            throw new ArgumentException($"Path contains invalid characters: {invalidChars}", nameof(relPath));
        }
    }

    private static void ValidateFileName(string fileName)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(fileName, nameof(fileName));

        if (!TryValidateRegEx(FileSystemRegex.InvalidFileNameCharsRegex(), fileName, out var invalidChars))
        {
            throw new ArgumentException($"File name contains invalid characters: {invalidChars}", nameof(fileName));
        }
    }

    private static bool TryValidateRegEx(Regex regex, string value, out string invalidChars)
    {
        invalidChars = "";
        var matches = regex.Matches(value);

        if (matches.Count == 0)
        {
            return true;
        }

        var chars = matches
            .SelectMany(o => o.Value.ToCharArray())
            .Where(ch => !char.IsWhiteSpace(ch))
            .Distinct()
            .ToArray();

        invalidChars = new string(chars);

        return false;
    }

    public static void ThrowIfWhiteSpace(string? argument, string? paramName = null)
    {
        if (argument is not null)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(argument, paramName);
        }
    }
}
