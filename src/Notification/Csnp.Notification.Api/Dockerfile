# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:9.0-bookworm-slim AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:9.0-bookworm-slim AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Notification/Csnp.Notification.Api/Csnp.Notification.Api.csproj", "src/Notification/Csnp.Notification.Api/"]
COPY ["shared/Csnp.Presentation.Common/Csnp.Presentation.Common.csproj", "shared/Csnp.Presentation.Common/"]
COPY ["src/Notification/Csnp.Notification.Infrastructure/Csnp.Notification.Infrastructure.csproj", "src/Notification/Csnp.Notification.Infrastructure/"]
COPY ["shared/Csnp.SharedKernel.Configuration/Csnp.SharedKernel.Configuration.csproj", "shared/Csnp.SharedKernel.Configuration/"]
COPY ["src/Notification/Csnp.Notification.Application/Csnp.Notification.Application.csproj", "src/Notification/Csnp.Notification.Application/"]
COPY ["shared/Csnp.Security.Infrastructure/Csnp.Security.Infrastructure.csproj", "shared/Csnp.Security.Infrastructure/"]
COPY ["shared/Csnp.SharedKernel.Application/Csnp.SharedKernel.Application.csproj", "shared/Csnp.SharedKernel.Application/"]
COPY ["shared/Csnp.SharedKernel.Domain/Csnp.SharedKernel.Domain.csproj", "shared/Csnp.SharedKernel.Domain/"]
COPY ["shared/Csnp.SeedWork/Csnp.SeedWork.csproj", "shared/Csnp.SeedWork/"]
COPY ["src/Notification/Csnp.Notification.Domain/Csnp.Notification.Domain.csproj", "src/Notification/Csnp.Notification.Domain/"]
COPY ["shared/Csnp.EventBus/Csnp.EventBus.csproj", "shared/Csnp.EventBus/"]
RUN dotnet restore "./src/Notification/Csnp.Notification.Api/Csnp.Notification.Api.csproj"
COPY . .
WORKDIR "/src/src/Notification/Csnp.Notification.Api"
RUN dotnet build "./Csnp.Notification.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Csnp.Notification.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app

# Add custom CA certificate for internal HTTPS (e.g., MinIO self-signed)
USER root
COPY rootCA.crt /usr/local/share/ca-certificates/minio.crt
RUN update-ca-certificates
USER $APP_UID

COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Csnp.Notification.Api.dll"]
