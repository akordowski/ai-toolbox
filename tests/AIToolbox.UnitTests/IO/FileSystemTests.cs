using FluentAssertions;

namespace AIToolbox.IO;

public class FileSystemTests : IDisposable
{
    private static readonly Type TypeArgumentException = typeof(ArgumentException);
    private static readonly Type TypeArgumentNullException = typeof(ArgumentNullException);

    private static readonly string Dir = Path.Combine(Directory.GetCurrentDirectory(), "test-file-system");

    private const string Volume = "TestVolume";
    private const string RelPath = "TestRelPath";
    private const string FileName = "TestFileName.txt";
    private const string Data = "TestData";

    public void Dispose()
    {
        if (Directory.Exists(Dir))
        {
            Directory.Delete(Dir, true);
        }
    }

    public static TheoryData<Action, string, Type> ConstructWithInvalidParameters =>
        new()
        {
            { () => _ = new DiskFileSystem(null!), "directory", TypeArgumentNullException },
            { () => _ = new DiskFileSystem(""), "directory", TypeArgumentException },
            { () => _ = new DiskFileSystem(" "), "directory", TypeArgumentException }
        };

    public static TheoryData<Func<Task>, string, Type> InvokeMethodWithInvalidParameters
    {
        get
        {
            var theoryData = new TheoryData<Func<Task>, string, Type>();
            var types = (FileSystemType[])Enum.GetValues(typeof(FileSystemType));

            foreach (var type in types)
            {
                var fileSystem = GetFileSystem(type);

                theoryData.Add(() => fileSystem.CreateVolumeAsync(null!), "volume", TypeArgumentNullException);
                theoryData.Add(() => fileSystem.CreateVolumeAsync(""), "volume", TypeArgumentException);
                theoryData.Add(() => fileSystem.CreateVolumeAsync(" "), "volume", TypeArgumentException);

                theoryData.Add(() => fileSystem.DeleteVolumeAsync(null!), "volume", TypeArgumentNullException);
                theoryData.Add(() => fileSystem.DeleteVolumeAsync(""), "volume", TypeArgumentException);
                theoryData.Add(() => fileSystem.DeleteVolumeAsync(" "), "volume", TypeArgumentException);

                theoryData.Add(() => fileSystem.VolumeExistsAsync(null!), "volume", TypeArgumentNullException);
                theoryData.Add(() => fileSystem.VolumeExistsAsync(""), "volume", TypeArgumentException);
                theoryData.Add(() => fileSystem.VolumeExistsAsync(" "), "volume", TypeArgumentException);

                theoryData.Add(() => fileSystem.DeleteFileAsync(null!, null, null!), "volume", TypeArgumentNullException);
                theoryData.Add(() => fileSystem.DeleteFileAsync("", null, null!), "volume", TypeArgumentException);
                theoryData.Add(() => fileSystem.DeleteFileAsync(" ", null, null!), "volume", TypeArgumentException);
                theoryData.Add(() => fileSystem.DeleteFileAsync(Volume, "", null!), "relPath", TypeArgumentException);
                theoryData.Add(() => fileSystem.DeleteFileAsync(Volume, " ", null!), "relPath", TypeArgumentException);
                theoryData.Add(() => fileSystem.DeleteFileAsync(Volume, RelPath, null!), "fileName", TypeArgumentNullException);
                theoryData.Add(() => fileSystem.DeleteFileAsync(Volume, RelPath, ""), "fileName", TypeArgumentException);
                theoryData.Add(() => fileSystem.DeleteFileAsync(Volume, RelPath, " "), "fileName", TypeArgumentException);

                theoryData.Add(() => fileSystem.FileExistsAsync(null!, null, null!), "volume", TypeArgumentNullException);
                theoryData.Add(() => fileSystem.FileExistsAsync("", null, null!), "volume", TypeArgumentException);
                theoryData.Add(() => fileSystem.FileExistsAsync(" ", null, null!), "volume", TypeArgumentException);
                theoryData.Add(() => fileSystem.FileExistsAsync(Volume, "", null!), "relPath", TypeArgumentException);
                theoryData.Add(() => fileSystem.FileExistsAsync(Volume, " ", null!), "relPath", TypeArgumentException);
                theoryData.Add(() => fileSystem.FileExistsAsync(Volume, RelPath, null!), "fileName", TypeArgumentNullException);
                theoryData.Add(() => fileSystem.FileExistsAsync(Volume, RelPath, ""), "fileName", TypeArgumentException);
                theoryData.Add(() => fileSystem.FileExistsAsync(Volume, RelPath, " "), "fileName", TypeArgumentException);

                theoryData.Add(() => fileSystem.GetFileNamesAsync(null!, null), "volume", TypeArgumentNullException);
                theoryData.Add(() => fileSystem.GetFileNamesAsync("", null), "volume", TypeArgumentException);
                theoryData.Add(() => fileSystem.GetFileNamesAsync(" ", null), "volume", TypeArgumentException);
                theoryData.Add(() => fileSystem.GetFileNamesAsync(Volume, ""), "relPath", TypeArgumentException);
                theoryData.Add(() => fileSystem.GetFileNamesAsync(Volume, " "), "relPath", TypeArgumentException);

                theoryData.Add(() => fileSystem.ReadFileAsBinaryAsync(null!, null, null!), "volume", TypeArgumentNullException);
                theoryData.Add(() => fileSystem.ReadFileAsBinaryAsync("", null, null!), "volume", TypeArgumentException);
                theoryData.Add(() => fileSystem.ReadFileAsBinaryAsync(" ", null, null!), "volume", TypeArgumentException);
                theoryData.Add(() => fileSystem.ReadFileAsBinaryAsync(Volume, "", null!), "relPath", TypeArgumentException);
                theoryData.Add(() => fileSystem.ReadFileAsBinaryAsync(Volume, " ", null!), "relPath", TypeArgumentException);
                theoryData.Add(() => fileSystem.ReadFileAsBinaryAsync(Volume, RelPath, null!), "fileName", TypeArgumentNullException);
                theoryData.Add(() => fileSystem.ReadFileAsBinaryAsync(Volume, RelPath, ""), "fileName", TypeArgumentException);
                theoryData.Add(() => fileSystem.ReadFileAsBinaryAsync(Volume, RelPath, " "), "fileName", TypeArgumentException);

                theoryData.Add(() => fileSystem.ReadFileAsTextAsync(null!, null, null!), "volume", TypeArgumentNullException);
                theoryData.Add(() => fileSystem.ReadFileAsTextAsync("", null, null!), "volume", TypeArgumentException);
                theoryData.Add(() => fileSystem.ReadFileAsTextAsync(" ", null, null!), "volume", TypeArgumentException);
                theoryData.Add(() => fileSystem.ReadFileAsTextAsync(Volume, "", null!), "relPath", TypeArgumentException);
                theoryData.Add(() => fileSystem.ReadFileAsTextAsync(Volume, " ", null!), "relPath", TypeArgumentException);
                theoryData.Add(() => fileSystem.ReadFileAsTextAsync(Volume, RelPath, null!), "fileName", TypeArgumentNullException);
                theoryData.Add(() => fileSystem.ReadFileAsTextAsync(Volume, RelPath, ""), "fileName", TypeArgumentException);
                theoryData.Add(() => fileSystem.ReadFileAsTextAsync(Volume, RelPath, " "), "fileName", TypeArgumentException);

                theoryData.Add(() => fileSystem.ReadFilesAsTextAsync(null!, null), "volume", TypeArgumentNullException);
                theoryData.Add(() => fileSystem.ReadFilesAsTextAsync("", null), "volume", TypeArgumentException);
                theoryData.Add(() => fileSystem.ReadFilesAsTextAsync(" ", null), "volume", TypeArgumentException);
                theoryData.Add(() => fileSystem.ReadFilesAsTextAsync(Volume, ""), "relPath", TypeArgumentException);
                theoryData.Add(() => fileSystem.ReadFilesAsTextAsync(Volume, " "), "relPath", TypeArgumentException);

                theoryData.Add(() => fileSystem.WriteFileAsync(null!, null, null!, (Stream)null!), "volume", TypeArgumentNullException);
                theoryData.Add(() => fileSystem.WriteFileAsync("", null, null!, (Stream)null!), "volume", TypeArgumentException);
                theoryData.Add(() => fileSystem.WriteFileAsync(" ", null, null!, (Stream)null!), "volume", TypeArgumentException);
                theoryData.Add(() => fileSystem.WriteFileAsync(Volume, "", null!, (Stream)null!), "relPath", TypeArgumentException);
                theoryData.Add(() => fileSystem.WriteFileAsync(Volume, " ", null!, (Stream)null!), "relPath", TypeArgumentException);
                theoryData.Add(() => fileSystem.WriteFileAsync(Volume, RelPath, null!, (Stream)null!), "fileName", TypeArgumentNullException);
                theoryData.Add(() => fileSystem.WriteFileAsync(Volume, RelPath, "", (Stream)null!), "fileName", TypeArgumentException);
                theoryData.Add(() => fileSystem.WriteFileAsync(Volume, RelPath, " ", (Stream)null!), "fileName", TypeArgumentException);
                theoryData.Add(() => fileSystem.WriteFileAsync(Volume, RelPath, FileName, (Stream)null!), "stream", TypeArgumentNullException);

                theoryData.Add(() => fileSystem.WriteFileAsync(null!, null, null!, (string)null!), "volume", TypeArgumentNullException);
                theoryData.Add(() => fileSystem.WriteFileAsync("", null, null!, (string)null!), "volume", TypeArgumentException);
                theoryData.Add(() => fileSystem.WriteFileAsync(" ", null, null!, (string)null!), "volume", TypeArgumentException);
                theoryData.Add(() => fileSystem.WriteFileAsync(Volume, "", null!, (string)null!), "relPath", TypeArgumentException);
                theoryData.Add(() => fileSystem.WriteFileAsync(Volume, " ", null!, (string)null!), "relPath", TypeArgumentException);
                theoryData.Add(() => fileSystem.WriteFileAsync(Volume, RelPath, null!, (string)null!), "fileName", TypeArgumentNullException);
                theoryData.Add(() => fileSystem.WriteFileAsync(Volume, RelPath, "", (string)null!), "fileName", TypeArgumentException);
                theoryData.Add(() => fileSystem.WriteFileAsync(Volume, RelPath, " ", (string)null!), "fileName", TypeArgumentException);
                theoryData.Add(() => fileSystem.WriteFileAsync(Volume, RelPath, FileName, (string)null!), "data", TypeArgumentNullException);
                theoryData.Add(() => fileSystem.WriteFileAsync(Volume, RelPath, FileName, ""), "data", TypeArgumentException);
                theoryData.Add(() => fileSystem.WriteFileAsync(Volume, RelPath, FileName, " "), "data", TypeArgumentException);
            }

            return theoryData;
        }
    }

    [Theory]
    [MemberData(nameof(ConstructWithInvalidParameters))]
    public void Should_Throw_On_Construct_With_Invalid_Parameters(Action act, string parameterName, Type exceptionType)
    {
        // Assert
        if (exceptionType == TypeArgumentException)
        {
            act.Should().Throw<ArgumentException>().WithParameterName(parameterName);
        }
        else if (exceptionType == TypeArgumentNullException)
        {
            act.Should().Throw<ArgumentNullException>().WithParameterName(parameterName);
        }
    }

    [Theory]
    [MemberData(nameof(InvokeMethodWithInvalidParameters))]
    public async Task Should_Throw_On_Invoke_Method_With_Invalid_Parameters(Func<Task> func, string parameterName, Type exceptionType)
    {
        // Assert
        if (exceptionType == TypeArgumentException)
        {
            await func.Should().ThrowAsync<ArgumentException>().WithParameterName(parameterName);
        }
        else if (exceptionType == TypeArgumentNullException)
        {
            await func.Should().ThrowAsync<ArgumentNullException>().WithParameterName(parameterName);
        }
    }

    [Theory]
    [InlineData(FileSystemType.Disk)]
    [InlineData(FileSystemType.Volatile)]
    public async Task Should_Create_Volume(FileSystemType type)
    {
        // Arrange
        var fileSystem = GetFileSystem(type);

        // Act
        await fileSystem.CreateVolumeAsync(Volume);

        // Assert
        var result = await fileSystem.VolumeExistsAsync(Volume);
        result.Should().BeTrue();
    }

    [Theory]
    [InlineData(FileSystemType.Disk)]
    [InlineData(FileSystemType.Volatile)]
    public async Task Should_Delete_Volume(FileSystemType type)
    {
        // Arrange
        var fileSystem = GetFileSystem(type);
        await fileSystem.CreateVolumeAsync(Volume);

        // Act
        await fileSystem.DeleteVolumeAsync(Volume);

        // Assert
        var result = await fileSystem.VolumeExistsAsync(Volume);
        result.Should().BeFalse();
    }

    [Theory]
    [InlineData(FileSystemType.Disk)]
    [InlineData(FileSystemType.Volatile)]
    public async Task Should_Get_Volumes(FileSystemType type)
    {
        // Arrange
        var fileSystem = GetFileSystem(type);
        var volumes = new List<string> { "Volume1", "Volume2" };

        foreach (var volume in volumes)
        {
            await fileSystem.CreateVolumeAsync(volume);
        }

        // Act
        var result = await fileSystem.GetVolumesAsync();

        // Assert
        result.Should().BeEquivalentTo(volumes);
    }

    [Theory]
    [InlineData(FileSystemType.Disk, true)]
    [InlineData(FileSystemType.Disk, false)]
    [InlineData(FileSystemType.Volatile, true)]
    [InlineData(FileSystemType.Volatile, false)]
    public async Task Should_Check_If_Volume_Exists(FileSystemType type, bool expectedResult)
    {
        // Arrange
        var fileSystem = GetFileSystem(type);

        if (expectedResult)
        {
            await fileSystem.CreateVolumeAsync(Volume);
        }

        // Act
        var result = await fileSystem.VolumeExistsAsync(Volume);

        // Assert
        result.Should().Be(expectedResult);
    }

    [Theory]
    [InlineData(FileSystemType.Disk, null)]
    [InlineData(FileSystemType.Disk, RelPath)]
    [InlineData(FileSystemType.Volatile, null)]
    [InlineData(FileSystemType.Volatile, RelPath)]
    public async Task Should_Delete_File(FileSystemType type, string? relPath)
    {
        // Arrange
        var fileSystem = GetFileSystem(type);

        await fileSystem.WriteFileAsync(Volume, relPath, FileName, Data);

        // Act
        await fileSystem.DeleteFileAsync(Volume, relPath, FileName);

        // Assert
        var result = await fileSystem.FileExistsAsync(Volume, relPath, FileName);
        result.Should().BeFalse();
    }

    [Theory]
    [InlineData(FileSystemType.Disk, null, true)]
    [InlineData(FileSystemType.Disk, RelPath, true)]
    [InlineData(FileSystemType.Disk, null, false)]
    [InlineData(FileSystemType.Disk, RelPath, false)]
    [InlineData(FileSystemType.Volatile, null, true)]
    [InlineData(FileSystemType.Volatile, RelPath, true)]
    [InlineData(FileSystemType.Volatile, null, false)]
    [InlineData(FileSystemType.Volatile, RelPath, false)]
    public async Task Should_Check_If_File_Exists(FileSystemType type, string? relPath, bool expectedResult)
    {
        // Arrange
        var fileSystem = GetFileSystem(type);

        if (expectedResult)
        {
            await fileSystem.WriteFileAsync(Volume, relPath, FileName, Data);
        }

        // Act
        var result = await fileSystem.FileExistsAsync(Volume, relPath, FileName);

        // Assert
        result.Should().Be(expectedResult);
    }

    [Theory]
    [InlineData(FileSystemType.Disk, null)]
    [InlineData(FileSystemType.Disk, RelPath)]
    [InlineData(FileSystemType.Volatile, null)]
    [InlineData(FileSystemType.Volatile, RelPath)]
    public async Task Should_Get_File_Names(FileSystemType type, string? relPath)
    {
        // Arrange
        var fileSystem = GetFileSystem(type);
        var fileNames = new List<string> { "file1.txt", "file2.txt" };

        foreach (var fileName in fileNames)
        {
            await fileSystem.WriteFileAsync(Volume, relPath, fileName, Data);
        }

        // Act
        var result = await fileSystem.GetFileNamesAsync(Volume, relPath);

        // Assert
        result.Should().BeEquivalentTo(fileNames);
    }

    [Theory]
    [InlineData(FileSystemType.Disk, null)]
    [InlineData(FileSystemType.Disk, RelPath)]
    [InlineData(FileSystemType.Volatile, null)]
    [InlineData(FileSystemType.Volatile, RelPath)]
    public async Task Should_Read_File_As_Binary(FileSystemType type, string? relPath)
    {
        // Arrange
        var fileSystem = GetFileSystem(type);
        var bytes = new byte[] { 1, 2, 3 };
        var stream = new MemoryStream(bytes);

        await fileSystem.WriteFileAsync(Volume, relPath, FileName, stream);

        // Act
        var result = await fileSystem.ReadFileAsBinaryAsync(Volume, relPath, FileName);

        // Assert
        result.ToArray().Should().BeEquivalentTo(bytes);
    }

    [Theory]
    [InlineData(FileSystemType.Disk, null)]
    [InlineData(FileSystemType.Disk, RelPath)]
    [InlineData(FileSystemType.Volatile, null)]
    [InlineData(FileSystemType.Volatile, RelPath)]
    public async Task Should_Read_File_As_Text(FileSystemType type, string? relPath)
    {
        // Arrange
        var fileSystem = GetFileSystem(type);

        await fileSystem.WriteFileAsync(Volume, relPath, FileName, Data);

        // Act
        var result = await fileSystem.ReadFileAsTextAsync(Volume, relPath, FileName);

        // Assert
        result.Should().Be(Data);
    }

    [Theory]
    [InlineData(FileSystemType.Disk, null)]
    [InlineData(FileSystemType.Disk, RelPath)]
    [InlineData(FileSystemType.Volatile, null)]
    [InlineData(FileSystemType.Volatile, RelPath)]
    public async Task Should_Read_Files_As_Text(FileSystemType type, string? relPath)
    {
        // Arrange
        var fileSystem = GetFileSystem(type);
        var filesData = new Dictionary<string, string>
        {
            { "file1.txt", "content1" },
            { "file2.txt", "content2" }
        };

        foreach (var fileData in filesData)
        {
            await fileSystem.WriteFileAsync(Volume, relPath, fileData.Key, fileData.Value);
        }

        // Act
        var result = await fileSystem.ReadFilesAsTextAsync(Volume, relPath);

        // Assert
        result.Should().BeEquivalentTo(filesData);
    }

    [Theory]
    [InlineData(FileSystemType.Disk, null)]
    [InlineData(FileSystemType.Disk, RelPath)]
    [InlineData(FileSystemType.Volatile, null)]
    [InlineData(FileSystemType.Volatile, RelPath)]
    public async Task Should_Write_File_From_Stream(FileSystemType type, string? relPath)
    {
        // Arrange
        var fileSystem = GetFileSystem(type);
        var bytes = new byte[] { 1, 2, 3 };
        var stream = new MemoryStream(bytes);

        // Act
        await fileSystem.WriteFileAsync(Volume, relPath, FileName, stream);

        // Assert
        var result = await fileSystem.ReadFileAsBinaryAsync(Volume, relPath, FileName);
        result.ToArray().Should().BeEquivalentTo(bytes);
    }

    [Theory]
    [InlineData(FileSystemType.Disk, null)]
    [InlineData(FileSystemType.Disk, RelPath)]
    [InlineData(FileSystemType.Volatile, null)]
    [InlineData(FileSystemType.Volatile, RelPath)]
    public async Task Should_Write_File_From_String(FileSystemType type, string? relPath)
    {
        // Arrange
        var fileSystem = GetFileSystem(type);

        // Act
        await fileSystem.WriteFileAsync(Volume, relPath, FileName, Data);

        // Assert
        var result = await fileSystem.ReadFileAsTextAsync(Volume, relPath, FileName);
        result.Should().Be(Data);
    }

    private static IFileSystem GetFileSystem(FileSystemType type)
    {
        if (type == FileSystemType.Disk)
        {
            Directory.CreateDirectory(Dir);
        }

        return type switch
        {
            FileSystemType.Disk => new DiskFileSystem(Dir),
            FileSystemType.Volatile => new VolatileFileSystem(),
            _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
        };
    }

    public enum FileSystemType
    {
        Disk,
        Volatile
    }
}
