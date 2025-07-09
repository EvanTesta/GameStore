FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /build

COPY GameStore.sln ./
COPY GameStore.Frontend/GameStore.Frontend.csproj GameStore.Frontend/
RUN dotnet restore GameStore.Frontend/GameStore.Frontend.csproj

COPY . ./
WORKDIR /build/GameStore.Frontend
RUN dotnet publish -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "GameStore.Frontend.dll"]
