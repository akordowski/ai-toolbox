using AIToolbox.IO;
using Microsoft.SemanticKernel.Memory;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;

#pragma warning disable CA1308

namespace AIToolbox.SemanticKernel.Memory;

public class SimpleMemoryStore : IMemoryStore
{
    private readonly IFileSystem _fileSystem;

    public SimpleMemoryStore(IFileSystem fileSystem)
    {
        ArgumentNullException.ThrowIfNull(fileSystem, nameof(fileSystem));

        _fileSystem = fileSystem;
    }

    /// <inheritdoc />
    public async Task CreateCollectionAsync(string collectionName, CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(collectionName, nameof(collectionName));

        var volume = GetVolume(collectionName);
        await _fileSystem.CreateVolumeAsync(volume, cancellationToken).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task DeleteCollectionAsync(string collectionName, CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(collectionName, nameof(collectionName));

        var volume = GetVolume(collectionName);
        await _fileSystem.DeleteVolumeAsync(volume, cancellationToken).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task<bool> DoesCollectionExistAsync(string collectionName, CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(collectionName, nameof(collectionName));

        var volume = GetVolume(collectionName);
        return await _fileSystem.VolumeExistsAsync(volume, cancellationToken).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task<MemoryRecord?> GetAsync(
        string collectionName,
        string key,
        bool withEmbedding = false,
        CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(collectionName, nameof(collectionName));
        ArgumentException.ThrowIfNullOrWhiteSpace(key, nameof(key));

        var volume = GetVolume(collectionName);
        var fileName = GetFileName(key);
        var fileExists = await _fileSystem.FileExistsAsync(volume, null, fileName, cancellationToken).ConfigureAwait(false);

        if (!fileExists)
        {
            return null;
        }

        var data = await _fileSystem.ReadFileAsTextAsync(volume, null, fileName, cancellationToken).ConfigureAwait(false);
        var record = GetDeserializedData(data);

        return withEmbedding
            ? record
            : MemoryRecord.FromMetadata(record.Metadata, null, record.Key, record.Timestamp);
    }

    /// <inheritdoc />
    public async IAsyncEnumerable<MemoryRecord> GetBatchAsync(
        string collectionName,
        IEnumerable<string> keys,
        bool withEmbeddings = false,
        [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(collectionName, nameof(collectionName));
        ArgumentNullException.ThrowIfNull(keys, nameof(keys));

        foreach (var key in keys)
        {
            var record = await GetAsync(collectionName, key, withEmbeddings, cancellationToken).ConfigureAwait(false);

            if (record is null)
            {
                continue;
            }

            yield return record;
        }
    }

    /// <inheritdoc />
    public async IAsyncEnumerable<string> GetCollectionsAsync(
        [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        var volumes = await _fileSystem.GetVolumesAsync(cancellationToken).ConfigureAwait(false);

        foreach (var volume in volumes)
        {
            yield return volume;
        }
    }

    /// <inheritdoc />
    public async Task<(MemoryRecord, double)?> GetNearestMatchAsync(
        string collectionName,
        ReadOnlyMemory<float> embedding,
        double minRelevanceScore = 0,
        bool withEmbedding = false,
        CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(collectionName, nameof(collectionName));

        var store = await GetStoreAsync(collectionName, cancellationToken).ConfigureAwait(false);

        return await store.GetNearestMatchAsync(
                collectionName,
                embedding,
                minRelevanceScore,
                withEmbedding,
                cancellationToken)
            .ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async IAsyncEnumerable<(MemoryRecord, double)> GetNearestMatchesAsync(
        string collectionName,
        ReadOnlyMemory<float> embedding,
        int limit,
        double minRelevanceScore = 0,
        bool withEmbeddings = false,
        [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(collectionName, nameof(collectionName));

        var store = await GetStoreAsync(collectionName, cancellationToken).ConfigureAwait(false);
        var matches = store.GetNearestMatchesAsync(
            collectionName,
            embedding,
            limit,
            minRelevanceScore,
            withEmbeddings,
            cancellationToken);

        await foreach (var match in matches.ConfigureAwait(false))
        {
            yield return match;
        }
    }

    /// <inheritdoc />
    public async Task RemoveAsync(
        string collectionName,
        string key,
        CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(collectionName, nameof(collectionName));
        ArgumentException.ThrowIfNullOrWhiteSpace(key, nameof(key));

        var volume = GetVolume(collectionName);
        var fileName = GetFileName(key);

        await _fileSystem.DeleteFileAsync(volume, null, fileName, cancellationToken).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task RemoveBatchAsync(
        string collectionName,
        IEnumerable<string> keys,
        CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(collectionName, nameof(collectionName));
        ArgumentNullException.ThrowIfNull(keys, nameof(keys));

        var volume = GetVolume(collectionName);

        foreach (var key in keys)
        {
            var fileName = GetFileName(key);

            await _fileSystem.DeleteFileAsync(volume, null, fileName, cancellationToken).ConfigureAwait(false);
        }
    }

    /// <inheritdoc />
    public async Task<string> UpsertAsync(
        string collectionName,
        MemoryRecord record,
        CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(collectionName, nameof(collectionName));
        ArgumentNullException.ThrowIfNull(record, nameof(record));
        ArgumentNullException.ThrowIfNull(record.Metadata.Id);

        record.Key = record.Metadata.Id;

        var volume = GetVolume(collectionName);
        var fileName = GetFileName(record.Key);
        var data = GetSerializedData(record);

        await _fileSystem.WriteFileAsync(volume, null, fileName, data, cancellationToken).ConfigureAwait(false);

        return record.Metadata.Id;
    }

    /// <inheritdoc />
    public async IAsyncEnumerable<string> UpsertBatchAsync(
        string collectionName,
        IEnumerable<MemoryRecord> records,
        [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(collectionName, nameof(collectionName));
        ArgumentNullException.ThrowIfNull(records, nameof(records));

        foreach (var record in records)
        {
            yield return await UpsertAsync(collectionName, record, cancellationToken).ConfigureAwait(false);
        }
    }

    private static string GetFileName(string key)
    {
        var bytes = Encoding.UTF8.GetBytes(key);
        return Convert.ToBase64String(bytes).Replace('=', '_');
    }

    public static MemoryRecord GetDeserializedData(string data) =>
        JsonSerializer.Deserialize<MemoryRecord>(data)!;

    public static string GetSerializedData(MemoryRecord record) =>
        JsonSerializer.Serialize(record);

    private static string GetVolume(string collectionName) =>
        SimpleMemoryStoreRegex.ReplaceVolumeCharsRegex().Replace(collectionName, "-").Trim().ToLowerInvariant();

    private async Task<VolatileMemoryStore> GetStoreAsync(string collectionName, CancellationToken cancellationToken)
    {
        var volume = GetVolume(collectionName);
        var files = await _fileSystem.ReadFilesAsTextAsync(volume, null, cancellationToken).ConfigureAwait(false);
        var records = files.Select(kvp => GetDeserializedData(kvp.Value));

        var store = new VolatileMemoryStore();
        await store.CreateCollectionAsync(collectionName, cancellationToken).ConfigureAwait(false);
        await store.UpsertBatchAsync(collectionName, records, cancellationToken).ToListAsync(cancellationToken).ConfigureAwait(false);

        return store;
    }
}
