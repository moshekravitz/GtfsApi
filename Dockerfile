FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["GtfsApi.csproj", "./"]
RUN dotnet restore "GtfsApi.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "GtfsApi.csproj" -c Release -o /app/build

WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "GtfsApi.dll"]
