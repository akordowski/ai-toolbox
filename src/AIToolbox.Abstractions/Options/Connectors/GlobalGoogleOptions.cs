namespace AIToolbox.Options.Connectors;

public sealed class GlobalGoogleOptions
{
    public string ApiKey { get; set; } = default!;
    public GoogleAIVersion ApiVersion { get; set; } = GoogleAIVersion.V1Beta;
    public string? ServiceId { get; set; }
}
