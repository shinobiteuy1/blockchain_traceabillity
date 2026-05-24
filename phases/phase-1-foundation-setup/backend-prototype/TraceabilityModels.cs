namespace AxonsFabricBackendPrototype;

public sealed class TraceabilityCreateRequest
{
    public string TraceabilityId { get; set; } = string.Empty;
    public string ProductType { get; set; } = string.Empty;
    public string OwnerOrg { get; set; } = string.Empty;
    public string? ParentTraceabilityId { get; set; }
    public string Status { get; set; } = "Created";
    public Dictionary<string, string> Metadata { get; set; } = new(StringComparer.OrdinalIgnoreCase);
}

public sealed class TraceabilityResponse
{
    public string TraceabilityId { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public string FabricTxId { get; set; } = string.Empty;
    public string OffChainId { get; set; } = string.Empty;
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
}

public sealed class TraceabilityVerificationResult
{
    public string TraceabilityId { get; set; } = string.Empty;
    public string VerificationStatus { get; set; } = "Pending";
    public string ProofSummary { get; set; } = string.Empty;
    public string[] ChaincodeFunctions { get; set; } = [];
}
