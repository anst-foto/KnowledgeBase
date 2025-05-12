# syntax=docker/dockerfile:1

ARG DOTNET_VERSION=9.0

# --- Build Stage ---
FROM mcr.microsoft.com/dotnet/sdk:${DOTNET_VERSION} AS builder
WORKDIR /src

# Copy csproj and restore as distinct layers
COPY --link KnowledgeBase.WebAPI.csproj ./
COPY --link ../KnowledgeBase.Core/KnowledgeBase.Core.csproj ../KnowledgeBase.Core/

RUN --mount=type=cache,target=/root/.nuget/packages \
    --mount=type=cache,target=/root/.cache/msbuild \
    dotnet restore ./KnowledgeBase.WebAPI.csproj --no-cache

# Copy the rest of the source code
COPY --link ../KnowledgeBase.Core ../KnowledgeBase.Core
COPY --link . .

# Build and publish
RUN --mount=type=cache,target=/root/.nuget/packages \
    --mount=type=cache,target=/root/.cache/msbuild \
    dotnet publish ./KnowledgeBase.WebAPI.csproj -c Release -o /app/publish --no-restore --no-cache

# --- Runtime Stage ---
FROM mcr.microsoft.com/dotnet/aspnet:${DOTNET_VERSION} AS final
WORKDIR /app

# Create a non-root user
RUN addgroup --system appgroup && adduser --system --ingroup appgroup appuser
USER appuser

COPY --from=builder /app/publish .

ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

ENTRYPOINT ["dotnet", "KnowledgeBase.WebAPI.dll"]
