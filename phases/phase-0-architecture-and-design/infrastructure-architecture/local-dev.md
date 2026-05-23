# Local Development Environment

## Purpose

This document defines local development architecture.

---

# Main Goals

- fast onboarding
- isolated development
- reproducible environments
- local blockchain testing

---

# Initial Local Stack

## Infrastructure

- Docker Compose
- PostgreSQL
- Hyperledger Fabric test network
- CouchDB
- Redis
- Kafka/SQS mock

---

# Initial Service Layout

```text
Developer Machine
├── Frontend
├── Backend Services
├── Blockchain Network
├── PostgreSQL
├── Message Broker
└── Monitoring Stack