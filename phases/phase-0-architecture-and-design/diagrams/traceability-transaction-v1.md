# Traceability Transaction V1

## Purpose

This document shows the transaction-level traceability flow for the AXONS food platform.

## Scope

This diagram focuses on the lifecycle of a single product batch from creation to retail verification:

- feed batch creation
- farm assignment
- slaughter transformation
- processing and packaging
- QR verification

## Mermaid Diagram

```mermaid
flowchart TD
    A[Create Feed Batch] --> B[Store Feed Metadata Off-chain]
    B --> C[Submit Feed Batch to Fabric]
    C --> D[FeedContract writes lineage record]

    D --> E[Assign Feed to Farm]
    E --> F[Store Farm Assignment Off-chain]
    F --> G[Submit Farm Assignment to Fabric]
    G --> H[FarmContract writes farm linkage]

    H --> I[Register Animal Lot]
    I --> J[Store Animal Lot Off-chain]
    J --> K[Submit Animal Lot to Fabric]
    K --> L[FarmContract updates animal state]

    L --> M[Transform Animal to Carcass]
    M --> N[Store Carcass Off-chain]
    N --> O[Submit Carcass Event to Fabric]
    O --> P[SlaughterContract records carcass lineage]

    P --> Q[Create Processed Product]
    Q --> R[Store Processing Metadata Off-chain]
    R --> S[Submit Processing Event to Fabric]
    S --> T[ProcessingContract records transformation]

    T --> U[Create Retail Package]
    U --> V[Store Package Metadata Off-chain]
    V --> W[Submit Retail Package to Fabric]
    W --> X[RetailContract records package lineage]

    X --> Y[Generate QR Code]
    Y --> Z[Link QR to Package Hash]
    Z --> AA[Consumer / Regulator verifies QR]
    AA --> AB[Query Fabric for proof]
    AB --> AC[Fetch off-chain metadata]
    AC --> AD[Return verification result]

    style A fill:#dbeafe,stroke:#2563eb,stroke-width:1px
    style B fill:#f3f4f6,stroke:#4b5563,stroke-width:1px
    style C fill:#fef3c7,stroke:#d97706,stroke-width:1px
    style D fill:#ecfccb,stroke:#65a30d,stroke-width:1px
    style E fill:#dbeafe,stroke:#2563eb,stroke-width:1px
    style F fill:#f3f4f6,stroke:#4b5563,stroke-width:1px
    style G fill:#fef3c7,stroke:#d97706,stroke-width:1px
    style H fill:#ecfccb,stroke:#65a30d,stroke-width:1px
    style I fill:#dbeafe,stroke:#2563eb,stroke-width:1px
    style J fill:#f3f4f6,stroke:#4b5563,stroke-width:1px
    style K fill:#fef3c7,stroke:#d97706,stroke-width:1px
    style L fill:#ecfccb,stroke:#65a30d,stroke-width:1px
    style M fill:#fee2e2,stroke:#dc2626,stroke-width:1px
    style N fill:#f3f4f6,stroke:#4b5563,stroke-width:1px
    style O fill:#fef3c7,stroke:#d97706,stroke-width:1px
    style P fill:#ecfccb,stroke:#65a30d,stroke-width:1px
    style Q fill:#ede9fe,stroke:#7c3aed,stroke-width:1px
    style R fill:#f3f4f6,stroke:#4b5563,stroke-width:1px
    style S fill:#fef3c7,stroke:#d97706,stroke-width:1px
    style T fill:#ecfccb,stroke:#65a30d,stroke-width:1px
    style U fill:#ccfbf1,stroke:#0f766e,stroke-width:1px
    style V fill:#f3f4f6,stroke:#4b5563,stroke-width:1px
    style W fill:#fef3c7,stroke:#d97706,stroke-width:1px
    style X fill:#ecfccb,stroke:#65a30d,stroke-width:1px
    style Y fill:#fce7f3,stroke:#db2777,stroke-width:1px
    style Z fill:#fce7f3,stroke:#db2777,stroke-width:1px
    AA fill:#fce7f3,stroke:#db2777,stroke-width:1px
    AB fill:#fef3c7,stroke:#d97706,stroke-width:1px
    AC fill:#f3f4f6,stroke:#4b5563,stroke-width:1px
    AD fill:#ecfccb,stroke:#65a30d,stroke-width:1px
```

## Transaction Notes

### On-chain Records

The Fabric ledger stores:

- batch creation events
- ownership and linkage events
- transformation records
- package creation events
- verification references

### Off-chain Records

The application stores:

- detailed operational metadata
- images and inspection data
- shipment and inventory details
- package presentation data

### Verification Model

The QR code should point to a traceability identifier that allows the application to:

1. retrieve the immutable proof from Fabric
2. fetch detailed metadata from off-chain storage
3. present a trusted verification result

## Suggested Next Refinements

- Add **error and exception paths** such as rejected batch, quarantine, or recall
- Add **permission checks** for each transaction step
- Add **event notifications** for downstream organizations
- Add **idempotency / duplicate prevention** for repeated submissions

## Optional Future Versions

A second version can show:

- transaction retries
- asynchronous event propagation
- private data collection usage
- regulatory audit queries
