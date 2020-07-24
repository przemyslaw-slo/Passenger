#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["src/Passenger.Api/Passenger.Api.csproj", "src/Passenger.Api/"]
COPY ["src/Passenger.Core/Passenger.Core.csproj", "src/Passenger.Core/"]
COPY ["src/Passenger.Infrastructure/Passenger.Infrastructure.csproj", "src/Passenger.Infrastructure/"]
RUN dotnet restore "src/Passenger.Api/Passenger.Api.csproj"
COPY . .
WORKDIR "/src/src/Passenger.Api"
RUN dotnet build "Passenger.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Passenger.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Passenger.Api.dll"]