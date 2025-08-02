# Etapa base de runtime (ASP.NET Core)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

# Etapa de build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["HandsOn/HandsOn.csproj", "HandsOn/"]
RUN dotnet restore "HandsOn/HandsOn.csproj"
COPY . .
WORKDIR "/src/HandsOn"
RUN dotnet build "HandsOn.csproj" -c Release -o /app/build

# Etapa de publish
FROM build AS publish
RUN dotnet publish "HandsOn.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Imagem final
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "HandsOn.dll"]