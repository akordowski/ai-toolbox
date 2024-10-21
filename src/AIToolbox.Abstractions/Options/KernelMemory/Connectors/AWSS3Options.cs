namespace AIToolbox.Options.KernelMemory;

public sealed class AWSS3Options
{
    public string AccessKey { get; set; } = default!;
    public string SecretAccessKey { get; set; } = default!;
    public string Endpoint { get; set; } = "https://s3.amazonaws.com";
    public string BucketName { get; set; } = default!;
}
