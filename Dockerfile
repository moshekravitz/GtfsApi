FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

ENV ASPNETCORE_URLS=http://+:80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["GtfsApi.csproj", "./"]
RUN dotnet restore "GtfsApi.csproj"
COPY . .

RUN dotnet publish "GtfsApi.csproj" -c Release -o /app/publish /p:UseAppHost=true

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "GtfsApi.dll"]

