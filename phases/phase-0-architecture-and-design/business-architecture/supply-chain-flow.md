# Example in supply-chain-flow.md

## Feed to Farm Flow

- Feed supplier produces Feed Batch A
- Feed Batch A is delivered to Farm X
- Farm X assigns Feed Batch A to Pig Group P1

<!-- COMMENT: Consider how to handle multiple feed batches feeding the same pig group -->

# Supply Chain Flow

## Overview

This document defines the high-level product flow across the food traceability ecosystem.

---

# Initial Flow

```text
FIT → Feed → Farm → Slaughter → Further → Retail

Flow Description
1. FIT

Responsible for:

raw material sourcing
supplier management
ingredient verification

Outputs:

ingredient batches

2. Feed

Responsible for:

feed production
feed batch creation
quality control

Outputs:

feed batches

3. Farm

Responsible for:

animal raising
feeding records
health records

Outputs:

farm batches / pig groups
4. Slaughter

Responsible for:

slaughter processing
carcass tracking
weight recording

Outputs:

slaughter batches
5. Further Processing

Responsible for:

cutting
packaging
transformation

Outputs:

packaged products
6. Retail

Responsible for:

distribution
consumer access
QR verification

Outputs:

retail traceability endpoint