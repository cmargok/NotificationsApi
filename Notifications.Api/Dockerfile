#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
ENV VaultUri=https://cmargokkeys.vault.azure.net/
ENV AZURE_USERNAME=cmargokk@hotmail.com
ENV mail=cmargokk@hotmail.com

EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Notifications.Api/Notifications.Api.csproj", "Notifications.Api/"]
COPY ["Notifications.Application/Notifications.Application.csproj", "Notifications.Application/"]
COPY ["Notifications.Infraestruture/Notifications.Infraestruture.csproj", "Notifications.Infraestruture/"]
RUN dotnet restore "Notifications.Api/Notifications.Api.csproj"
COPY . .
WORKDIR "/src/Notifications.Api"
RUN dotnet build "Notifications.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
RUN dotnet publish "Notifications.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Notifications.Api.dll"]