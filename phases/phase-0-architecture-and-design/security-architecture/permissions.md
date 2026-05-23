# Permissions Model

## Purpose

This document defines authorization and access control strategy.

---

# Access Layers

## Application Layer
Controls:
- API access
- UI visibility
- operational permissions

---

## Blockchain Layer
Controls:
- chaincode execution
- ledger access
- private data visibility

---

# Initial Permission Model

## Feed Organization

Can:
- create feed batches
- transfer feed ownership
- view owned records

Cannot:
- modify farm records
- access private slaughter data

---

## Farm Organization

Can:
- register pig groups
- assign feed batches
- update farm lifecycle

---

## Slaughter Organization

Can:
- create slaughter batches
- record carcass information
- create lineage transformations

---

## Retail Organization

Can:
- verify retail products
- manage retail inventory
- access public lineage

---

## Regulator

Can:
- audit records
- view traceability history
- generate compliance reports

Cannot:
- modify operational transactions

---

# Permission Enforcement

Need enforcement at:
- API gateway
- service layer
- blockchain smart contracts

---

# Initial Security Direction

Prefer:
- least privilege access
- append-only blockchain updates
- organization-scoped visibility

<!-- COMMENT:
Need to define:
- cross-org collaboration scenarios
- temporary delegated access
-->