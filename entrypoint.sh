#!/bin/bash
set -e

# Aplicar migrações
dotnet ef database update --project /app/ComexAPI/ComexAPI.csproj --startup-project /app/ComexAPI/ComexAPI.csproj

# Iniciar a aplicação
exec dotnet ComexAPI.dll
