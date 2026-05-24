# Phase 1 — Local Fabric Network Prototype

## Purpose

This document defines a practical local infrastructure prototype for running a first Hyperledger Fabric network for AXONS Food Traceability.

## Prototype Goal

Build a **local, reproducible Fabric environment** that demonstrates:

- organization identities
- peer nodes
- orderer service
- one shared channel
- initial chaincode deployment
- basic application connectivity

## Prototype Scope

### MVP Scope

Start with a minimal local environment that proves the architecture:

1. **Orderer organization**
2. **FIT organization**
3. **Farm organization**
4. **Retail organization**

This MVP is enough to validate:

- CA-based identity issuance
- peer enrollment
- channel creation
- chaincode installation and approval
- basic transaction flow

### Expansion Scope

After the MVP succeeds, expand to:

- Feed organization
- Slaughter organization
- Further Processing organization
- Regulator organization

## Prototype Topology

```text
Orderer Org
  └── Orderer Node

FIT Org
  └── Peer

Farm Org
  └── Peer

Retail Org
  └── Peer

Shared Channel
  └── TraceabilityChannel

Chaincode
  └── Basic traceability contract
```

## Infrastructure Components

### Required Local Services

- Docker Desktop
- Docker Compose
- Hyperledger Fabric binaries
- Fabric CA binaries
- Optional: jq, curl, openssl

### Container / Service List

- Orderer
- CA for each organization
- Peer for each organization
- CLI container
- Optional: CouchDB for peer state database
- Optional: PostgreSQL for off-chain metadata
- Optional: API service stub

## Suggested Directory Layout

```text
phases/phase-1-foundation-setup/
├── README.md
├── docker/
│   ├── docker-compose.yaml
│   ├── config/
│   └── scripts/
├── chaincode/
│   └── traceability/
├── env/
│   └── .env.example
└── docs/
    ├── bootstrap.md
    └── validation-checklist.md
```

## Recommended Prototype Stack

### Local Blockchain Layer

- Hyperledger Fabric 2.x
- Fabric CA
- CouchDB
- Raft orderer

### Local Supporting Services

- PostgreSQL
- MinIO (optional for off-chain artifacts)
- RabbitMQ or SQS-style stub (optional for event flow)

## Minimal End-to-End Prototype Flow

1. Start Docker Compose infrastructure
2. Generate and enroll identities
3. Create the channel
4. Join peers to the channel
5. Install and approve chaincode
6. Commit chaincode definition
7. Invoke a basic traceability transaction
8. Query the ledger
9. Validate peer state

## Recommended First Chaincode Scope

The first chaincode should be intentionally small:

### Functions

- CreateTraceabilityRecord
- UpdateTraceabilityRecord
- QueryTraceabilityRecord

### Data Model

- traceabilityId
- productType
- ownerOrg
- parentTraceabilityId
- status
- createdAt
- hashOrReference

This is enough to prove:

- immutable write flow
- lineage linkage
- query by traceability ID

## Prototype Infrastructure Checklist

### Docker and Fabric Setup

- [ ] Docker Desktop installed
- [ ] Docker Compose available
- [ ] Fabric binaries installed
- [ ] Fabric CA binaries installed
- [ ] Network scripts prepared

### Identity and Network Setup

- [ ] CA containers running
- [ ] Organization identities enrolled
- [ ] Orderer configured
- [ ] Channel created
- [ ] Peers joined

### Chaincode Setup

- [ ] Chaincode packaged
- [ ] Chaincode installed on peers
- [ ] Chaincode approved
- [ ] Chaincode committed

### Validation

- [ ] Basic transaction invoke succeeds
- [ ] Query returns expected state
- [ ] Peer logs show committed transaction

## Suggested Implementation Sequence

### Phase 1A — Infrastructure Setup

- Create Docker Compose file
- Create bootstrap scripts
- Add environment variables
- Start local network

### Phase 1B — Network Validation

- Verify CA endpoints
- Enroll identities
- Create channel
- Join peers

### Phase 1C — Chaincode Prototype

- Implement minimal chaincode
- Test invoke and query
- Confirm ledger state

### Phase 1D — Integration Prototype

- Add a simple API service
- Connect to Fabric Gateway SDK
- Store off-chain metadata in PostgreSQL
- Return traceability proof to client

## Prototype Decisions to Lock

### Recommended Decisions

- Use **one channel** for MVP
- Use **one peer per organization** for local prototype
- Use **CouchDB** for peer state database
- Keep **off-chain metadata** external to Fabric
- Keep **chaincode small** and domain-focused

### Deferred Decisions

- Private data collections
- Endorsement policies beyond MVP
- Multi-region deployment
- Full regulator-only access model
- Advanced monitoring stack

## Proposed Repository Structure

```text

blockchain/
  fabric-network/
    docker-compose.yaml
    scripts/
    config/
chaincode/
  traceability/
backend/
  prototypes/
    fabric-client/
infra/
  local-dev/
```

## Prototype Validation Commands

The following commands are a practical validation checklist for the local prototype:

```bash
# Verify Docker containers
docker ps

# Check peer logs
docker logs <peer-container>

# Query channel information
peer channel list

# Invoke chaincode
peer chaincode invoke -C traceabilitychannel -n traceabilitycc -c '{"Args":["CreateTraceabilityRecord","..."]}'

# Query chaincode
peer chaincode query -C traceabilitychannel -n traceabilitycc -c '{"Args":["QueryTraceabilityRecord","..."]}'
```

## Phase 1 Implementation Status

The first Phase 1 infrastructure bootstrap is now implemented in the Docker folder:

- local Docker Compose for Orderer, CA, Peer, CouchDB, and CLI
- Fabric crypto and config generation templates
- bootstrap script that creates genesis block, channel transaction, and joins the peer

### Run Phase 1 now

```bash
docker compose -f phases/phase-1-foundation-setup/docker/docker-compose.local-fabric.yaml up -d
docker compose -f phases/phase-1-foundation-setup/docker/docker-compose.local-fabric.yaml exec cli sh /scripts/bootstrap-local-fabric.sh
```

### Verify Phase 1

```bash
docker compose -f phases/phase-1-foundation-setup/docker/docker-compose.local-fabric.yaml exec cli peer channel list
```

A successful run shows `traceabilitychannel` in the joined channels output.

## Suggested Prototype Deliverables

- Local Fabric network up and running
- Identity issuance working
- Channel created and peers joined
- Chaincode deployed and tested
- Basic traceability transaction working
- Minimal API integration proof

## Recommended Next Artifact

The next document to create after this README should be:

- `docker/docker-compose.yaml`
- `docker/scripts/bootstrap.sh`
- `chaincode/traceability/chaincode.go` or equivalent
- `docs/validation-checklist.md`

## Notes

This phase-1 document should be treated as the **prototype foundation**, not the final production architecture.

The goal is to prove the local architecture before moving to a production-ready infrastructure design.


📐 Recommended prototype phases
Phase 1 — Infrastructure foundation
Create the containerized Fabric components:

Orderer
CA
Peer
CouchDB
CLI
shared Docker network
Phase 2 — Organization and identity
Set up:

Org1 MSP
Org2 MSP
admin users
application users
TLS certificates
Phase 3 — Channel creation
Create and manage:

channel genesis
channel config
org anchor peers
channel join operations
Phase 4 — Peer and chaincode
Bring in:

peer lifecycle
chaincode package
install
approve
commit
invoke/query
Phase 5 — Backend integration
Then connect the backend to:

real connection profile
real user identity
real channel name
real chaincode name
