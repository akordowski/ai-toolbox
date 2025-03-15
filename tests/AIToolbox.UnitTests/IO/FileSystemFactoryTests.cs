using FluentAssertions;

namespace AIToolbox.IO;

public class FileSystemFactoryTests
{
    private readonly FileSystemFactory _factory = new();

    [Fact]
    public void Should_Throw_When_Options_Is_Null()
    {
        // Act
        var act = () => _factory.CreateFileSystem(null!);

        // Assert
        act.Should().Throw<ArgumentNullException>().WithParameterName("options");
    }

    [Fact]
    public void Should_Throw_When_Type_Is_Invalid()
    {
        // Arrange
        var options = new FileSystemOptions { Type = (FileSystemType)999 };

        // Act
        var act = () => _factory.CreateFileSystem(options);

        // Assert
        act.Should().Throw<ArgumentOutOfRangeException>().WithMessage("*Invalid file system type '999'*");
    }
    public static TheoryData<FileSystemType, Type> CreateByType =>
        new()
        {
            { FileSystemType.Volatile, typeof(VolatileFileSystem) },
            { FileSystemType.Disk, typeof(DiskFileSystem) }
        };

    [Theory]
    [MemberData(nameof(CreateByType))]
    public void Should_Create_File_System(FileSystemType fileSystemType, Type expectedType)
    {
        // Arrange
        var options = new FileSystemOptions
        {
            Type = fileSystemType,
            Directory = "dir"
        };

        // Assert
        var result = _factory.CreateFileSystem(options);

        // Assert
        result.Should().BeOfType(expectedType);
    }
}
