#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["Order/Ordering.API/Ordering.API.csproj", "Order/Ordering.API/"]
COPY ["Order/Ordering.Application/Ordering.Application.csproj", "Order/Ordering.Application/"]
COPY ["Order/Ordering.Domain/Ordering.Domain.csproj", "Order/Ordering.Domain/"]
COPY ["BuildingBlocks/EventBus.Messages/EventBus.Messages.csproj", "BuildingBlocks/EventBus.Messages/"]
COPY ["Order/Ordering.Infrastructure/Ordering.Infrastructure.csproj", "Order/Ordering.Infrastructure/"]
RUN dotnet restore "Order/Ordering.API/Ordering.API.csproj"
COPY . .
WORKDIR "/src/Order/Ordering.API"
RUN dotnet build "Ordering.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Ordering.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Ordering.API.dll"]