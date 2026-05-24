using Microsoft.Extensions.Options;

namespace AxonsFabricBackendPrototype;

public sealed class FabricBlockchainClient : ITraceabilityBlockchainClient
{
    private readonly FabricOptions _options;
    private readonly ILogger<FabricBlockchainClient> _logger;

    public FabricBlockchainClient(IOptions<FabricOptions> options, ILogger<FabricBlockchainClient> logger)
    {
        _options = options.Value;
        _logger = logger;
    }

    public Task<TraceabilityResponse> CreateRecordAsync(TraceabilityCreateRequest request, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        if (string.IsNullOrWhiteSpace(_options.GatewayProfilePath))
        {
            throw new InvalidOperationException("Fabric gateway profile path is not configured.");
        }

        _logger.LogInformation(
            "Simulating Fabric write for traceability {TraceabilityId} on channel {ChannelName} using chaincode {ChaincodeName}.",
            request.TraceabilityId,
            _options.ChannelName,
            _options.ChaincodeName);

        var txId = Guid.NewGuid().ToString("N");
        var offChainId = $"meta-{request.TraceabilityId}";

        return Task.FromResult(new TraceabilityResponse
        {
            TraceabilityId = request.TraceabilityId,
            Status = "Committed",
            FabricTxId = txId,
            OffChainId = offChainId
        });
    }

    public Task<TraceabilityResponse?> GetRecordAsync(string traceabilityId, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        _logger.LogInformation("Simulating Fabric query for traceability {TraceabilityId}.", traceabilityId);

        return Task.FromResult<TraceabilityResponse?>(new TraceabilityResponse
        {
            TraceabilityId = traceabilityId,
            Status = "Queried",
            FabricTxId = Guid.NewGuid().ToString("N"),
            OffChainId = $"meta-{traceabilityId}"
        });
    }

    public Task<TraceabilityVerificationResult> VerifyAsync(string traceabilityId, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        _logger.LogInformation("Simulating Fabric verification for traceability {TraceabilityId}.", traceabilityId);

        return Task.FromResult(new TraceabilityVerificationResult
        {
            TraceabilityId = traceabilityId,
            VerificationStatus = "Verified",
            ProofSummary = "Fabric proof resolved and metadata matched.",
            ChaincodeFunctions = ["CreateTraceabilityRecord", "QueryTraceabilityRecord"]
        });
    }
}
