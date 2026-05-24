# AXONS Food Traceability Blockchain

Enterprise Food Traceability Platform powered by Hyperledger Fabric.

---

# Vision

AXONS Food Traceability Blockchain is a private enterprise blockchain platform designed for end-to-end food traceability across the supply chain.

The platform focuses on:

- Immutable traceability
- Supply chain transparency
- Batch lineage tracking
- QR verification
- Enterprise governance
- Auditability
- Food safety compliance

---

# Main Goal

Build a blockchain network owned and operated internally by AXONS instead of relying on third-party blockchain providers.

---

# Core Supply Chain Flow

```text
FIT → Feed → Farm → Slaughter → Further → Retail
```

Example flow:

```text
Feed Ingredient
    ↓
Animal Feed Production
    ↓
Pig Farm
    ↓
Slaughterhouse
    ↓
Further Processing
    ↓
Retail / Supermarket
```

---

# Technology Stack

| Layer | Technology |
|---|---|
| Blockchain | Hyperledger Fabric |
| Smart Contract | Fabric Chaincode |
| Backend API | ASP.NET Core |
| Database | PostgreSQL |
| Search Engine | Elasticsearch |
| Messaging | RabbitMQ / AWS SQS |
| Object Storage | MinIO / S3 |
| Frontend | React / Flet |
| QR Verification | QR + Hash Verification |
| Containerization | Docker |
| Future Orchestration | Kubernetes |

---

# System Architecture

```text
Frontend
   ↓
ASP.NET Core APIs
   ↓
Fabric Gateway SDK
   ↓
Hyperledger Fabric Network
   ↓
Peer Nodes / Ledger
```

---

# Blockchain Network Architecture

## Organizations

Planned organizations:

| Organization | Responsibility |
|---|---|
| FIT | Feed Ingredient |
| FEED | Feed Production |
| FARM | Pig Farming |
| SLAUGHTER | Slaughterhouse |
| FURTHER | Further Processing |
| RETAIL | Retail / Supermarket |

---

# Blockchain Components

Planned Fabric components:

- Orderer Node
- Peer Nodes
- Certificate Authorities
- Channels
- Smart Contracts (Chaincode)
- MSP (Membership Service Provider)

---

# Asset Types

Main blockchain assets:

## FeedBatch

Represents feed ingredient or feed production batch.

## AnimalLot

Represents pig lots raised in farms.

## CarcassBatch

Represents slaughtered carcass batches.

## RetailPackage

Represents packaged retail food products.

---

# Core Blockchain Transactions

## CreateFeedBatch()

Create feed production batch.

---

## TransferOwnership()

Transfer ownership between organizations.

Example:

```text
Feed → Farm
Farm → Slaughter
```

---

## LinkAnimalFeed()

Link feed batches consumed by animal lots.

---

## TransformAnimalToCarcass()

Convert animal lot into carcass batch.

---

## CreateRetailPackage()

Convert carcass into retail package.

---

## QueryTraceability()

Query complete product lineage and traceability history.

---

# QR Traceability

Each retail package will contain:

- QR Code
- Traceability ID
- Blockchain Verification Hash

Consumers can scan QR codes to verify:

- Feed source
- Farm source
- Slaughter information
- Product lineage
- Certifications
- Blockchain verification

---

# Development Roadmap

---

# Phase 0 — Architecture & Design

Estimated Time:
1 Week

## Goals

- Define blockchain topology
- Define organization structure
- Define asset schema
- Define smart contract design
- Define QR strategy
- Define transaction flow

## Deliverables

- Architecture diagram
- Blockchain topology
- Asset model
- Transaction model
- Traceability design

---

# Phase 1 — Local Fabric Network

Estimated Time:
1–2 Weeks

## Goals

Build first local Hyperledger Fabric network.

## Tasks

- Install Docker
- Install Hyperledger Fabric binaries
- Setup Fabric CA
- Create organizations
- Create peer nodes
- Create orderer
- Create genesis block
- Create channel
- Deploy first chaincode

## Deliverables

- Working blockchain network
- Peer nodes
- Orderer node
- First deployed smart contract

---

# Phase 2 — Smart Contract Development

Estimated Time:
2–3 Weeks

## Goals

Build blockchain business logic.

## Tasks

- Implement FeedBatch asset
- Implement AnimalLot asset
- Implement CarcassBatch asset
- Implement RetailPackage asset
- Implement transaction functions
- Implement lineage queries
- Implement ownership transfers

## Deliverables

- Production-ready chaincode prototype
- Immutable traceability logic

---

# Phase 3 — ASP.NET Core Integration

Estimated Time:
2 Weeks

## Goals

Integrate blockchain into backend APIs.

## Tasks

- Fabric Gateway SDK integration
- Blockchain service layer
- Transaction APIs
- Query APIs
- Event listeners
- PostgreSQL synchronization

## Deliverables

- Connected backend APIs
- Blockchain integration layer

---

# Phase 4 — QR Traceability System

Estimated Time:
1 Week

## Goals

Build consumer traceability verification.

## Tasks

- Generate QR codes
- Traceability API
- Lineage visualization
- Verification endpoint
- Blockchain proof verification

## Deliverables

- End-to-end traceability demo

---

# Phase 5 — Consortium Governance

Estimated Time:
2–3 Weeks

## Goals

Expand network governance and enterprise features.

## Tasks

- Multi-peer architecture
- Endorsement policies
- Identity roles
- Private data collections
- Access control policies

## Deliverables

- Enterprise consortium blockchain

---

# Phase 6 — Production Infrastructure

Estimated Time:
3–6 Weeks

## Goals

Prepare production deployment architecture.

## Tasks

- Kubernetes deployment
- Monitoring
- Logging
- CI/CD
- Secret management
- Backup strategy
- High availability setup

## Deliverables

- Production-ready infrastructure

---

# Current Progress Tracking

```text
[x] Phase 0 — Architecture & Design
[x] Phase 1 — Local Fabric Network
[ ] Phase 2 — Smart Contract Development
[ ] Phase 3 — ASP.NET Core Integration
[ ] Phase 4 — QR Traceability System
[ ] Phase 5 — Consortium Governance
[ ] Phase 6 — Production Infrastructure
```

## What we have done

- Completed the architecture and design work in the Phase 0 docs, including blockchain topology, governance, and traceability flow.
- Built a Phase 1 backend prototype in ASP.NET Core with Docker support.
- Added a guided frontend prototype that demonstrates network, organization, and member setup in a no-code style.
- Confirmed the backend can serve the UI and demo endpoints locally, and the frontend can target the backend when opened directly from disk.
- Added a local Docker-based Fabric bootstrap path with configuration and documentation support.

## What we are doing next

- Connect the prototype to real Fabric CA enrollment and identity flow.
- Replace the in-memory infrastructure demo state with real Fabric-backed operations.
- Build the next stage of chaincode and transaction lifecycle support.
- Continue the backend integration path so the UI can move from demo state to real blockchain behavior.

## Practical next milestone

1. Finish real Fabric identity and enrollment flow.
2. Add chaincode package/install/approve/commit workflow.
3. Wire the backend to Fabric Gateway operations.
4. Keep the frontend demo intact while it grows into a real operational console.

---

# Project Structure (Planned)

```text
axons_food_traceability/
│
├── blockchain/
│   ├── fabric-network/
│   ├── organizations/
│   ├── chaincode/
│   └── scripts/
│
├── backend/
│   ├── api/
│   ├── services/
│   ├── infrastructure/
│   └── integrations/
│
├── frontend/
│
├── qr-system/
│
├── documents/
│
├── infrastructure/
│
└── README.md
```

---

# Future Features

Planned future capabilities:

- IoT temperature tracking
- AI anomaly detection
- Carbon footprint tracking
- Export certification
- Government audit portal
- Mobile traceability application
- GS1 compliance
- Supply chain analytics

---

# Long-Term Goal

Become an internally operated blockchain platform provider for enterprise food traceability and supply chain verification.

