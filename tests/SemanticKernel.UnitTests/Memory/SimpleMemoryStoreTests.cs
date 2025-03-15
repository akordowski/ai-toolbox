using AIToolbox.IO;
using FluentAssertions;
using Microsoft.SemanticKernel.Memory;

namespace AIToolbox.SemanticKernel.Memory;

public class SimpleMemoryStoreTests
{
    private static readonly Type TypeArgumentException = typeof(ArgumentException);
    private static readonly Type TypeArgumentNullException = typeof(ArgumentNullException);

    private const string CollectionName = "TestCollection";

    private readonly SimpleMemoryStore _memoryStore = new(new VolatileFileSystem());
    private readonly float[] _embedding = [0.1f, 0.2f, 0.3f];

    public static TheoryData<Func<Task>, string, Type> InvokeMethodWithInvalidParameters
    {
        get
        {
            var memoryStore = new SimpleMemoryStore(new VolatileFileSystem());

            return new TheoryData<Func<Task>, string, Type>
            {
                { () => memoryStore.CreateCollectionAsync(null!), "collectionName", TypeArgumentNullException },
                { () => memoryStore.CreateCollectionAsync(""), "collectionName", TypeArgumentException },
                { () => memoryStore.CreateCollectionAsync(" "), "collectionName", TypeArgumentException },

                { () => memoryStore.DeleteCollectionAsync(null!), "collectionName", TypeArgumentNullException },
                { () => memoryStore.DeleteCollectionAsync(""), "collectionName", TypeArgumentException },
                { () => memoryStore.DeleteCollectionAsync(" "), "collectionName", TypeArgumentException },

                { () => memoryStore.DoesCollectionExistAsync(null!), "collectionName", TypeArgumentNullException },
                { () => memoryStore.DoesCollectionExistAsync(""), "collectionName", TypeArgumentException },
                { () => memoryStore.DoesCollectionExistAsync(" "), "collectionName", TypeArgumentException },

                { () => memoryStore.GetAsync(null!, null!), "collectionName", TypeArgumentNullException },
                { () => memoryStore.GetAsync("", null!), "collectionName", TypeArgumentException },
                { () => memoryStore.GetAsync(" ", null!), "collectionName", TypeArgumentException },
                { () => memoryStore.GetAsync("Collection", null!), "key", TypeArgumentNullException },
                { () => memoryStore.GetAsync("Collection", ""), "key", TypeArgumentException },
                { () => memoryStore.GetAsync("Collection", " "), "key", TypeArgumentException },

                { () => AsyncEnumerableExtensions.ToListAsync(memoryStore.GetBatchAsync(null!, null!)), "collectionName", TypeArgumentNullException },
                { () => AsyncEnumerableExtensions.ToListAsync(memoryStore.GetBatchAsync("", null!)), "collectionName", TypeArgumentException },
                { () => AsyncEnumerableExtensions.ToListAsync(memoryStore.GetBatchAsync(" ", null!)), "collectionName", TypeArgumentException },
                { () => AsyncEnumerableExtensions.ToListAsync(memoryStore.GetBatchAsync("Collection", null!)), "keys", TypeArgumentNullException },

                { () => memoryStore.GetNearestMatchAsync(null!, ReadOnlyMemory<float>.Empty), "collectionName", TypeArgumentNullException },
                { () => memoryStore.GetNearestMatchAsync("", ReadOnlyMemory<float>.Empty), "collectionName", TypeArgumentException },
                { () => memoryStore.GetNearestMatchAsync(" ", ReadOnlyMemory<float>.Empty), "collectionName", TypeArgumentException },

                { () => AsyncEnumerableExtensions.ToListAsync(memoryStore.GetNearestMatchesAsync(null!, ReadOnlyMemory<float>.Empty, 10)), "collectionName", TypeArgumentNullException },
                { () => AsyncEnumerableExtensions.ToListAsync(memoryStore.GetNearestMatchesAsync("", ReadOnlyMemory<float>.Empty, 10)), "collectionName", TypeArgumentException },
                { () => AsyncEnumerableExtensions.ToListAsync(memoryStore.GetNearestMatchesAsync(" ", ReadOnlyMemory<float>.Empty, 10)), "collectionName", TypeArgumentException },

                { () => memoryStore.RemoveAsync(null!, null!), "collectionName", TypeArgumentNullException },
                { () => memoryStore.RemoveAsync("", null!), "collectionName", TypeArgumentException },
                { () => memoryStore.RemoveAsync(" ", null!), "collectionName", TypeArgumentException },
                { () => memoryStore.RemoveAsync("Collection", null!), "key", TypeArgumentNullException },
                { () => memoryStore.RemoveAsync("Collection", ""), "key", TypeArgumentException },
                { () => memoryStore.RemoveAsync("Collection", " "), "key", TypeArgumentException },

                { () => memoryStore.RemoveBatchAsync(null!, null!), "collectionName", TypeArgumentNullException },
                { () => memoryStore.RemoveBatchAsync("", null!), "collectionName", TypeArgumentException },
                { () => memoryStore.RemoveBatchAsync(" ", null!), "collectionName", TypeArgumentException },
                { () => memoryStore.RemoveBatchAsync("Collection", null!), "keys", TypeArgumentNullException },

                { () => memoryStore.UpsertAsync(null!, null!), "collectionName", TypeArgumentNullException },
                { () => memoryStore.UpsertAsync("", null!), "collectionName", TypeArgumentException },
                { () => memoryStore.UpsertAsync(" ", null!), "collectionName", TypeArgumentException },
                { () => memoryStore.UpsertAsync("Collection", null!), "record", TypeArgumentNullException },

                { () => AsyncEnumerableExtensions.ToListAsync(memoryStore.UpsertBatchAsync(null!, null!)), "collectionName", TypeArgumentNullException },
                { () => AsyncEnumerableExtensions.ToListAsync(memoryStore.UpsertBatchAsync("", null!)), "collectionName", TypeArgumentException },
                { () => AsyncEnumerableExtensions.ToListAsync(memoryStore.UpsertBatchAsync(" ", null!)), "collectionName", TypeArgumentException },
                { () => AsyncEnumerableExtensions.ToListAsync(memoryStore.UpsertBatchAsync("Collection", null!)), "records", TypeArgumentNullException }
            };
        }
    }

    [Fact]
    public void Should_Throw_On_Construct_With_Null_Parameters()
    {
        // Arrange
        var act = () => new SimpleMemoryStore(null!);

        // Assert
        act.Should().Throw<ArgumentNullException>().WithParameterName("fileSystem");
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

    [Fact]
    public async Task Should_Create_Collection()
    {
        // Act
        await _memoryStore.CreateCollectionAsync(CollectionName);

        // Assert
        var result = await _memoryStore.DoesCollectionExistAsync(CollectionName);
        result.Should().BeTrue();
    }

    [Fact]
    public async Task Should_Delete_Collection()
    {
        // Arrange
        await _memoryStore.CreateCollectionAsync(CollectionName);

        // Act
        await _memoryStore.DeleteCollectionAsync(CollectionName);

        // Assert
        var result = await _memoryStore.DoesCollectionExistAsync(CollectionName);
        result.Should().BeFalse();
    }

    [Fact]
    public async Task Should_Check_If_Collection_Exists()
    {
        // Arrange
        await _memoryStore.CreateCollectionAsync(CollectionName);

        // Act
        var result = await _memoryStore.DoesCollectionExistAsync(CollectionName);

        // Assert
        result.Should().BeTrue();
    }

    [Theory]
    [InlineData("id1", false)]
    [InlineData("id", true)]
    public async Task Should_Get_Record(string key, bool isNull)
    {
        // Arrange
        await AddRecordsAsync();

        // Act
        var result = await _memoryStore.GetAsync(CollectionName, key);

        // Assert
        if (isNull)
        {
            result.Should().BeNull();
        }
        else
        {
            result.Should().NotBeNull();
            result.Key.Should().Be(key);
        }
    }

    [Fact]
    public async Task Should_Get_Batch_Of_Records()
    {
        // Arrange
        await AddRecordsAsync();
        var keys = new[] { "id1", "id3" };

        // Act
        var result = new List<MemoryRecord>();

        await foreach (var record in _memoryStore.GetBatchAsync(CollectionName, keys))
        {
            result.Add(record);
        }

        // Assert
        result.Should().HaveCount(2);
        result.Should().ContainSingle(o => o.Key == keys[0]);
        result.Should().ContainSingle(o => o.Key == keys[1]);
    }

    [Fact]
    public async Task Should_Get_Collections()
    {
        // Arrange
        var collectionNames = new[] { "collection1", "collection2" };

        await AddRecordsAsync(collectionNames[0]);
        await AddRecordsAsync(collectionNames[1]);

        // Act
        var result = new List<string>();

        await foreach (var collection in _memoryStore.GetCollectionsAsync())
        {
            result.Add(collection);
        }

        // Assert
        result.Should().BeEquivalentTo(collectionNames);
    }

    [Fact]
    public async Task Should_Get_Nearest_Match()
    {
        // Arrange
        await AddRecordsAsync();

        // Act
        var result = await _memoryStore.GetNearestMatchAsync(CollectionName, _embedding);

        // Assert
        result.Should().NotBeNull();
        result.Value.Item1.Key.Should().Be("id1");
    }

    [Fact]
    public async Task Should_Get_Nearest_Matches()
    {
        // Arrange
        await AddRecordsAsync();

        // Act
        var result = new List<(MemoryRecord, double)>();

        await foreach (var match in _memoryStore.GetNearestMatchesAsync(CollectionName, _embedding, 10))
        {
            result.Add(match);
        }

        // Assert
        result.Should().HaveCount(3);
    }

    [Fact]
    public async Task Should_Remove_Record()
    {
        // Arrange
        await AddRecordsAsync();

        // Act
        await _memoryStore.RemoveAsync(CollectionName, "id1");

        // Assert
        var result = await _memoryStore.GetAsync(CollectionName, "id1");
        result.Should().BeNull();
    }

    [Fact]
    public async Task Should_Remove_Batch_Of_Records()
    {
        // Arrange
        await AddRecordsAsync();
        var keys = new List<string> { "id1", "id3" };

        // Act
        await _memoryStore.RemoveBatchAsync(CollectionName, keys);

        // Assert
        var result1 = await _memoryStore.GetAsync(CollectionName, "id1");
        var result2 = await _memoryStore.GetAsync(CollectionName, "id3");

        result1.Should().BeNull();
        result2.Should().BeNull();
    }

    [Fact]
    public async Task Should_Upsert_Record()
    {
        // Arrange
        var record = GetMemoryRecord();

        // Act
        var result = await _memoryStore.UpsertAsync(CollectionName, record);

        // Assert
        result.Should().Be(record.Metadata.Id);
    }

    [Fact]
    public async Task Should_Upsert_Batch_Of_Records()
    {
        // Arrange
        var records = new[] { GetMemoryRecord(1), GetMemoryRecord(2) };

        // Act
        var result = new List<string>();

        await foreach (var id in _memoryStore.UpsertBatchAsync(CollectionName, records))
        {
            result.Add(id);
        }

        // Assert
        result.Should().BeEquivalentTo("id1", "id2");
    }

    private async Task AddRecordsAsync(string? collection = null)
    {
        var records = GetMemoryRecords(3);
        await AsyncEnumerableExtensions.ToListAsync(_memoryStore.UpsertBatchAsync(collection ?? CollectionName, records));
    }

    private MemoryRecord GetMemoryRecord(int? num = null)
    {
        var metadata = new MemoryRecordMetadata(
            false,
            $"id{num}",
            $"text{num}",
            $"description{num}",
            $"externalSourceName{num}",
            $"additionalMetadata{num}");

        return new MemoryRecord(metadata, _embedding, $"key{num}");
    }

    private IEnumerable<MemoryRecord> GetMemoryRecords(int? count = 0)
    {
        for (var i = 0; i < count; i++)
        {
            yield return GetMemoryRecord(i + 1);
        }
    }
}
