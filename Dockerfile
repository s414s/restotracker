# Build Stage

# FROM mcr.microsoft.com/dotnet/aspnet:7.0-alpine AS build
FROM mcr.microsoft.com/dotnet/sdk:7.0-alpine AS build

WORKDIR /App

# Copy everything
COPY . ./

# Restore as distinct layers
RUN dotnet restore "./Retrotracker.Presentation/Retrotracker.Presentation.csproj" --disable-parallel

# Build and publish a release
RUN dotnet publish -c Release -o out
# RUN dotnet publish "./Retrotracker.Presentation/Retrotracker.Presentation.csproj" -c release -o /App --no-restore

# Serve Stage
FROM mcr.microsoft.com/dotnet/aspnet:7.0-alpine AS runtime
WORKDIR /App

# Copy all the artifacts generated during bulid stage
COPY --from=build /App/out ./

EXPOSE 5000

ENTRYPOINT ["dotnet", "Retrotracker.Presentation.dll"]