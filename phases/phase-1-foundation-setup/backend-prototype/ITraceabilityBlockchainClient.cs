namespace AxonsFabricBackendPrototype;

public interface ITraceabilityBlockchainClient
{
    Task<TraceabilityResponse> CreateRecordAsync(TraceabilityCreateRequest request, CancellationToken cancellationToken = default);
    Task<TraceabilityResponse?> GetRecordAsync(string traceabilityId, CancellationToken cancellationToken = default);
    Task<TraceabilityVerificationResult> VerifyAsync(string traceabilityId, CancellationToken cancellationToken = default);
}
