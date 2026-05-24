namespace AxonsFabricBackendPrototype;

public sealed class TraceabilityService
{
    private readonly ITraceabilityBlockchainClient _blockchainClient;

    public TraceabilityService(ITraceabilityBlockchainClient blockchainClient)
    {
        _blockchainClient = blockchainClient;
    }

    public Task<TraceabilityResponse> CreateRecordAsync(TraceabilityCreateRequest request, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(request.TraceabilityId))
        {
            throw new ArgumentException("TraceabilityId is required.", nameof(request));
        }

        return _blockchainClient.CreateRecordAsync(request, cancellationToken);
    }

    public Task<TraceabilityResponse?> GetRecordAsync(string traceabilityId, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(traceabilityId))
        {
            throw new ArgumentException("traceabilityId is required.", nameof(traceabilityId));
        }

        return _blockchainClient.GetRecordAsync(traceabilityId, cancellationToken);
    }

    public Task<TraceabilityVerificationResult> VerifyAsync(string traceabilityId, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(traceabilityId))
        {
            throw new ArgumentException("traceabilityId is required.", nameof(traceabilityId));
        }

        return _blockchainClient.VerifyAsync(traceabilityId, cancellationToken);
    }
}
