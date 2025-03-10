namespace AIToolbox;

internal static class Verify
{
    public static void ThrowIfNotType<T>(object obj)
    {
        var type = obj.GetType();
        var targetType = typeof(T);

        if (type != targetType)
        {
            throw new InvalidOperationException($"The type '{type.Name}' is not of expected type '${targetType.Name}'.");
        }
    }

    public static void ThrowIfOptionsNull<T>(T? options)
    {
        if (options is null)
        {
            throw new InvalidOperationException($"No '{typeof(T).Name}' provided.");
        }
    }
}
