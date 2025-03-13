namespace AIToolbox.IO;

public interface IFileSystem
{
    Task CreateVolumeAsync(string volume, CancellationToken cancellationToken = default);

    Task DeleteVolumeAsync(string volume, CancellationToken cancellationToken = default);

    Task<IEnumerable<string>> GetVolumesAsync(CancellationToken cancellationToken = default);

    Task<bool> VolumeExistsAsync(string volume, CancellationToken cancellationToken = default);

    Task DeleteFileAsync(
        string volume,
        string? relPath,
        string fileName,
        CancellationToken cancellationToken = default);

    Task<bool> FileExistsAsync(
        string volume,
        string? relPath,
        string fileName,
        CancellationToken cancellationToken = default);

    Task<IEnumerable<string>> GetFileNamesAsync(
        string volume,
        string? relPath,
        CancellationToken cancellationToken = default);

    Task<BinaryData> ReadFileAsBinaryAsync(
        string volume,
        string? relPath,
        string fileName,
        CancellationToken cancellationToken = default);

    Task<string> ReadFileAsTextAsync(
        string volume,
        string? relPath,
        string fileName,
        CancellationToken cancellationToken = default);

    Task<IDictionary<string, string>> ReadFilesAsTextAsync(
        string volume,
        string? relPath,
        CancellationToken cancellationToken = default);

    Task WriteFileAsync(
        string volume,
        string? relPath,
        string fileName,
        Stream stream,
        CancellationToken cancellationToken = default);

    Task WriteFileAsync(
        string volume,
        string? relPath,
        string fileName,
        string data,
        CancellationToken cancellationToken = default);
}
