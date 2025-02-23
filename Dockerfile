# Use .NET SDK 8.0 for building
ARG VARIANT=8.0
FROM mcr.microsoft.com/dotnet/sdk:${VARIANT} AS build

# Set working directory
WORKDIR /app

# Install Node.js for Tailwind CSS and clean up unnecessary files to reduce image size
RUN curl -fsSL https://deb.nodesource.com/setup_lts.x | bash - \
    && apt-get update \
    && apt-get install -y nodejs \
    && apt-get install -y git \
    && apt-get clean

# Generate and trust the .NET development certificate
RUN dotnet dev-certs https --clean && dotnet dev-certs https --trust

# Copy and restore dependencies
COPY dotnet-core-tailwind.csproj ./ 
RUN dotnet restore

# Copy remaining files and build
COPY . . 
RUN dotnet publish -c Release -o out --no-restore

# Runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final

# Set working directory
WORKDIR /app

# Copy built application from build stage
COPY --from=build /app/out .

# Expose port 80
EXPOSE 80

# Run the application
CMD ["dotnet", "dotnet-core-tailwind.dll"]