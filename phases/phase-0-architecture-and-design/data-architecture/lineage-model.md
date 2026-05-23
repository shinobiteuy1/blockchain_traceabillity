# Lineage Model

## Purpose

This document defines how product lineage and traceability relationships are modeled across the platform.

The lineage system is one of the most important parts of the entire architecture.

---

# Main Objectives

The lineage model must support:

- Full end-to-end traceability
- Parent-child relationships
- Batch split operations
- Batch merge operations
- Transformation tracking
- Immutable history
- Efficient querying

---

# Initial Product Flow

```text
Ingredient Batch
    ↓
Feed Batch
    ↓
Pig Group
    ↓
Slaughter Batch
    ↓
Processing Batch
    ↓
Retail Product