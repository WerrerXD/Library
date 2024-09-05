#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Library_API.Application.csproj", "Library_API.Application"]
COPY ["Library_API.Core.csproj", "Library_API.Core"]
COPY ["Library_API.DataAccess.csproj", "Library_API.DataAccess"]
COPY ["Library_API.csproj", "Library_API"]
WORKDIR D:\Projects\Library\backend\Library_API
#RUN dotnet restore "Library_API.csproj"
COPY . .
WORKDIR "/source"
#RUN dotnet build "./Library_API.csproj" -c $BUILD_CONFIGURATION -o /app/build

#FROM build AS publish
#ARG BUILD_CONFIGURATION=Release
#RUN dotnet publish "Library_API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

#FROM mcr.microsoft.com/dotnet/aspnet:8.0
#WORKDIR /app
#COPY --from=publish/app /app .
#ENTRYPOINT ["dotnet", "Library_API.dll"]