# Use the .NET SDK image for building the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /app

# Copiar o arquivo de projeto
COPY ComexAPI.csproj ./

# Restaurar dependências
RUN dotnet restore

# Copiar o resto dos arquivos da aplicação
COPY . ./

# Construir a aplicação
RUN dotnet build ComexAPI.csproj -c Release -o /app/build

# Publicar a aplicação
RUN dotnet publish ComexAPI.csproj -c Release -o /app/publish

# Use a imagem de runtime para rodar a aplicação
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime

WORKDIR /app

# Copiar os arquivos publicados do estágio de build
COPY --from=build /app/publish .

# Instalar dotnet-ef e aplicar migrações no contêiner de runtime
RUN dotnet tool install --global dotnet-ef && \
    export PATH="$PATH:/root/.dotnet/tools"

# Definir o comando de inicialização
ENTRYPOINT ["sh", "-c", "dotnet ef database update && dotnet ComexAPI.dll"]
