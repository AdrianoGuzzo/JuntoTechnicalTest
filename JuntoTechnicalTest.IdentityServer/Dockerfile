#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["JuntoTechnicalTest.IdentityServer/JuntoTechnicalTest.IdentityServer.csproj", "JuntoTechnicalTest.IdentityServer/"]
RUN dotnet restore "./JuntoTechnicalTest.IdentityServer/JuntoTechnicalTest.IdentityServer.csproj"
COPY . .
WORKDIR "/src/JuntoTechnicalTest.IdentityServer"
RUN dotnet build "./JuntoTechnicalTest.IdentityServer.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./JuntoTechnicalTest.IdentityServer.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "JuntoTechnicalTest.IdentityServer.dll"]