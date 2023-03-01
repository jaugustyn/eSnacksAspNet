# .NET Core SDK
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
EXPOSE 80

# Sets the working directory and copy projects
WORKDIR /app

# Restore as distinct layers
COPY eSnacks/*.csproj .
RUN dotnet restore

# Build and publish a release
COPY eSnacks/. .
RUN dotnet publish -c Release -o publish

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build-env /app/publish .

ENTRYPOINT ["dotnet", "eSnacks.dll"]


