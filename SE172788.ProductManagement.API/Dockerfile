# Use the official .NET 8.0 image as a parent image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

# Use the SDK image for building the app
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy the project files and restore dependencies
COPY ["SE172788.ProductManagement.API/SE172788.ProductManagement.API.csproj", "SE172788.ProductManagement.API/"]
COPY ["SE172788.ProductManagement.Repo/SE172788.ProductManagement.Repo.csproj", "SE172788.ProductManagement.Repo/"]
RUN dotnet restore "SE172788.ProductManagement.API/SE172788.ProductManagement.API.csproj"

# Copy the rest of the application files and build
COPY . .
WORKDIR "/src/SE172788.ProductManagement.API"
RUN dotnet build "SE172788.ProductManagement.API.csproj" -c Release -o /app/build

# Publish the application
FROM build AS publish
RUN dotnet publish "SE172788.ProductManagement.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Final stage/image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SE172788.ProductManagement.API.dll"]
