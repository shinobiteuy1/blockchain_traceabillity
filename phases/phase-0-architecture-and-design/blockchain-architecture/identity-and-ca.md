# Identity & Certificate Authority Design

## Purpose

This document defines blockchain identity management and certificate authority architecture.

Hyperledger Fabric is a permissioned blockchain.
Every participant must have a trusted identity.

---

# Main Components

## Fabric CA

Responsible for:
- issuing certificates
- managing identities
- revoking certificates

---

## MSP (Membership Service Provider)

Responsible for:
- validating identities
- organization trust management
- permission enforcement

---

# Identity Hierarchy

```text
Root CA
    ↓
Organization CA
    ↓
Users / Peers / Services