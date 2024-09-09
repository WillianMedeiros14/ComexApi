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

# Use a nova imagem de SDK para instalar ferramentas e aplicar migrações
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS tools

WORKDIR /app

# Copiar os arquivos necessários
COPY --from=build /app/publish ./

# Instalar dotnet-ef globalmente e configurar o PATH
RUN dotnet tool install --global dotnet-ef && \
    export PATH="$PATH:/root/.dotnet/tools" && \
    dotnet ef database update

# Use the .NET ASP.NET runtime image for running the application
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime

WORKDIR /app

# Copiar os arquivos publicados do estágio de build
COPY --from=build /app/publish .

# Definir o comando de inicialização
ENTRYPOINT ["dotnet", "ComexAPI.dll"]
