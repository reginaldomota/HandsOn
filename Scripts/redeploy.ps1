# Caminho absoluto do .env (ajustado para rodar dentro da pasta /Scripts)
$envFilePath = Join-Path $PSScriptRoot "..\.env"

Write-Host "Carregando variáveis do .env de $envFilePath..."

# Lê o .env e exporta variáveis no ambiente atual
Get-Content $envFilePath | ForEach-Object {
    if ($_ -match "^\s*([^#][^=]+)=(.+)$") {
        $name = $matches[1].Trim()
        $value = $matches[2].Trim()
        [System.Environment]::SetEnvironmentVariable($name, $value)
    }
}

Write-Host "Parando e removendo container antigo..."
docker stop hands-on-dev-container 2>$null
docker rm hands-on-dev-container 2>$null

Write-Host "Construindo nova imagem da API..."
docker build -t hands-on-dev-api ..  # Dockerfile está na raiz

Write-Host "Criando container com variáveis de ambiente..."
docker run -d `
    -p 8080:8080 `
    --network container_network `
    --name hands-on-dev-container `
    --env POSTGRES_HOST=$env:POSTGRES_HOST `
    --env POSTGRES_PORT=$env:POSTGRES_PORT `
    --env POSTGRES_DB=$env:POSTGRES_DB `
    --env POSTGRES_USER=$env:POSTGRES_USER `
    --env POSTGRES_PASSWORD=$env:POSTGRES_PASSWORD `
    hands-on-dev-api

Write-Host "Deploy finalizado com sucesso."
