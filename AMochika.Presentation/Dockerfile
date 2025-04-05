FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["AMochika.Presentation/AMochika.Presentation.csproj", "AMochika.Presentation/"]
RUN dotnet restore "AMochika.Presentation/AMochika.Presentation.csproj"
COPY . .
WORKDIR "/src/AMochika.Presentation"
RUN dotnet build "AMochika.Presentation.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "AMochika.Presentation.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AMochika.Presentation.dll"]

# Cambiar a usuario root para tener permisos de instalación
USER root

# Instalar dependencias de Azure CLI
RUN apt-get update && apt-get install -y curl lsb-release apt-transport-https ca-certificates

# Instalar Azure CLI
RUN curl -sL https://aka.ms/InstallAzureCLIDeb | bash

# Iniciar Azure CLI
RUN az --version
