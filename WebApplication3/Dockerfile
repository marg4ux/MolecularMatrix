# Use the official .NET 8.0 SDK image for building the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# Set the working directory in the container
WORKDIR /app

# Copy the .csproj file and restore any dependencies (via dotnet restore)
COPY *.csproj ./
RUN dotnet restore

# Copy the rest of the application files
COPY . ./

# Publish the app to the /out directory
RUN dotnet publish -c Release -o /out

# Use the official .NET 8.0 runtime image for running the application
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final

# Set the working directory for the app
WORKDIR /app

# Copy the published application from the build image
COPY --from=build /out .

# Expose port 80 for the application
EXPOSE 80

# Define the entry point for the application
ENTRYPOINT ["dotnet", "WebApplication3.dll"]
