namespace AIToolbox.Options.KernelMemory;

public sealed class SimpleFileStorageOptions
{
    /// <summary>
    /// The type of storage to use.
    /// </summary>
    /// <value>Defaults to <see cref="StorageType.Volatile"/></value>
    public StorageType StorageType { get; set; } = StorageType.Volatile;

    /// <summary>
    /// Directory of the file storage.
    /// </summary>
    /// <value>Defaults to 'tmp-memory-files'.</value>
    public string Directory { get; set; } = "tmp-memory-files";
}
