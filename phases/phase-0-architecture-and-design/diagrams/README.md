# Design Diagrams

## Purpose

This folder stores all architecture and design diagrams for the platform.

---

# Planned Diagram Categories

## Business Diagrams
- supply chain flow
- ownership transfer
- operational lifecycle

---

## Blockchain Diagrams
- network topology
- peer relationships
- smart contract boundaries

---

## System Diagrams
- microservices architecture
- event flow
- API interactions

---

## Infrastructure Diagrams
- Kubernetes deployment
- networking
- monitoring stack

---

## Data Diagrams
- lineage relationships
- entity relationships
- event sourcing flow

---

# Suggested Tools

- Draw.io
- Excalidraw
- Mermaid
- PlantUML

---

# Diagram Naming Convention

Examples:

```text
business-flow-v1.drawio
blockchain-network-v1.drawio
microservices-flow-v1.drawio



# DIAGRAM FLOW
Diagram sequence from your folder
From the diagram files you already created, the recommended sequence is:

Business process diagram

File: traceability-flow-v1.md
Purpose: show the end-to-end supply chain journey
Flow: FIT → Feed → Farm → Slaughter → Further Processing → Retail
Organization & governance diagram

File: organization-governance-v1.md
Purpose: show who participates, what each organization owns, and who can read or write
Includes: consortium, regulator, Fabric CA, RBAC, least privilege
Fabric network topology diagram

File: fabric-network-topology-v1.md
Purpose: show the Hyperledger Fabric architecture
Includes: orderer, peers, CAs, channel, contracts, application layer
Application + blockchain integration diagram

File: application-blockchain-integration-v1.md
Purpose: show how the app talks to blockchain
Includes: API Gateway, Application Service, Blockchain Service, Fabric Gateway SDK, off-chain DB, object storage, QR verification
Traceability transaction diagram

File: traceability-transaction-v1.md
Purpose: show the transaction-by-transaction lifecycle
Includes: create feed batch → assign farm → register animal lot → transform to carcass → create processed product → create retail package → QR verification
Data flow diagram

This is the final step I recommended
Purpose: show what data goes where, and clearly separate:
on-chain data
off-chain data
query and verification flow
