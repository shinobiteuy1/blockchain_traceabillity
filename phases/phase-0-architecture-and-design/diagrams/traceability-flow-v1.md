# Traceability Flow V1

## Purpose

This document captures the first version of the food traceability flow using Mermaid so it can be reviewed, refined, and later converted into a more formal architecture diagram.

## Flow Summary

The 1-6 flow represents the main lifecycle of a food product in the AXONS traceability platform:

1. Feed ingredient sourcing
2. Feed production
3. Animal raising / farm operations
4. Slaughter and carcass creation
5. Processing and packaging
6. Retail verification and consumer access

## Mermaid Flowchart

```mermaid
flowchart TD
    A[1. Feed Ingredient / FIT] --> B[2. Feed Production]
    B --> C[3. Farm / Animal Raising]
    C --> D[4. Slaughter / Carcass Creation]
    D --> E[5. Further Processing / Packaging]
    E --> F[6. Retail / QR Verification]

    subgraph Inputs
        A1[Raw material sourcing]
        A2[Supplier validation]
        A3[Batch creation]
    end

    subgraph Business Events
        B1[Feed batch created]
        B2[Feed assigned to animal group]
        C1[Animal lot registered]
        C2[Growth and health records updated]
        D1[Animal transformed into carcass]
        D2[Carcass lot created]
        E1[Processed product created]
        E2[Retail package generated]
        F1[QR code generated]
        F2[Consumer verification]
    end

    A --> A1
    A --> A2
    A --> A3

    B --> B1
    C --> B2
    C --> C1
    C --> C2
    D --> D1
    D --> D2
    E --> E1
    E --> E2
    F --> F1
    F --> F2

    style A fill:#dbeafe,stroke:#2563eb,stroke-width:1px
    style B fill:#ecfccb,stroke:#65a30d,stroke-width:1px
    style C fill:#fef3c7,stroke:#d97706,stroke-width:1px
    style D fill:#fee2e2,stroke:#dc2626,stroke-width:1px
    style E fill:#ede9fe,stroke:#7c3aed,stroke-width:1px
    style F fill:#ccfbf1,stroke:#0f766e,stroke-width:1px
```

## Suggested Next Refinements

- Add the **on-chain vs off-chain boundary** to the flow.
- Add **ownership transfer events** between organizations.
- Add **verification points** for regulators and consumers.
- Add **error or exception states**, such as rejected batch, quarantine, or recall.

## Recommended Diagram Style

- Use **Mermaid** for quick design iteration.
- Keep nodes **business-focused** first.
- Add **blockchain annotations** after the business flow is stable.

## Optional Future Version

A second version can show:

- API Gateway
- Blockchain service layer
- Fabric peer connections
- off-chain database writes
- QR verification lookup
