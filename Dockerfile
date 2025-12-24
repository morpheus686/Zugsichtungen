# 1️⃣ Build Stage
FROM mcr.microsoft.com/dotnet/sdk:8.0-windowsservercore-ltsc2022 AS build
WORKDIR /src

# Copy Server-Projekt und restore
COPY Zugsichtungen.Rest.Server/Zugsichtungen.Rest.Server.csproj Zugsichtungen.Rest.Server/
RUN dotnet restore Zugsichtungen.Rest.Server/Zugsichtungen.Rest.Server.csproj

# Copy alle restlichen Dateien
COPY . .

# Publish Server-Projekt
RUN dotnet publish Zugsichtungen.Rest.Server/Zugsichtungen.Rest.Server.csproj -c Release -f net8.0 -r win-x64 --self-contained=false -o /app

# 2️⃣ Runtime Stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0-windowsservercore-ltsc2022
WORKDIR /app

# Kopiere veröffentlichten Server in Runtime-Image
COPY --from=build /app .

# Expose the port the server listens on
EXPOSE 7046

# EntryPoint: Server starten
ENTRYPOINT ["dotnet", "Zugsichtungen.Rest.Server.dll"]
