# Axons Fabric Backend Prototype

## Purpose

This project is the **backend-first prototype** for the AXONS Food Traceability platform.

The current goal is to provide a **clean service boundary** that can talk to a local Hyperledger Fabric network while keeping the business logic and Fabric integration isolated.

## Current Capabilities

### Exposed endpoints

- `GET /`
  - returns basic service status
- `POST /traceability/records`
  - creates a traceability record request
- `GET /traceability/records/{traceabilityId}`
  - retrieves a traceability record response
- `POST /traceability/verify/{traceabilityId}`
  - returns a verification result

### Current implementation style

- the backend uses a **service layer** for traceability operations
- the Fabric dependency is isolated behind `ITraceabilityBlockchainClient`
- the current client is a **simulation layer** and is not yet connected to the real Fabric Gateway SDK

## Project Layout

- `Program.cs` — API endpoints and dependency registration
- `FabricOptions.cs` — configuration model for local Fabric settings
- `TraceabilityModels.cs` — request / response DTOs
- `ITraceabilityBlockchainClient.cs` — abstraction for blockchain interaction
- `FabricBlockchainClient.cs` — current Fabric adapter implementation
- `TraceabilityService.cs` — business service orchestration
- `config/local/connection-profile.json` — placeholder connection profile for local Fabric

## Configuration

The prototype reads Fabric settings from `appsettings.json` and `appsettings.Development.json`.

Current Fabric-related settings include:

- `GatewayProfilePath`
- `ChannelName`
- `ChaincodeName`
- `OrgMspId`
- `UserName`
- `UseLocalDockerNetwork`
- `ApiBaseUrl`

## Current Status

### Implemented

- ASP.NET Core web skeleton
- service layer for traceability operations
- endpoint definitions
- configuration model
- placeholder Fabric adapter
- local connection profile file

### Not yet implemented

- real Fabric Gateway SDK integration
- real chaincode invocation
- real query implementation
- real identity enrollment
- real local Fabric runtime validation

## Run Locally

```bash
dotnet run --project phases/phase-1-foundation-setup/backend-prototype/AxonsFabricBackendPrototype.csproj
```

## Build

```bash
dotnet build phases/phase-1-foundation-setup/backend-prototype/AxonsFabricBackendPrototype.csproj
```

## Docker Build

Build the container image from the repository root:

```bash
docker build -f phases/phase-1-foundation-setup/backend-prototype/Dockerfile -t axons-backend .
```

Run the container locally:

```bash
docker run --rm -p 5000:8080 --name axons-backend axons-backend
```

Run with Docker Compose from the docker folder:

```bash
docker compose -f phases/phase-1-foundation-setup/docker/docker-compose.backend.yaml up --build
```

Stop and remove the container:

```bash
docker stop axons-backend
```

## Example Requests

### Create traceability record

```bash
curl -X POST http://localhost:5257/traceability/records \
  -H "Content-Type: application/json" \
  -d '{
    "traceabilityId": "TR-0001",
    "productType": "RetailPackage",
    "ownerOrg": "Retail",
    "parentTraceabilityId": "TR-0000",
    "status": "Created",
    "metadata": {
      "batchCode": "BATCH-001",
      "location": "Bangkok"
    }
  }'
```

### Get traceability record

```bash
curl http://localhost:5000/traceability/records/TR-0001
```

### Verify traceability

```bash
curl -X POST http://localhost:5000/traceability/verify/TR-0001
```

## Next Steps

1. Replace `FabricBlockchainClient` with a real Fabric Gateway SDK implementation.
2. Add a chaincode starter for the local network.
3. Add Docker Compose wiring for the local Fabric environment.
4. Add bootstrap scripts for CA, channel, and chaincode setup.
5. Add integration tests against the local network.

## Design Goal

This prototype is intentionally focused on **backend service readiness** first, so that the Fabric integration can evolve without rewriting the application boundary.
