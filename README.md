# 🚀 Docker: PostgreSQL + pgAdmin + HandsOn API

## ✅ Pré-requisitos

Antes de começar, certifique-se de que os seguintes requisitos estão atendidos:

- [Docker](https://www.docker.com/products/docker-desktop) instalado e em execução
  - Para verificar, execute `docker --version`
- Acesso a um terminal (PowerShell, CMD, Terminal Linux/macOS)
- Permissão para criar arquivos locais (para o `servers.json`)

---

Guia prático para subir containers PostgreSQL, pgAdmin e uma aplicação ASP.NET Core (`HandsOn`) no Docker.

---

## 📙 Índice

- [📦 1. Baixar imagens Docker](#1-baixar-imagens-docker)
- [🌐 2. Criar rede Docker](#2-criar-rede-docker)
- [🐘 3. Executar PostgreSQL](#3-executar-postgresql)
- [🛀️ 4. Criar arquivo servers.json](#4-criar-arquivo-serversjson)
- [💻 5. Executar pgAdmin](#5-executar-pgadmin)
- [🧪 6. Build e execução do projeto HandsOn](#6-build-e-execucao-do-projeto-handson)
- [📘️ 7. Criar estrutura de plano de contas](#7-criar-estrutura-de-plano-de-contas)
- [📅 8. Inserir dados iniciais](#8-inserir-dados-iniciais)
- [🌍 9. Acessar pgAdmin](#9-acessar-pgadmin)
- [🔗 10. Acessar Swagger da API](#10-acessar-swagger-da-api)

---

## 📦 1. Baixar imagens Docker

```bash
docker pull postgres
docker pull dpage/pgadmin4
docker pull mcr.microsoft.com/dotnet/aspnet:8.0
docker pull mcr.microsoft.com/dotnet/sdk:8.0
```

---

## 🌐 2. Criar rede Docker

```bash
docker network create container_network
```

---

## 🐘 3. Executar PostgreSQL

```bash
docker run --name Postgres --network container_network -e POSTGRES_USER=root -e POSTGRES_PASSWORD=root1234 -e POSTGRES_DB=accounts -p 5432:5432 -d postgres
```

---

## 🛀️ 4. Criar arquivo `servers.json`

```json
{
  "Servers": {
    "1": {
      "Name": "Meu PostgreSQL",
      "Group": "Servers",
      "Host": "Postgres",
      "Port": 5432,
      "MaintenanceDB": "accounts",
      "Username": "root",
      "Password": "root1234",
      "SSLMode": "prefer"
    }
  }
}
```

Salve no mesmo diretório onde você irá rodar o pgAdmin.

---

## 💻 5. Executar pgAdmin

> `$(pwd)` (Linux/macOS) ou `${PWD}` (Windows PowerShell) representa o caminho absoluto da pasta atual. Para obter este caminho, utilize:
>
> - Linux/macOS: `pwd`
> - PowerShell: `${PWD}`

### 💼 Linux/macOS:

```bash
docker run --name pgadmin --network container_network -p 8090:80 -e PGADMIN_DEFAULT_EMAIL="reginaldomotacc@gmail.com" -e PGADMIN_DEFAULT_PASSWORD="rm1234" -v "$(pwd)/servers.json":/pgadmin4/servers.json -e PGADMIN_SERVER_JSON_FILE="/pgadmin4/servers.json" -d dpage/pgadmin4
```

### 🩠 Windows PowerShell:

```powershell
docker run --name pgadmin --network container_network -p 8090:80 -e PGADMIN_DEFAULT_EMAIL="reginaldomotacc@gmail.com" -e PGADMIN_DEFAULT_PASSWORD="rm1234" -v "${PWD}\servers.json:/pgadmin4/servers.json" -e PGADMIN_SERVER_JSON_FILE="/pgadmin4/servers.json" -d dpage/pgadmin4
```

---

## 🧪 6. Build e execução do projeto `HandsOn`

### 📁 Clonar repositório

```bash
git clone https://github.com/reginaldomota/HandsOn.git
cd HandsOn/HandsOn
```

### 💡 Dockerfile incluído no repositório

> Já está presente em `HandsOn/HandsOn/Dockerfile` com configuração pronta:

```dockerfile
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["ChartOfAccounts.Api.csproj", "."]
RUN dotnet restore "ChartOfAccounts.Api.csproj"
COPY . .
RUN dotnet build "ChartOfAccounts.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ChartOfAccounts.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
COPY appsettings.json .
ENTRYPOINT ["dotnet", "ChartOfAccounts.Api.dll"]
```

### 🔧 Build da imagem

```bash
docker build -t hands-on-api .
```

### ▶️ Executar o container

```bash
docker run -d -p 8080:8080 --network container_network --name hands-on-container hands-on-api
```

---

## 📘️ 7. Criar estrutura de plano de contas

Baixe o arquivo de criação da tabela:

📄 [`table_chart_of_accounts.sql`](Scripts/table_chart_of_accounts.sql)

```bash
docker cp Scripts/table_chart_of_accounts.sql Postgres:/table_chart_of_accounts.sql

docker exec -it Postgres psql -U root -d accounts -f /table_chart_of_accounts.sql
```

---

## 📅 8. Inserir dados iniciais

Baixe e copie o arquivo de inserção:

📄 [`insert_data_chart_of_accounts.sql`](Scripts/insert_data_chart_of_accounts.sql)

```bash
docker cp Scripts/insert_data_chart_of_accounts.sql Postgres:/insert_data_chart_of_accounts.sql

docker exec -it Postgres psql -U root -d accounts -f /insert_data_chart_of_accounts.sql
```

---

## 🌍 9. Acessar pgAdmin

```
http://localhost:8090
```

- **Email:** `reginaldomotacc@gmail.com`
- **Senha:** `rm1234`

O pgAdmin já estará conectado ao PostgreSQL automaticamente.

---

## 🔗 10. Acessar Swagger da API

```
http://localhost:8080/swagger
```

---

✅ Pronto! Você agora tem PostgreSQL, pgAdmin, dados populados e sua API `HandsOn` rodando com Docker.
