namespace AIToolbox.Options.KernelMemory;

public sealed class RabbitMqOptions
{
    public string Host { get; set; } = default!;
    public int Port { get; set; }
    public string Username { get; set; } = default!;
    public string Password { get; set; } = default!;
    public string VirtualHost { get; set; } = "/";
    public int MessageTtlSecs { get; set; } = 3600;
}
