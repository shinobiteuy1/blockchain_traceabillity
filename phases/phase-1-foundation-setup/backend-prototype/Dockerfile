# syntax=docker/dockerfile:1

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY ["phases/phase-1-foundation-setup/backend-prototype/AxonsFabricBackendPrototype.csproj", "phases/phase-1-foundation-setup/backend-prototype/"]
RUN dotnet restore "phases/phase-1-foundation-setup/backend-prototype/AxonsFabricBackendPrototype.csproj"

COPY ["phases/phase-1-foundation-setup/backend-prototype/", "phases/phase-1-foundation-setup/backend-prototype/"]
WORKDIR "/src/phases/phase-1-foundation-setup/backend-prototype"
RUN dotnet publish "AxonsFabricBackendPrototype.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
ENV ASPNETCORE_URLS=http://+:8080
ENV ASPNETCORE_ENVIRONMENT=Production

COPY --from=build /app/publish .
COPY ["phases/phase-1-foundation-setup/backend-prototype/config/local", "/app/config/local"]
COPY ["phases/phase-5-frontend-applications/ui", "/app/ui"]

EXPOSE 8080
ENTRYPOINT ["dotnet", "AxonsFabricBackendPrototype.dll"]
