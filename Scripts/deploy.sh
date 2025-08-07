#!/bin/bash

# Absolute path to .env file (adjusted to run from /Scripts folder)
SCRIPT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"
ENV_FILE_PATH="$SCRIPT_DIR/../.env"

echo "Loading variables from .env at $ENV_FILE_PATH..."

# Read .env and export variables to current environment
while IFS= read -r line || [[ -n "$line" ]]; do
    if [[ $line =~ ^[[:space:]]*([^#][^=]+)=(.+)$ ]]; then
        name="${BASH_REMATCH[1]}"
        value="${BASH_REMATCH[2]}"
        export "$name"="$value"
    fi
done < "$ENV_FILE_PATH"

echo "Stopping and removing old container..."
docker stop hands-on-dev-container 2>/dev/null || true
docker rm hands-on-dev-container 2>/dev/null || true

echo "Building new API image..."
docker build -t hands-on-dev-api "$SCRIPT_DIR/.."  # Dockerfile is in the root

echo "Creating container with environment variables..."
docker run -d \
    -p 8080:8080 \
    --network container_network \
    --name hands-on-dev-container \
    --env POSTGRES_HOST="$POSTGRES_HOST" \
    --env POSTGRES_PORT="$POSTGRES_PORT" \
    --env POSTGRES_DB="$POSTGRES_DB" \
    --env POSTGRES_USER="$POSTGRES_USER" \
    --env POSTGRES_PASSWORD="$POSTGRES_PASSWORD" \
    hands-on-dev-api

echo "Deployment completed successfully."