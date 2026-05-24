# Local Fabric Prototype Docker Notes

## Purpose

This folder contains the Docker-based wiring for the first local Hyperledger Fabric prototype.

## Current Status

Phase 1 is now implemented for the infrastructure bootstrap:

- Orderer container
- Org1 CA
- Org1 peer
- CouchDB
- CLI container
- generated MSP and TLS material
- genesis block
- channel creation
- peer join

## Files included

- `docker-compose.local-fabric.yaml`
- `config/crypto-config.yaml`
- `config/configtx.yaml`
- `scripts/bootstrap-local-fabric.sh`
- `scripts/cleanup.sh`

## Run Phase 1

Start the local Fabric network:

```bash
docker compose -f phases/phase-1-foundation-setup/docker/docker-compose.local-fabric.yaml up -d
```

Bootstrap the channel and join the peer:

```bash
docker compose -f phases/phase-1-foundation-setup/docker/docker-compose.local-fabric.yaml exec cli sh /scripts/bootstrap-local-fabric.sh
```

Verify the channel is joined:

```bash
docker compose -f phases/phase-1-foundation-setup/docker/docker-compose.local-fabric.yaml exec cli peer channel list
```

Reset generated artifacts if you want to rebuild the network from scratch:

```bash
docker compose -f phases/phase-1-foundation-setup/docker/docker-compose.local-fabric.yaml exec cli sh /scripts/cleanup.sh
```

## What is still pending

- CA-based registration and enrollment automation
- chaincode packaging and installation
- application connection profile generation
- backend Fabric Gateway integration
