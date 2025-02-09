# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/devcontainers/dotnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Install Node.js to build Tailwind
FROM node:16 AS nodebuild

# Set the working directory for Tailwind build
WORKDIR /app

# Copy the package.json and package-lock.json (if present) and install the dependencies
COPY ./package*.json ./ 
RUN npm install

# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Install curl, wget, and other dependencies
# RUN apt-get update && apt-get install -y curl wget gnupg

# Add Microsoft package repository for .NET SDK installation
# RUN curl https://packages.microsoft.com/keys/microsoft.asc | apt-key add -
# RUN curl https://packages.microsoft.com/config/ubuntu/20.04/prod.list > /etc/apt/sources.list.d/microsoft-prod.list
# RUN apt-get update

# Install .NET SDK 8.0
# RUN apt-get install -y dotnet-sdk-8.0

# Copy the project and restore dependencies
COPY dotnet-core-tailwind.csproj /src/dotnet-core-tailwind.csproj  
RUN dotnet restore "/src/dotnet-core-tailwind.csproj"

# Add Microsoft.CodeAnalysis.Analyzers NuGet package explicitly
RUN dotnet add /src/dotnet-core-tailwind.csproj package Microsoft.CodeAnalysis.Analyzers --version 3.3.3

COPY . . 
WORKDIR "/src/dotnet-core-tailwind"
RUN dotnet build "/src/dotnet-core-tailwind.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Build Tailwind CSS here by using the node image
FROM nodebuild AS tailwindbuild
WORKDIR /app
COPY ./wwwroot/css /app/wwwroot/css
RUN npx tailwindcss -i ./wwwroot/css/styles.css -o ./wwwroot/css/styles.min.css --minify

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "/src/dotnet-core-tailwind.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Copy the generated Tailwind CSS from the Tailwind build container
COPY --from=tailwindbuild /app/wwwroot/css/styles.min.css /app/wwwroot/css/styles.min.css

ENTRYPOINT ["dotnet", "dotnet-core-tailwind.dll"]