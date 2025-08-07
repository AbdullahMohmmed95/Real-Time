#!/bin/bash
echo "⛔️ Stopping and removing old containers..."
docker compose down

echo "📦 Cleaning old build artifacts..."
rm -rf frontend/build
docker system prune -f

echo "🚀 Rebuilding full stack..."
docker compose build --no-cache
docker compose up -d --force-recreate

echo "✅ App rebuilt and running!"

