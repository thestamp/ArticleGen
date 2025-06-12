# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Set environment variables for production container hosting
ENV ASPNETCORE_ENVIRONMENT=Production
ENV ASPNETCORE_URLS=http://+:8080
ENV ASPNETCORE_FORWARDEDHEADERS_ENABLED=true


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copy project files in dependency order for better layer caching
COPY ["Common.OpenAiClient/Common.OpenAiClient.csproj", "Common.OpenAiClient/"]
COPY ["Common.Web/Common.Web.csproj", "Common.Web/"]
COPY ["ArticleGen.Core/ArticleGen.Core.csproj", "ArticleGen.Core/"]
COPY ["ArticleGen.ServiceDefaults/ArticleGen.ServiceDefaults.csproj", "ArticleGen.ServiceDefaults/"]
COPY ["ArticleGen.Web/ArticleGen.Web.csproj", "ArticleGen.Web/"]

# Restore dependencies
RUN dotnet restore "./ArticleGen.Web/ArticleGen.Web.csproj"

# Copy all source code
COPY . .
WORKDIR "/src/ArticleGen.Web"
RUN dotnet build "./ArticleGen.Web.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./ArticleGen.Web.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ArticleGen.Web.dll"]