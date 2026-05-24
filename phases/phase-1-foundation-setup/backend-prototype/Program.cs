using AxonsFabricBackendPrototype;
using Microsoft.AspNetCore.StaticFiles;

var builder = WebApplication.CreateBuilder(args);

var runtimeUiPath = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "ui"));
var sourceUiPath = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "..", "..", "phase-5-frontend-applications", "ui"));
var frontendPath = Directory.Exists(runtimeUiPath) ? runtimeUiPath : sourceUiPath;
var contentTypeProvider = new FileExtensionContentTypeProvider();

builder.Services.Configure<FabricOptions>(builder.Configuration.GetSection(FabricOptions.SectionName));
builder.Services.AddSingleton<ITraceabilityBlockchainClient, FabricBlockchainClient>();
builder.Services.AddSingleton<TraceabilityService>();
builder.Services.AddSingleton<InfrastructureDemoService>();

var app = builder.Build();

IResult ServeFrontendFile(string? relativePath)
{
    var safeRelativePath = string.IsNullOrWhiteSpace(relativePath) ? "index.html" : relativePath;
    var normalizedRelativePath = safeRelativePath.Replace('\\', '/').TrimStart('/');
    var requestedPath = Path.GetFullPath(Path.Combine(frontendPath, normalizedRelativePath));

    if (!requestedPath.StartsWith(frontendPath, StringComparison.OrdinalIgnoreCase) || !File.Exists(requestedPath))
    {
        return Results.NotFound();
    }

    contentTypeProvider.TryGetContentType(Path.GetFileName(requestedPath), out var contentType);
    return Results.File(File.OpenRead(requestedPath), contentType ?? "application/octet-stream");
}

app.MapGet("/ui/{path?}", (string? path) => ServeFrontendFile(path));

app.MapGet("/", () => new
{
    service = "Axons Fabric Backend Prototype",
    status = "running"
});

app.MapPost("/traceability/records", async (TraceabilityCreateRequest request, TraceabilityService service, CancellationToken cancellationToken) =>
{
    var result = await service.CreateRecordAsync(request, cancellationToken);
    return Results.Ok(result);
});

app.MapGet("/traceability/records/{traceabilityId}", async (string traceabilityId, TraceabilityService service, CancellationToken cancellationToken) =>
{
    var result = await service.GetRecordAsync(traceabilityId, cancellationToken);

    return result is null
        ? Results.NotFound(new { traceabilityId, message = "Traceability record not found." })
        : Results.Ok(result);
});

app.MapPost("/traceability/verify/{traceabilityId}", async (string traceabilityId, TraceabilityService service, CancellationToken cancellationToken) =>
{
    var result = await service.VerifyAsync(traceabilityId, cancellationToken);
    return Results.Ok(result);
});

app.MapPost("/infrastructure/network", (InfrastructureNetworkRequest request, InfrastructureDemoService service) =>
    Results.Ok(service.CreateNetwork(request)));

app.MapGet("/infrastructure/network", (InfrastructureDemoService service) =>
    Results.Ok(service.GetNetwork() ?? new InfrastructureNetworkResponse()));

app.MapPost("/infrastructure/organizations", (InfrastructureOrganizationRequest request, InfrastructureDemoService service) =>
    Results.Ok(service.CreateOrganization(request)));

app.MapGet("/infrastructure/organizations", (InfrastructureDemoService service) =>
    Results.Ok(service.GetOrganizations()));

app.MapPost("/infrastructure/members", (InfrastructureMemberRequest request, InfrastructureDemoService service) =>
    Results.Ok(service.AddMember(request)));

app.MapGet("/infrastructure/members", (InfrastructureDemoService service) =>
    Results.Ok(service.GetMembers()));

app.Run();
