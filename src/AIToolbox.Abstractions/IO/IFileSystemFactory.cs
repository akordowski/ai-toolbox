namespace AIToolbox.IO;

public interface IFileSystemFactory
{
    IFileSystem CreateFileSystem(FileSystemOptions options);
}
