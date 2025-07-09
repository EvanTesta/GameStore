# Base runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 5023

# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /build

# Copy solution and projects
COPY GameStore.sln ./
COPY GameStore.Api/GameStore.Api.csproj GameStore.Api/
RUN dotnet restore GameStore.Api/GameStore.Api.csproj

# Copy the rest of the source
COPY . ./
WORKDIR /build/GameStore.Api
RUN dotnet publish -c Release -o /app/publish

# Final image
FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "GameStore.Api.dll"]
