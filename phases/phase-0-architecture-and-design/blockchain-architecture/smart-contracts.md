
---

# `blockchain-architecture/smart-contracts.md`

```markdown id="2y4ylr"
# Smart Contract Design

## Purpose

This document defines the smart contract architecture for the blockchain layer.

---

# Main Responsibilities

Smart contracts are responsible for:

- validating transactions
- enforcing business rules
- storing immutable lineage data
- verifying ownership
- tracking state transitions

---

# Initial Contract Candidates

## FeedContract

Responsible for:
- feed batch creation
- feed ownership transfer
- feed verification

---

## FarmContract

Responsible for:
- pig registration
- feed assignment
- farm lifecycle tracking

---

## SlaughterContract

Responsible for:
- slaughter registration
- carcass tracking
- slaughter batch generation

---

## ProcessingContract

Responsible for:
- product transformation
- packaging
- processing lineage

---

## RetailContract

Responsible for:
- retail registration
- consumer verification
- QR validation

---

# Shared Contract Concerns

## Validation
Must validate:
- ownership
- organization permissions
- lineage consistency

---

## Immutability
Contracts should avoid:
- destructive updates
- history deletion

---

## Event Emission
Contracts should emit:
- blockchain events
- lineage events
- audit events

<!-- COMMENT:
Need to define event naming conventions.
-->

---

# Initial State Strategy

## Ledger State
Stores:
- latest active state

## Blockchain History
Stores:
- immutable transaction history

---

# Initial Design Direction

Prefer:
- modular contracts
- bounded domain ownership
- small transaction payloads

Avoid:
- storing large files on-chain
- storing images directly on-chain

---

# Future Considerations

- private data collections
- confidential transactions
- regulator-only access
- multi-country compliance

<!-- COMMENT:
Need to evaluate chaincode language:
- Go
- TypeScript
-->