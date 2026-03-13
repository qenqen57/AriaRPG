# Build stage
FROM mcr.microsoft.com/dotnet/sdk:6.0-alpine AS build
WORKDIR /src

# 1. Copy only csproj first → cache NuGet restore
COPY ["AriaRPG.csproj", "."]
RUN dotnet restore --runtime linux-musl-x64

# 2. Copy source and publish (only re-runs if source changed)
COPY . .
RUN dotnet publish -c Release -o /app/publish \
    --runtime linux-musl-x64 \
    --self-contained false \
    --no-restore

# Runtime stage (Alpine = ~100MB smaller)
FROM mcr.microsoft.com/dotnet/aspnet:6.0-alpine AS final
WORKDIR /app
COPY --from=build /app/publish .

# Create a directory for the SQLite database
RUN mkdir -p /app/data

EXPOSE 80

ENTRYPOINT ["dotnet", "AriaRPG.dll"]
