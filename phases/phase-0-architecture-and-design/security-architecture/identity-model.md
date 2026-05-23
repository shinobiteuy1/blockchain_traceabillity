# Identity Model

## Purpose

This document defines identity and access management across the platform.

The blockchain network is permissioned, meaning every participant must have a verified identity.

---

# Main Identity Types

## Organization Identity

Represents:
- Feed organization
- Farm organization
- Slaughter organization
- Retail organization

Each organization owns:
- peer nodes
- users
- certificates

---

## User Identity

Represents:
- operators
- administrators
- auditors
- regulators

---

## Service Identity

Represents:
- backend services
- automation workers
- API integrations

---

# Initial Authentication Direction

## User Authentication

Potential methods:
- JWT
- OAuth2
- OpenID Connect

---

## Blockchain Authentication

Using:
- Fabric CA
- X.509 certificates
- MSP (Membership Service Provider)

---

# Permission Strategy

## RBAC

Role examples:
- FeedOperator
- FarmOperator
- SlaughterOperator
- Regulator
- SuperAdmin

---

# Organization Isolation

Organizations should only access:
- owned transactions
- permitted lineage data
- approved operational records

---

# Regulator Access

Need special rules for:
- read-only access
- cross-organization auditing
- investigation mode

<!-- COMMENT:
Need to define emergency access workflow.
-->

---

# Future Considerations

- SSO integration
- MFA
- hardware security modules
- certificate rotation automation

<!-- COMMENT:
Need to evaluate Keycloak integration later.
-->