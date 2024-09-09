# Use a imagem do .NET SDK para construir a aplicação e aplicar migrações
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

# Instalar dotnet-ef e aplicar migrações
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS migrations

WORKDIR /app

COPY --from=build /app/publish .

RUN dotnet tool install --global dotnet-ef && \
    dotnet ef database update

# Use a imagem de runtime para rodar a aplicação
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime

WORKDIR /app

# Copiar os arquivos publicados do estágio de build
COPY --from=build /app/publish .

# Definir o comando de inicialização
ENTRYPOINT ["dotnet", "ComexAPI.dll"]
