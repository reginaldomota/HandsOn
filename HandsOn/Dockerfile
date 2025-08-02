# Etapa base de runtime (ASP.NET Core)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

# Etapa de build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["ChartOfAccounts.Api.csproj", "."]
RUN dotnet restore "./ChartOfAccounts.Api.csproj"
COPY . .
RUN dotnet build "ChartOfAccounts.Api.csproj" -c Release -o /app/build

# Etapa de publish
FROM build AS publish
RUN dotnet publish "ChartOfAccounts.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Imagem final
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ChartOfAccounts.Api.dll"]
