
---

# `infrastructure-architecture/cloud-architecture.md`

```markdown id="k8vcv0"
# Cloud Architecture

## Purpose

This document defines cloud deployment direction.

---

# Initial Deployment Goals

- scalability
- high availability
- disaster recovery
- observability
- security isolation

---

# Initial Infrastructure Direction

## Container Platform
- Kubernetes

## Cloud Candidates
- AWS
- Hybrid deployment

---

# Initial Cluster Design

```text
Production Cluster
├── API Gateway
├── Backend Services
├── Blockchain Gateway
├── Monitoring
├── Messaging
└── Databases