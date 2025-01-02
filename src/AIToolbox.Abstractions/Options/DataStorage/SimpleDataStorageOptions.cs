namespace AIToolbox.Options.DataStorage;

public class SimpleDataStorageOptions
{
    /// <summary>
    /// The type of storage to use. Defaults to volatile.
    /// </summary>
    public StorageType StorageType { get; set; } = StorageType.Volatile;

    /// <summary>
    /// Directory of the file storage.
    /// </summary>
    public string Directory { get; set; } = "tmp-data";
}
