# syntax=docker/dockerfile:1

# Set .NET version from csproj (net9.0)
ARG DOTNET_VERSION=9.0

# --- Build Stage ---
FROM mcr.microsoft.com/dotnet/sdk:${DOTNET_VERSION} AS builder
WORKDIR /src

# Copy csproj and restore as distinct layers for better caching
COPY --link KnowledgeBase.Desktop.csproj ./
COPY --link ../Avalonia.Extension/Avalonia.Extension.csproj ../Avalonia.Extension/
COPY --link ../KnowledgeBase.Model/KnowledgeBase.Model.csproj ../KnowledgeBase.Model/

# Restore dependencies (use cache mounts for speed)
RUN --mount=type=cache,target=/root/.nuget/packages \
    --mount=type=cache,target=/root/.cache/msbuild \
    dotnet restore KnowledgeBase.Desktop.csproj

# Copy the rest of the source code
COPY --link . .
COPY --link ../Avalonia.Extension ../Avalonia.Extension
COPY --link ../KnowledgeBase.Model ../KnowledgeBase.Model

# Build and publish the app
RUN --mount=type=cache,target=/root/.nuget/packages \
    --mount=type=cache,target=/root/.cache/msbuild \
    dotnet publish KnowledgeBase.Desktop.csproj -c Release -o /app/publish --no-restore

# --- Runtime Stage ---
FROM mcr.microsoft.com/dotnet/aspnet:${DOTNET_VERSION} AS final
WORKDIR /app

# Create a non-root user
RUN addgroup --system kbgroup && adduser --system --ingroup kbgroup kbuser
USER kbuser

# Copy published output from build stage
COPY --from=builder /app/publish .

# Avalonia Desktop apps are GUI apps; running in a container requires X11 forwarding or similar
# No ports are exposed by default

ENTRYPOINT ["dotnet", "KnowledgeBase.Desktop.dll"]
