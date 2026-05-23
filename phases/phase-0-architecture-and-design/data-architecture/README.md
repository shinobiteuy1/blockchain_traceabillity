
---

# `data-architecture/README.md`

```markdown id="0g9vyd"
# Data Architecture

## Purpose

This folder defines:
- data ownership
- data storage
- lineage relationships
- blockchain/off-chain separation

---

# Main Storage Layers

## On-Chain
Stores:
- transaction proofs
- immutable events
- hashes
- lineage references

---

## Off-Chain
Stores:
- operational records
- images
- PDFs
- detailed metadata

---

# Main Databases

- PostgreSQL
- CouchDB
- Object Storage

---

# Main Objectives

- scalable lineage tracking
- immutable references
- efficient querying
- audit support

<!-- COMMENT:
Need to define:
- historical event storage strategy
- event sourcing possibility
- archival strategy
-->