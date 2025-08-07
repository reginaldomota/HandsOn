# 🚀 Plano de Contas API: Ambiente Completo com Docker, PostgreSQL e pgAdmin

## 📊 Introdução ao Projeto Plano de Contas

Este projeto implementa uma API para gerenciamento de Plano de Contas (Chart of Accounts), um componente fundamental em sistemas contábeis e financeiros. Um Plano de Contas é uma estrutura hierárquica organizada que classifica todas as contas contábeis de uma organização, facilitando o registro, categorização e análise de transações financeiras.

### Características principais:

- **Multitenancy**: Suporte a múltiplos inquilinos (tenants) com isolamento de dados através do `TenantId`
- **Estrutura hierárquica**: Contas organizadas em estrutura de árvore usando o campo `ParentCode`
- **Idempotência**: Implementação de idempotência nas operações através do `IdempotencyKey`
- **Normalização de códigos**: Armazenamento de versões normalizadas dos códigos para melhor pesquisa
- **Segurança**: Autenticação via JWT para proteger os endpoints da API

### Modelo de dados:

A aplicação gerencia contas contábeis com os seguintes atributos:
- **Code**: Código único da conta (ex: "1.01.01.001")
- **Name**: Nome descritivo da conta
- **Type**: Categoria da conta (Ativo, Passivo, Receita, Despesa, etc.)
- **IsPostable**: Indica se a conta permite lançamentos diretos
- **ParentCode**: Referência à conta pai na hierarquia

### 🔍 APIs disponíveis

A aplicação expõe um conjunto de endpoints RESTful organizados por grupos:
8
#### 🔐 Autenticação JWT

* **POST** `/api/v1/auth/token` — Geração de token JWT com base nas credenciais informadas.

  **Tenants disponíveis para autenticação:**
  
  Utilize um dos seguintes conjuntos de credenciais para gerar seu token JWT:

#### 📚 Plano de Contas

* **GET** `/api/v1/accounts/suggestion/{parentCode}` — Sugere um novo código com base no código pai informado.
* **GET** `/api/v1/accounts` — Lista todas as contas existentes com suporte a filtros e paginação.
  * **Filtros disponíveis:**
    * `find` — Termo de pesquisa genérico que busca correspondências em códigos e nomes
    * `isPostable` — Filtra contas que permitem (true) ou não (false) lançamentos diretos
    * `page` — Número da página para paginação (padrão: 1)
    * `pageSize` — Quantidade de itens por página (padrão: 20)
  * **Exemplos:** 
    * `/api/v1/accounts?find=Banco&page=1&pageSize=10` — Busca contas com "Banco" no código ou nome
    * `/api/v1/accounts?isPostable=true` — Lista apenas contas que permitem lançamentos
    * `/api/v1/accounts?find=1.01&isPostable=false` — Busca contas sintéticas que começam com 1.01
* **POST** `/api/v1/accounts` — Cria uma nova conta no plano de contas.
* **GET** `/api/v1/accounts/{code}` — Consulta os detalhes de uma conta específica.
* **DELETE** `/api/v1/accounts/{code}` — Remove uma conta específica do plano.

---

## ✅ Pré-requisitos

Antes de começar, certifique-se de que os seguintes requisitos estão atendidos:

* [Docker](https://www.docker.com/products/docker-desktop) instalado e em execução
  * Para verificar, execute `docker --version`
* Acesso a um terminal (PowerShell, CMD, Terminal Linux/macOS)
* Permissão para criar arquivos locais (para o `servers.json`)

---

Guia prático para subir containers PostgreSQL, pgAdmin e uma aplicação ASP.NET Core (`HandsOn`) no Docker.

---

## 📙 Índice

* [📊 Introdução ao Projeto Plano de Contas](#introdução-ao-projeto-plano-de-contas)
* [✅ Pré-requisitos](#pré-requisitos)
* [📁 1. Clonar o repositório do projeto](#1-clonar-o-repositório-do-projeto)
* [📦 2. Baixar imagens Docker](#2-baixar-imagens-docker)
* [🐘 3. Executar PostgreSQL](#3-executar-postgresql)
* [🛀️ 4. Criar arquivo servers.json](#4-criar-arquivo-serversjson)
* [💻 5. Executar pgAdmin](#5-executar-pgadmin)
* [🔧 6. Build e execução da API](#6-build-e-execução-da-api)
* [📘️ 7. Criar estrutura de plano de contas](#7-criar-estrutura-de-plano-de-contas)
* [🗕️ 8. Inserir dados iniciais](#8-inserir-dados-iniciais)
* [🌍 9. Acessar pgAdmin](#9-acessar-pgadmin)
* [🔗 10. Acessar Swagger da API](#10-acessar-swagger-da-api)
* [🔑 11. Autenticando na API](#11-autenticando-na-api)

---

## 📁 1. Clonar o repositório do projeto

### 📁 Clonar repositório

```bash
git clone https://github.com/reginaldomota/HandsOn.git
cd HandsOn
```

> **O que está acontecendo:** Clonamos o repositório que contém o código-fonte da API de Plano de Contas e navegamos para o diretório do projeto. Este código contém uma aplicação ASP.NET Core que implementa os endpoints RESTful para gerenciar o plano de contas.

### 💡 Dockerfile incluído no repositório

> Já está presente em `HandsOn/Dockerfile` com configuração pronta:

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
> **O que está acontecendo:** Estamos criando uma imagem Docker para nossa API usando o Dockerfile presente no diretório. O parâmetro `-t hands-on-api` attribui um nome ("tag") à imagem para facilitar sua referência posteriormente. O ponto final (`.`) indica que o Dockerfile está no diretório atual.

### 🌐 Criar rede Docker

```bash
docker network create container_network
```

> **O que está acontecendo:** Estamos criando uma rede Docker dedicada chamada `container_network`. Esta rede permite que os containers se comuniquem entre si usando nomes como hostnames, em vez de endereços IP. Isso é essencial para que o pgAdmin consiga se conectar ao PostgreSQL e para que nossa API acesse o banco de dados usando nomes de hosts consistentes.


### ▶️ Executar o container

```bash
docker run -d -p 8080:8080 --network container_network --name hands-on-container hands-on-api
```

> **O que está acontecendo:** Este comando cria e executa um container com nossa API:
> - `-d`: Executa o container em segundo plano
> - `-p 8080:8080`: Mapeia a porta 8080 do container para a mesma porta no host
> - `--network container_network`: Conecta o container à mesma rede do PostgreSQL e pgAdmin
> - `--name hands-on-container`: Define um nome para o container
> - `hands-on-api`: Usa a imagem que acabamos de criar
>
> Agora nossa API está rodando e pode se comunicar com o banco de dados PostgreSQL.

---

## 📦 2. Baixar imagens Docker

```bash
docker pull postgres
docker pull dpage/pgadmin4
```

> **O que está acontecendo:** Estamos baixando as imagens oficiais do PostgreSQL e pgAdmin do Docker Hub. Essas imagens contêm o software pré-configurado que utilizaremos para criar os containers. Baixar as imagens antes de executar os containers garante uma inicialização mais rápida e evita problemas de conectividade durante a criação dos containers.

---

## 🐘 3. Executar PostgreSQL

```bash
docker run --name Postgres --network container_network -e POSTGRES_USER=postgresuser -e POSTGRES_PASSWORD=admin1234 -e POSTGRES_DB=accounts -p 5432:5432 -d postgres
```

> **O que está acontecendo:** Este comando cria e inicia um container PostgreSQL com as seguintes características:
> - `--name Postgres`: Define o nome do container como "Postgres" para fácil referência
> - `--network container_network`: Conecta o container à rede que criamos anteriormente
> - `-e POSTGRES_USER=postgresuser`: Cria um usuário administrador chamado "root"
> - `-e POSTGRES_PASSWORD=admin1234`: Define a senha deste usuário
> - `-e POSTGRES_DB=accounts`: Cria automaticamente um banco de dados chamado "accounts"
> - `-p 5432:5432`: Mapeia a porta 5432 do container para a porta 5432 do host, permitindo conexões externas
> - `-d`: Executa o container em segundo plano (modo detached)

---

## 🛀️ 4. Criar arquivo `postgres_server.json`

```json
{
  "Servers": {
    "1": {
      "Name": "Meu PostgreSQL",
      "Group": "Servers",
      "Host": "Postgres",
      "Port": 5432,
      "MaintenanceDB": "accounts",
      "Username": "postgresuser",
      "Password": "admin1234",
      "SSLMode": "prefer"
    }
  }
}
```

> **O que está acontecendo:** Estamos criando um arquivo de configuração para o pgAdmin que contém informações de conexão pré-configuradas para o nosso servidor PostgreSQL. Isso elimina a necessidade de configurar manualmente a conexão através da interface do pgAdmin. Note que o campo `Host` usa o nome do container PostgreSQL ("Postgres"), o que só funciona porque ambos os containers estão na mesma rede Docker.

Salve no mesmo diretório onde você ira rodar o pgAdmin.

---

## 💻 5. Executar pgAdmin

> `$(pwd)` (Linux/macOS) ou `${PWD}` (Windows PowerShell) representa o caminho absoluto da pasta atual. Para obter este caminho, utilize:
>
> * Linux/macOS: `pwd`
> * PowerShell: `${PWD}`

### 💼 Linux/macOS:

```bash
docker run --name pgadmin --network container_network -p 8090:80 -e PGADMIN_DEFAULT_EMAIL="user@yourmail.com" -e PGADMIN_DEFAULT_PASSWORD="user1234" -v "$(pwd)/Scripts/postgres_server.json":/pgadmin4/servers.json -e PGADMIN_SERVER_JSON_FILE="/pgadmin4/servers.json" -d dpage/pgadmin4
```

### 🪠 Windows PowerShell:

```powershell
docker run --name pgadmin --network container_network -p 8090:80 -e PGADMIN_DEFAULT_EMAIL="user@yourmail.com" -e PGADMIN_DEFAULT_PASSWORD="user1234" -v "${PWD}\Scripts\postgres_server.json:/pgadmin4/servers.json" -e PGADMIN_SERVER_JSON_FILE="/pgadmin4/servers.json" -d dpage/pgadmin4
```

> **O que está acontecendo:** Este comando cria e inicia um container pgAdmin com as seguintes características:
> - `--name pgadmin`: Define o nome do container como "pgadmin"
> - `--network container_network`: Conecta o container à mesma rede do PostgreSQL
> - `-p 8090:80`: Mapeia a porta 80 do container para a porta 8090 do host, permitindo acesso via navegador
> - `-e PGADMIN_DEFAULT_EMAIL/PASSWORD`: Define credenciais para acessar o pgAdmin
> - `-v "$(pwd)/postgres_server.json:/pgadmin4/servers.json"`: Monta o arquivo de configuração que criamos dentro do container
> - `-e PGADMIN_SERVER_JSON_FILE="/pgadmin4/servers.json"`: Indica ao pgAdmin para usar nosso arquivo de configuração
> - `-d`: Executa o container em segundo plano (modo detached)

---

## 🔧 6. Build e execução da API

Após a construção da imagem da API, certifique-se de que o container esteja em execução:

```bash
docker start hands-on-container
```

> **O que está acontecendo:** Estamos iniciando o container da API caso ele esteja parado. Isso é útil para retomar o trabalho sem precisar recriar o container.

---

## 📘️ 7. Criar estrutura de plano de contas

Baixe o arquivo de criação da tabela:

📄 [`table_chart_of_accounts.sql`](Scripts/table_chart_of_accounts.sql)

```bash
docker cp Scripts/table_chart_of_accounts.sql Postgres:/table_chart_of_accounts.sql
docker exec -it Postgres psql -U postgresuser -d accounts -f /table_chart_of_accounts.sql
```

> **O que está acontecendo:** Estamos executando dois comandos:
> 1. `docker cp`: Copia o arquivo SQL do seu computador para dentro do container PostgreSQL
> 2. `docker exec`: Executa o comando `psql` dentro do container para rodar o script SQL
>
> O script cria a estrutura da tabela `ChartOfAccounts` com todos os campos, índices e constraints necessários para o funcionamento da aplicação. Esta tabela almacenará todas as contas do plano contábil.

---

## 🗕️ 8. Inserir dados iniciais

Baixe e copie o arquivo de inserção:

📄 [`insert_data_chart_of_accounts.sql`](Scripts/insert_data_chart_of_accounts.sql)

```bash
docker cp Scripts/insert_data_chart_of_accounts.sql Postgres:/insert_data_chart_of_accounts.sql
docker exec -it Postgres psql -U postgresuser -d accounts -f /insert_data_chart_of_accounts.sql
```

> **O que está acontecendo:** Similar ao passo anterior, estamos:
> 1. Copiando um arquivo SQL para o container
> 2. Executando-o para inserir dados iniciais no banco
>
> Este script popula a tabela com um conjunto básico de contas contábeis, criando uma estrutura hierárquica inicial para o plano de contas. Isso permite que você comece a usar a API imediatamente, sem precisar criar manualmente todas as contas necessárias.

---

## 🌍 9. Acessar pgAdmin

Acesse pelo navegador:
```
http://localhost:8090
```

Entre com as credenciais definidas:
- **Email**: user@yourmail.com
- **Senha**: user1234

> **O que está acontecendo:** Estamos acessando a interface web do pgAdmin, uma ferramenta de administração para PostgreSQL. Aqui você pode visualizar, editar e gerenciar visualmente seu banco de dados.

---

## 🔗 10. Acessar Swagger da API

Após subir a aplicação, acesse pelo navegador:
```
http://localhost:8080/swagger
```

> **O que está acontecendo:** Estamos acessando a documentação interativa da API gerada pelo Swagger/OpenAPI. Esta interface permite:
> 1. Visualizar todos os endpoints disponíveis na API
> 2. Entender os parâmetros e formatos de dados aceitos por cada endpoint
> 3. Testar as chamadas diretamente no navegador
> 4. Explorar os modelos de dados utilizados pela aplicação
>
> O Swagger é uma ferramenta essencial para entender e testar a API sem precisar de ferramentas adicionais como Postman ou Insomnia.

> **Importante:** O `tenantId` define o escopo dos dados que serão acessados e manipulados. Cada tenant tem seu próprio conjunto isolado de contas contábeis.

---

## 🔑 11. Autenticando na API

Para utilizar a API, você precisa primeiro obter um token JWT usando um dos três tenants disponíveis:

1. Acesse o Swagger: `http://localhost:8080/swagger`
2. Expanda o endpoint **POST** `/api/v1/auth/token`
3. Clique em "Try it out"
4. Insira um dos seguintes conjuntos de credenciais de tenant:

**Tenant 1:**
```json
{ 
  "username": "tenant_1", 
  "password": "secret1", 
  "tenantId": "5e0d1c8a-1000-4000-b000-000000000001" 
} 
```

**Tenant 2:**
```json
{ 
  "username": "tenant_2", 
  "password": "secret2", 
  "tenantId": "5e0d1c8a-1000-4000-b000-000000000002" 
} 
```

**Tenant 3:**
```json
{ 
  "username": "tenant_3", 
  "password": "secret3", 
  "tenantId": "5e0d1c8a-1000-4000-b000-000000000003" 
} 
```

5. Clique em "Execute" para realizar a requisição e obter o token JWT
6. Copie o token gerado
7. Clique no botão "Authorize" no topo da página do Swagger
8. Cole o token no campo "Value" (prefixado com "Bearer ")
9. Clique em "Authorize"

Agora todas as suas requisições serão autenticadas e os dados serão filtrados pelo tenant especificado.

> **O que está acontecendo:** A API implementa um sistema de autenticação baseado em JWT (JSON Web Token) com multitenancy. Ao autenticar com um tenant específico, você terá acesso apenas aos dados desse tenant. Isso permite que a mesma API seja usada por diferentes organizações ou departamentos sempre