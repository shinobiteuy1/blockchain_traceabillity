namespace AxonsFabricBackendPrototype;

public sealed class InfrastructureNetworkRequest
{
    public string NetworkName { get; set; } = "AXONS Local Fabric";
    public string ChannelName { get; set; } = "traceabilitychannel";
    public string OrdererHost { get; set; } = "orderer.example.com";
    public int PeerCount { get; set; } = 1;
    public string Environment { get; set; } = "Local Docker";
}

public sealed class InfrastructureNetworkResponse
{
    public string NetworkId { get; set; } = string.Empty;
    public string NetworkName { get; set; } = string.Empty;
    public string ChannelName { get; set; } = string.Empty;
    public string OrdererHost { get; set; } = string.Empty;
    public int PeerCount { get; set; }
    public string Environment { get; set; } = string.Empty;
    public string Status { get; set; } = "Configured";
    public DateTimeOffset CreatedAt { get; set; }
    public List<string> SetupSteps { get; set; } = [];
}

public sealed class InfrastructureOrganizationRequest
{
    public string OrganizationName { get; set; } = string.Empty;
    public string OrganizationType { get; set; } = "Producer";
    public string AdminName { get; set; } = string.Empty;
    public string AdminEmail { get; set; } = string.Empty;
}

public sealed class InfrastructureOrganizationResponse
{
    public string OrganizationId { get; set; } = string.Empty;
    public string OrganizationName { get; set; } = string.Empty;
    public string OrganizationType { get; set; } = string.Empty;
    public string AdminName { get; set; } = string.Empty;
    public string AdminEmail { get; set; } = string.Empty;
    public DateTimeOffset CreatedAt { get; set; }
    public string Readiness { get; set; } = "Ready for channel join";
}

public sealed class InfrastructureMemberRequest
{
    public string OrganizationId { get; set; } = string.Empty;
    public string MemberName { get; set; } = string.Empty;
    public string MemberRole { get; set; } = "Operator";
    public string MemberEmail { get; set; } = string.Empty;
}

public sealed class InfrastructureMemberResponse
{
    public string MemberId { get; set; } = string.Empty;
    public string OrganizationId { get; set; } = string.Empty;
    public string MemberName { get; set; } = string.Empty;
    public string MemberRole { get; set; } = string.Empty;
    public string MemberEmail { get; set; } = string.Empty;
    public DateTimeOffset AddedAt { get; set; }
    public string Status { get; set; } = "Pending onboarding";
}

public sealed class InfrastructureDemoService
{
    private readonly Dictionary<string, InfrastructureNetworkResponse> _networks = new(StringComparer.OrdinalIgnoreCase);
    private readonly Dictionary<string, InfrastructureOrganizationResponse> _organizations = new(StringComparer.OrdinalIgnoreCase);
    private readonly Dictionary<string, InfrastructureMemberResponse> _members = new(StringComparer.OrdinalIgnoreCase);

    public InfrastructureNetworkResponse CreateNetwork(InfrastructureNetworkRequest request)
    {
        var network = new InfrastructureNetworkResponse
        {
            NetworkId = Guid.NewGuid().ToString("N"),
            NetworkName = string.IsNullOrWhiteSpace(request.NetworkName) ? "AXONS Local Fabric" : request.NetworkName,
            ChannelName = string.IsNullOrWhiteSpace(request.ChannelName) ? "traceabilitychannel" : request.ChannelName,
            OrdererHost = string.IsNullOrWhiteSpace(request.OrdererHost) ? "orderer.example.com" : request.OrdererHost,
            PeerCount = request.PeerCount <= 0 ? 1 : request.PeerCount,
            Environment = string.IsNullOrWhiteSpace(request.Environment) ? "Local Docker" : request.Environment,
            CreatedAt = DateTimeOffset.UtcNow,
            Status = "Configured",
            SetupSteps =
            [
                "Start Docker and local Fabric containers",
                "Generate ordering and peer identities",
                $"Create channel {request.ChannelName}",
                "Join each peer to the channel",
                "Approve and commit the first chaincode package"
            ]
        };

        _networks[network.NetworkId] = network;
        return network;
    }

    public InfrastructureNetworkResponse? GetNetwork()
    {
        if (_networks.Count == 0)
        {
            return null;
        }

        return _networks.OrderByDescending(x => x.Value.CreatedAt)
            .First().Value;
    }

    public InfrastructureOrganizationResponse CreateOrganization(InfrastructureOrganizationRequest request)
    {
        var organization = new InfrastructureOrganizationResponse
        {
            OrganizationId = Guid.NewGuid().ToString("N"),
            OrganizationName = string.IsNullOrWhiteSpace(request.OrganizationName) ? "New Organization" : request.OrganizationName,
            OrganizationType = string.IsNullOrWhiteSpace(request.OrganizationType) ? "Producer" : request.OrganizationType,
            AdminName = string.IsNullOrWhiteSpace(request.AdminName) ? "Org Admin" : request.AdminName,
            AdminEmail = string.IsNullOrWhiteSpace(request.AdminEmail) ? "admin@example.com" : request.AdminEmail,
            CreatedAt = DateTimeOffset.UtcNow,
            Readiness = "Ready for channel join"
        };

        _organizations[organization.OrganizationId] = organization;
        return organization;
    }

    public IReadOnlyCollection<InfrastructureOrganizationResponse> GetOrganizations()
    {
        return _organizations.Values
            .OrderByDescending(x => x.CreatedAt)
            .ToList();
    }

    public InfrastructureMemberResponse AddMember(InfrastructureMemberRequest request)
    {
        if (!_organizations.ContainsKey(request.OrganizationId))
        {
            throw new InvalidOperationException("Organization not found.");
        }

        var member = new InfrastructureMemberResponse
        {
            MemberId = Guid.NewGuid().ToString("N"),
            OrganizationId = request.OrganizationId,
            MemberName = string.IsNullOrWhiteSpace(request.MemberName) ? "New Member" : request.MemberName,
            MemberRole = string.IsNullOrWhiteSpace(request.MemberRole) ? "Operator" : request.MemberRole,
            MemberEmail = string.IsNullOrWhiteSpace(request.MemberEmail) ? "member@example.com" : request.MemberEmail,
            AddedAt = DateTimeOffset.UtcNow,
            Status = "Pending onboarding"
        };

        _members[member.MemberId] = member;
        return member;
    }

    public IReadOnlyCollection<InfrastructureMemberResponse> GetMembers()
    {
        return _members.Values
            .OrderByDescending(x => x.AddedAt)
            .ToList();
    }
}
