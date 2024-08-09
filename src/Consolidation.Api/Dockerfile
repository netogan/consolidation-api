FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
ENV ASPNETCORE_URLS http://+:8080
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Consolidation.Api/Consolidation.Api.csproj", "."]
RUN dotnet restore "./Consolidation.Api/Consolidation.Api.csproj"
COPY . .
WORKDIR "/src/Consolidation.Api"
RUN dotnet build "./Consolidation.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Consolidation.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Consolidation.Api.dll"]