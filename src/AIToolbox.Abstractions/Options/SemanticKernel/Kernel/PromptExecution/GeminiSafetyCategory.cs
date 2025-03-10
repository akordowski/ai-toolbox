namespace AIToolbox.Options.SemanticKernel;

public enum GeminiSafetyCategory
{
    /// <summary>
    /// Includes content that promotes, facilitates, or encourages harmful acts.
    /// </summary>
    Dangerous,

    /// <summary>
    /// Contains dangerous content.
    /// </summary>
    DangerousContent,

    /// <summary>
    /// Contains negative or harmful comments targeting identity and/or protected attributes.
    /// </summary>
    Derogatory,

    /// <summary>
    /// Consists of harassment content.
    /// </summary>
    Harassment,

    /// <summary>
    /// Contains unchecked medical advice.
    /// </summary>
    Medical,

    /// <summary>
    /// Contains references to sexual acts or other lewd content.
    /// </summary>
    Sexual,

    /// <summary>
    /// Contains sexually explicit content.
    /// </summary>
    SexuallyExplicit,

    /// <summary>
    /// Includes content that is rude, disrespectful, or profane.
    /// </summary>
    Toxicity,

    /// <summary>
    /// Category is unspecified.
    /// </summary>
    Unspecified,

    /// <summary>
    /// Describes scenarios depicting violence against an individual or group, or general descriptions of gore.
    /// </summary>
    Violence
}
