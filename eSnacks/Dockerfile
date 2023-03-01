# .NET Core SDK
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
EXPOSE 80

# Sets the working directory and copy projects
WORKDIR /app
COPY . ./

# Restore as distinct layers
RUN dotnet restore

# Build and publish a release
RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build-env /app/out .

ENTRYPOINT ["dotnet", "eSnacks.dll"]


