# Application + Blockchain Integration V1

## Purpose

This document describes the initial application-to-blockchain integration flow for the AXONS food traceability platform.

## Scope

This diagram focuses on the interaction between:

- client applications
- API gateway
- application services
- blockchain service layer
- Hyperledger Fabric peers
- off-chain storage
- QR verification flow

## Mermaid Diagram

```mermaid
flowchart TD
    A[Client / Web / Mobile / Internal App] --> B[API Gateway]
    B --> C[Identity & Auth]
    C --> D[Application Service]

    D --> E[Business Validation]
    E --> F[Event Publisher]
    F --> G[Message Broker]

    G --> H[Blockchain Service]
    H --> I[Fabric Gateway SDK]
    I --> J[FIT Peer]
    I --> K[Feed Peer]
    I --> L[Farm Peer]
    I --> M[Slaughter Peer]
    I --> N[Further Processing Peer]
    I --> O[Retail Peer]
    I --> P[Regulator Peer]

    J --> Q[Traceability Channel]
    K --> Q
    L --> Q
    M --> Q
    N --> Q
    O --> Q
    P --> Q

    H --> R[Chaincode Invocation]
    R --> S[FeedContract]
    R --> T[FarmContract]
    R --> U[SlaughterContract]
    R --> V[ProcessingContract]
    R --> W[RetailContract]
    R --> X[TraceabilityQueryContract]

    H --> Y[Off-chain DB]
    H --> Z[Object Storage]

    H --> AA[Event Outbox / Audit Log]
    AA --> G

    D --> AB[QR / Verification Service]
    AB --> AC[Traceability Lookup]
    AC --> H
    AC --> Y

    H --> AD[Transaction Receipt]
    AD --> D
    D --> B
    B --> A

    style A fill:#dbeafe,stroke:#2563eb,stroke-width:1px
    style B fill:#e0f2fe,stroke:#0284c7,stroke-width:1px
    style C fill:#f3f4f6,stroke:#4b5563,stroke-width:1px
    style D fill:#ecfccb,stroke:#65a30d,stroke-width:1px
    style E fill:#fef3c7,stroke:#d97706,stroke-width:1px
    style F fill:#ede9fe,stroke:#7c3aed,stroke-width:1px
    style G fill:#fce7f3,stroke:#db2777,stroke-width:1px
    style H fill:#fee2e2,stroke:#dc2626,stroke-width:1px
    style I fill:#fee2e2,stroke:#dc2626,stroke-width:1px
    style J fill:#ccfbf1,stroke:#0f766e,stroke-width:1px
    style K fill:#ccfbf1,stroke:#0f766e,stroke-width:1px
    style L fill:#ccfbf1,stroke:#0f766e,stroke-width:1px
    style M fill:#ccfbf1,stroke:#0f766e,stroke-width:1px
    style N fill:#ccfbf1,stroke:#0f766e,stroke-width:1px
    style O fill:#ccfbf1,stroke:#0f766e,stroke-width:1px
    style P fill:#ccfbf1,stroke:#0f766e,stroke-width:1px
    style Q fill:#e0f2fe,stroke:#0ea5e9,stroke-width:1px
    style R fill:#fef3c7,stroke:#d97706,stroke-width:1px
    style S fill:#fde68a,stroke:#d97706,stroke-width:1px
    style T fill:#fde68a,stroke:#d97706,stroke-width:1px
    style U fill:#fde68a,stroke:#d97706,stroke-width:1px
    style V fill:#fde68a,stroke:#d97706,stroke-width:1px
    style W fill:#fde68a,stroke:#d97706,stroke-width:1px
    style X fill:#fde68a,stroke:#d97706,stroke-width:1px
    style Y fill:#f3f4f6,stroke:#4b5563,stroke-width:1px
    style Z fill:#f3f4f6,stroke:#4b5563,stroke-width:1px
    style AA fill:#f3f4f6,stroke:#4b5563,stroke-width:1px
    style AB fill:#fef3c7,stroke:#d97706,stroke-width:1px
    style AC fill:#fef3c7,stroke:#d97706,stroke-width:1px
    style AD fill:#ecfccb,stroke:#65a30d,stroke-width:1px
```

## Integration Notes

### Write Flow

1. Client sends request to API Gateway
2. Gateway authenticates and forwards to application service
3. Service validates business rules
4. Blockchain service submits transaction through Fabric Gateway SDK
5. Fabric peers execute chaincode and commit transaction
6. Off-chain metadata is stored in database or object storage
7. Receipt is returned back to caller

### Read / Verification Flow

1. Consumer or internal user scans QR code
2. Verification service resolves traceability ID
3. Blockchain service queries Fabric for lineage proof
4. Off-chain data is fetched for detailed metadata
5. Verification result is returned to the user

## Suggested Next Refinements

- Add **transaction retry and timeout handling**
- Add **idempotency behavior** for duplicate submissions
- Add **event-driven asynchronous processing**
- Add **private data collection behavior**
- Add **permission enforcement points** for API, service, and chaincode

## Optional Future Versions

A second version can show:

- exact **microservice boundaries**
- **RabbitMQ / SQS** flow details
- **audit logging** and observability paths
- **regulator read-only access**
- **QR service to blockchain service integration**
