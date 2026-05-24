#!/bin/sh
set -eu

CHANNEL_NAME=${CHANNEL_NAME:-traceabilitychannel}
ORDERER_HOST=${ORDERER_HOST:-orderer.example.com}
ORDERER_PORT=${ORDERER_PORT:-7050}
ORDERER_CA_FILE=/var/hyperledger/orderer/tls/ca.crt

mkdir -p /var/hyperledger/channel-artifacts /var/hyperledger/orderer/msp /var/hyperledger/orderer/tls /var/hyperledger/crypto-config

if [ ! -d /var/hyperledger/crypto-config/ordererOrganizations ] || [ ! -d /var/hyperledger/crypto-config/peerOrganizations ]; then
  cryptogen generate --config /config/crypto-config.yaml --output /var/hyperledger/crypto-config
fi

if [ ! -f /var/hyperledger/orderer/msp/cacerts/ca.example.com-cert.pem ]; then
  cp -R /var/hyperledger/crypto-config/ordererOrganizations/example.com/orderers/orderer.example.com/msp/. /var/hyperledger/orderer/msp/
  cp -R /var/hyperledger/crypto-config/ordererOrganizations/example.com/orderers/orderer.example.com/tls/. /var/hyperledger/orderer/tls/
fi

export FABRIC_CFG_PATH=/config

if [ ! -f /var/hyperledger/channel-artifacts/orderer.genesis.block ]; then
  configtxgen -profile OrdererGenesis -channelID ordererchannel -outputBlock /var/hyperledger/channel-artifacts/orderer.genesis.block
  cp /var/hyperledger/channel-artifacts/orderer.genesis.block /var/hyperledger/orderer/orderer.genesis.block
fi

if [ ! -f /var/hyperledger/channel-artifacts/traceabilitychannel.tx ]; then
  configtxgen -profile TraceabilityChannel -channelID "$CHANNEL_NAME" -outputCreateChannelTx /var/hyperledger/channel-artifacts/traceabilitychannel.tx
fi

export FABRIC_CFG_PATH=/etc/hyperledger/fabric

if [ ! -f /var/hyperledger/channel-artifacts/traceabilitychannel.block ]; then
  peer channel create \
    -o "$ORDERER_HOST:$ORDERER_PORT" \
    -c "$CHANNEL_NAME" \
    -f /var/hyperledger/channel-artifacts/traceabilitychannel.tx \
    --outputBlock /var/hyperledger/channel-artifacts/traceabilitychannel.block \
    --tls \
    --cafile "$ORDERER_CA_FILE"
fi

peer channel join \
  -b /var/hyperledger/channel-artifacts/traceabilitychannel.block

peer channel list
