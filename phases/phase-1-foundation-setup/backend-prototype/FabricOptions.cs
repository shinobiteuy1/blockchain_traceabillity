namespace AxonsFabricBackendPrototype;

public sealed class FabricOptions
{
    public const string SectionName = "Fabric";

    public string GatewayProfilePath { get; set; } = "./config/local/connection-profile.json";
    public string ChannelName { get; set; } = "traceabilitychannel";
    public string ChaincodeName { get; set; } = "traceabilitycc";
    public string OrgMspId { get; set; } = "Org1MSP";
    public string UserName { get; set; } = "appUser";
    public bool UseLocalDockerNetwork { get; set; } = true;
    public string ApiBaseUrl { get; set; } = "http://localhost:5000";
}
