# Event Flow Design

## Purpose

This document defines how events move through the platform.

The architecture will primarily follow an event-driven model.

---

# High-Level Flow

```text
Frontend
    ↓
API Gateway
    ↓
Microservice
    ↓
Event Bus
    ↓
Blockchain Service
    ↓
Hyperledger Fabric