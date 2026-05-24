# Backend Service Prototype — Local Fabric First

## Purpose

This document describes a **backend-first prototype** for AXONS Food Traceability where the initial deliverable is a **strong service layer** backed by a **local Hyperledger Fabric network**.

The goal is to prove:

- how the backend uses Fabric
- how the backend exposes traceability operations
- how the local network can be expanded later into a BaaS-style model

## Prototype Objective

Build the first prototype around the **service contract**, not the full infrastructure.

### Primary Goal

Create a backend that can:

1. create a traceability record
2. read traceability state
3. verify a product lineage
4. store off-chain metadata
5. expose clean APIs for future expansion

### Secondary Goal

Use a local Fabric network as the **initial backing service**, so the backend can later evolve into a **blockchain service abstraction**.

## Proposed Backend Shape

### Service Layers

1. **API Layer**
   - HTTP endpoints for traceability operations
   - authentication and request validation

2. **Application Service Layer**
   - business rules
   - orchestration
   - DTO mapping
   - event publication

3. **Blockchain Adapter Layer**
   - abstracts Fabric SDK usage
   - isolates chaincode invocation logic
   - supports future alternative providers or multi-network routing

4. **Off-chain Data Layer**
   - PostgreSQL for structured metadata
   - object storage for files and evidence

5. **Observability Layer**
   - logs
   - metrics
   - request tracing

## Recommended Backend Contract

### REST or Minimal API Endpoints

#### Create Traceability Record

- `POST /traceability/records`

#### Query Traceability Record

- `GET /traceability/records/{id}`

#### Verify Product

- `POST /traceability/verify`

#### Update Ownership / Stage

- `POST /traceability/transition`

## Example Request / Response Model

### Create Record

```json
{
  "traceabilityId": "TR-2026-0001",
  "productType": "RetailPackage",
  "ownerOrg": "Retail",
  "parentTraceabilityId": "TR-2026-0000",
  "status": "Created",
  "metadata": {
    "batchCode": "BATCH-001",
    "location": "Bangkok",
    "timestamp": "2026-05-23T10:00:00Z"
  }
}
```

### Response

```json
{
  "traceabilityId": "TR-2026-0001",
  "status": "Committed",
  "fabricTxId": "tx-123",
  "offChainId": "meta-456"
}
```

## Backend-to-Blockchain Mapping

### On-chain operations

- create immutable traceability event
- update lineage reference
- record ownership transition
- record transformation event
- record verification hash

### Off-chain operations

- store detailed shipment metadata
- store documents and evidence
- store images
- store operational notes

## Recommended Internal Modules

### 1. TraceabilityController

Handles HTTP requests and response mapping.

### 2. TraceabilityService

Coordinates business logic and high-level operations.

### 3. BlockchainClient

Encapsulates Fabric Gateway SDK calls.

### 4. MetadataStore

Encapsulates PostgreSQL and object storage access.

### 5. AuditLogger

Records request and transaction outcomes.

## Prototype Execution Flow

### Write Flow

1. API receives request
2. Service validates request
3. Service persists off-chain metadata
4. Service sends a minimal transaction to Fabric
5. Service stores returned transaction hash / receipt
6. API returns success response

### Read Flow

1. API receives query request
2. Service queries Fabric for the proof
3. Service reads off-chain metadata
4. Service composes the response
5. API returns the combined result

## Local Network Usage Strategy

### First Prototype

Use a **local Fabric network** as the backend backing environment.

#### Why this is good

- rapid iteration
- low cost
- easier debugging
- easy onboarding
- suitable for service validation

### Backend-Hardware Separation

The backend should be designed so the **Fabric dependency is only in one adapter layer**.

That means the rest of the service can evolve independently.

## How This Becomes a BaaS-style Backend

The backend can evolve into a BaaS-style service by introducing:

### 1. Tenant / Organization Routing

- route requests per organization
- select tenant-specific networks or channels

### 2. Network Configuration Service

- manage local dev network config
- manage staging network config
- manage production network config

### 3. Blockchain Adapter Registry

- local Fabric adapter
- future network adapter
- future multi-channel routing

### 4. Service Contracts

- standard create / query / verify endpoints
- contract-specific operations hidden behind adapter layer

## Prototype Expansion Path

### Phase A — Local Single Network

- one local Fabric network
- one channel
- one backend service
- minimal chaincode

### Phase B — Multi-Organization Local Network

- add FIT, Farm, Slaughter, Retail, Regulator organizations
- add more peers
- add endorsement policy

### Phase C — Backend as Platform Layer

- abstract network configuration
- add organization-aware routing
- add API versioning
- add tenant management

### Phase D — Production Expansion

- move from local Docker to managed or private deployment
- add secrets management
- add monitoring
- add CI/CD

## Suggested Service Boundary

### Backend Service Responsibilities

- validate request
- handle business orchestration
- call blockchain adapter
- persist off-chain metadata
- return traceability result

### Fabric Responsibilities

- immutable transaction proof
- lineage persistence
- consensus and ledger integrity

### UI Responsibilities

- consumer display
- operator dashboards
- QR scanning UX

## Prototype Deliverables

### Minimum Viable Backend Prototype

- API endpoints for create / query / verify
- Blockchain adapter for Fabric
- Off-chain metadata persistence
- Local Fabric network integration
- Basic request and transaction logging

### Expansion Outputs

- service abstraction for multiple networks
- organization routing concept
- readiness for BaaS service design

## Suggested Implementation Order

### 1. Define backend APIs

- create record
- query record
- verify record
- transition record

### 2. Create blockchain adapter abstraction

- FabricGatewayClient
- interface for chaincode operations

### 3. Connect local Fabric network

- local orderer
- local peers
- local channel

### 4. Add off-chain metadata store

- PostgreSQL
- object storage placeholder

### 5. Add observability

- request logs
- transaction logs
- error handling

## Recommended Principles for the Prototype

- keep the first backend **small and explicit**
- isolate Fabric behind an adapter
- do not couple business logic directly to Fabric SDK details
- store only proof and critical lineage on-chain
- keep detailed operational data off-chain

## Prototype Success Criteria

The prototype is successful when:

- the backend can create a traceability record
- the backend can query a record
- the backend can produce a verification result
- the backend can run against a local Fabric network
- the service boundary is clear enough to expand later

## Notes

This approach lets you prove **backend service behavior first**, while the local Fabric network remains the initial infrastructure proof. It reduces the risk of getting blocked by Fabric deployment details too early.

## Recommended Next Artifacts

1. `backend-service-prototype.md` (this document)
2. `docker/docker-compose.yaml`
3. `backend/project skeleton`
4. `chaincode prototype`
5. `validation checklist`
