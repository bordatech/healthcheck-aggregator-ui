FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["AspNetCore.HealthChecks.Aggregator.UI.csproj", "AspNetCore.HealthChecks.Aggregator.UI/"]
RUN dotnet restore "AspNetCore.HealthChecks.Aggregator.UI/AspNetCore.HealthChecks.Aggregator.UI.csproj"
COPY . ./AspNetCore.HealthChecks.Aggregator.UI
WORKDIR "/src/AspNetCore.HealthChecks.Aggregator.UI"
RUN dotnet build "AspNetCore.HealthChecks.Aggregator.UI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AspNetCore.HealthChecks.Aggregator.UI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AspNetCore.HealthChecks.Aggregator.UI.dll"]
