# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Install Node.js to build Tailwind
FROM node:16 AS nodebuild

# Set the working directory for Tailwind build
WORKDIR /app

# Copy the package.json and package-lock.json (if present) and install the dependencies
COPY MvcProject.web/package*.json ./
RUN npm install

# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["MvcProject.web/MvcProject.web.csproj", "MvcProject.web/"]
RUN dotnet restore "./MvcProject.web/MvcProject.web.csproj"
COPY . . 
WORKDIR "/src/MvcProject.web"
RUN dotnet build "./MvcProject.web.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Build Tailwind CSS here by using the node image
FROM nodebuild AS tailwindbuild
WORKDIR /app
COPY MvcProject.web/wwwroot/css ./wwwroot/css
RUN npx tailwindcss -i ./wwwroot/css/styles.css -o ./wwwroot/css/styles.min.css --minify

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./MvcProject.web.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Copy the generated Tailwind CSS from the Tailwind build container
COPY --from=tailwindbuild /app/wwwroot/css/styles.min.css /app/wwwroot/css/styles.min.css

ENTRYPOINT ["dotnet", "MvcProject.web.dll"]
