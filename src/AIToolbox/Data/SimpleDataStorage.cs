using AIToolbox.IO;
using AIToolbox.Options;
using AIToolbox.Options.DataStorage;
using Microsoft.Extensions.Logging;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace AIToolbox.Data;

public class SimpleDataStorage : IDataStorage
{
    private static readonly Regex ReplaceVolumeCharsRegex = new(Constants.InvalidVolumeCharsPattern);

    private readonly IFileSystem _fileSystem;

    public SimpleDataStorage(
        SimpleDataStorageOptions options,
        ILoggerFactory? loggerFactory = null)
    {
        ArgumentNullException.ThrowIfNull(options, nameof(options));

        _fileSystem = options.StorageType switch
        {
            StorageType.Volatile => new VolatileFileSystem(loggerFactory),
            StorageType.Persistent => new DiskFileSystem(options.Directory, loggerFactory),
            _ => throw new ArgumentOutOfRangeException($"Invalid storage type '{options.StorageType}'")
        };
    }

    public async Task AddOrUpdateAsync<TValue>(
        TValue value,
        Func<TValue, string> keyFactory,
        CancellationToken cancellationToken = default) where TValue : notnull
    {
        ArgumentNullException.ThrowIfNull(value, nameof(value));
        ArgumentNullException.ThrowIfNull(keyFactory, nameof(keyFactory));

        var volume = GetVolume<TValue>();
        var fileName = GetFileName(keyFactory(value));
        var data = GetSerializedData(value);

        await _fileSystem.WriteFileAsync(volume, null, fileName, data, cancellationToken).ConfigureAwait(false);
    }

    public async Task AddOrUpdateRangeAsync<TValue>(
        IEnumerable<TValue> values,
        Func<TValue, string> keyFactory,
        CancellationToken cancellationToken = default) where TValue : notnull
    {
        ArgumentNullException.ThrowIfNull(values, nameof(values));
        ArgumentNullException.ThrowIfNull(keyFactory, nameof(keyFactory));

        var volume = GetVolume<TValue>();

        foreach (var value in values)
        {
            var fileName = GetFileName(keyFactory(value));
            var data = GetSerializedData(value);

            await _fileSystem.WriteFileAsync(volume, null, fileName, data, cancellationToken).ConfigureAwait(false);
        }
    }

    public async IAsyncEnumerable<TValue> AsAsyncEnumerable<TValue>()
    {
        var volume = GetVolume<TValue>();
        var fileNames = Enumerable.Empty<string>();

        try
        {
            fileNames = await _fileSystem.GetFileNamesAsync(volume, null).ConfigureAwait(false);
        }
        catch (DirectoryNotFoundException)
        {
        }

        foreach (var fileName in fileNames)
        {
            var fileData = await _fileSystem.ReadFileAsTextAsync(volume, null, fileName).ConfigureAwait(false);

            yield return GetDeserializedData<TValue>(fileData)!;
        }
    }

    public IQueryable<TValue> AsQueryable<TValue>()
    {
        try
        {
            var volume = GetVolume<TValue>();
            var dict = _fileSystem.ReadFilesAsTextAsync(volume, null).GetAwaiter().GetResult();
            return dict.Values.Select(data => GetDeserializedData<TValue>(data)!).AsQueryable();
        }
        catch (DirectoryNotFoundException)
        {
            return Enumerable.Empty<TValue>().AsQueryable();
        }
    }

    public async Task<TValue?> FindAsync<TValue>(string key, CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(key, nameof(key));

        var volume = GetVolume<TValue>();
        var fileName = GetFileName(key);
        var fileExists = await _fileSystem.FileExistsAsync(volume, null, fileName, cancellationToken).ConfigureAwait(false);

        if (!fileExists)
        {
            return default;
        }

        var fileData = await _fileSystem.ReadFileAsTextAsync(volume, null, fileName, cancellationToken).ConfigureAwait(false);

        return GetDeserializedData<TValue>(fileData);
    }

    public IAsyncEnumerator<TValue> GetAsyncEnumerator<TValue>(CancellationToken cancellationToken = default) =>
        AsAsyncEnumerable<TValue>().GetAsyncEnumerator(cancellationToken);

    public async Task RemoveAsync<TValue>(string key, CancellationToken cancellationToken = default)
    {
        var volume = GetVolume<TValue>();
        var fileName = GetFileName(key);

        await _fileSystem.DeleteFileAsync(volume, null, fileName, cancellationToken).ConfigureAwait(false);
    }

    public async Task RemoveRangeAsync<TValue>(IEnumerable<string> keys, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(keys, nameof(keys));

        var volume = GetVolume<TValue>();

        foreach (var key in keys)
        {
            var fileName = GetFileName(key);

            await _fileSystem.DeleteFileAsync(volume, null, fileName, cancellationToken).ConfigureAwait(false);
        }
    }

    private static string GetFileName(string key)
    {
        var bytes = Encoding.UTF8.GetBytes(key);
        return Convert.ToBase64String(bytes).Replace('=', '_');
    }

    public static T? GetDeserializedData<T>(string data) =>
        JsonSerializer.Deserialize<T>(data);

    public static string GetSerializedData<T>(T data) =>
        JsonSerializer.Serialize(data);

#pragma warning disable CA1308 // Normalize strings to uppercase
    private static string GetVolume<T>() =>
        ReplaceVolumeCharsRegex.Replace(typeof(T).Name, "-").Trim().ToLowerInvariant();
#pragma warning restore CA1308 // Normalize strings to uppercase
}
