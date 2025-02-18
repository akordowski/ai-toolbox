namespace AIToolbox;

internal static class Verify
{
    public static void ThrowIfOptionsNull<T>(T? options)
    {
        if (options is null)
        {
            throw new InvalidOperationException($"No '{typeof(T).Name}' provided.");
        }
    }
}
