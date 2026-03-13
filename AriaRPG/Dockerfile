# Build stage
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["AriaRPG.csproj", "."]
RUN dotnet restore
COPY . .
RUN dotnet publish -c Release -o /app/publish

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

# Create a directory for the SQLite database
RUN mkdir -p /app/data

EXPOSE 80
EXPOSE 443

ENTRYPOINT ["dotnet", "AriaRPG.dll"]
