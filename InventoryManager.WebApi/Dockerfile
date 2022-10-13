#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["InventoryManager.WebApi/InventoryManager.WebApi.csproj", "InventoryManager.WebApi/"]
COPY ["InventoryManager.Application/InventoryManager.Application.csproj", "InventoryManager.Application/"]
COPY ["InventoryManager.CrossCutting/InventoryManager.CrossCutting.csproj", "InventoryManager.CrossCutting/"]
COPY ["InventoryManager.Infraestructure/InventoryManager.Infraestructure.csproj", "InventoryManager.Infraestructure/"]
COPY ["InventoryManager.Domain/InventoryManager.Domain.csproj", "InventoryManager.Domain/"]
RUN dotnet restore "InventoryManager.WebApi/InventoryManager.WebApi.csproj"
COPY . .
WORKDIR "/src/InventoryManager.WebApi"
RUN dotnet build "InventoryManager.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "InventoryManager.WebApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "InventoryManager.WebApi.dll"]