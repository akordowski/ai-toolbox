using AIToolbox.Options.Enums;

namespace AIToolbox.Options.Connectors;

public sealed class GlobalVertexAIOptions
{
    public string BearerKey { get; set; } = default!;
    public string Location { get; set; } = default!;
    public string ProjectId { get; set; } = default!;
    public VertexAIVersion ApiVersion { get; set; } = VertexAIVersion.V1;
    public string? ServiceId { get; set; }
}
