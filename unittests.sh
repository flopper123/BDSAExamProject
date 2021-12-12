#!/bin/bash

set -eu -o pipefail

echo "Trying to restore"

dotnet restore /code/LitExplore.Tests/LitExplore.Tests.csproj

echo "dotnet restored"

dotnet test /code/LitExplore.Tests/