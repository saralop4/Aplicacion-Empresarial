#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

RUN apt-get update && apt-get install -y nano

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Ecommerce.Services.WebApi/Ecommerce.Services.WebApi.csproj", "Ecommerce.Services.WebApi/"]
COPY ["Ecommerce.Aplicacion.DTO/Ecommerce.Aplicacion.DTO.csproj", "Ecommerce.Aplicacion.DTO/"]
COPY ["Ecommerce.Aplicacion.Interface/Ecommerce.Aplicacion.Interface.csproj", "Ecommerce.Aplicacion.Interface/"]
COPY ["Ecommerce.Transversal/Ecommerce.Common.csproj", "Ecommerce.Transversal/"]
COPY ["Ecommerce.Aplicacion.Main/Ecommerce.Aplicacion.Main.csproj", "Ecommerce.Aplicacion.Main/"]
COPY ["Ecommerce.Aplicacion.Validator/Ecommerce.Aplicacion.Validator.csproj", "Ecommerce.Aplicacion.Validator/"]
COPY ["Ecommerce.Dominio/Ecommerce.Dominio.csproj", "Ecommerce.Dominio/"]
COPY ["Ecommerce.Domain.Core/Ecommerce.Domain.Core.csproj", "Ecommerce.Domain.Core/"]
COPY ["Ecommerce.Infraestructura/Ecommerce.Infraestructura.csproj", "Ecommerce.Infraestructura/"]
COPY ["Ecommerce.Transversal.Mapper/Ecommerce.Transversal.Mapper.csproj", "Ecommerce.Transversal.Mapper/"]
COPY ["Ecommerce.Transversal.Logging/Ecommerce.Transversal.Logging.csproj", "Ecommerce.Transversal.Logging/"]
RUN dotnet restore "./Ecommerce.Services.WebApi/Ecommerce.Services.WebApi.csproj"
COPY . .
WORKDIR "/src/Ecommerce.Services.WebApi"
RUN dotnet build "./Ecommerce.Services.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Ecommerce.Services.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENV ASPNETCORE_ENVIRONMENT=Development

ENTRYPOINT ["dotnet", "Ecommerce.Services.WebApi.dll"]