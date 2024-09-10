# ComexAPI

## Índice

- [Sobre o Projeto](#sobre-o-projeto)
- [Tecnologias Utilizadas](#tecnologias-utilizadas)
- [Deploy e Endpoints Disponíveis](#Deploy-e-Endpoints-Disponíveis)
- [Pré-requisitos](#pré-requisitos)
- [Configuração do Ambiente](#configuração-do-ambiente)
- [Instalação](#instalação)
- [Rodando a Aplicação](#rodando-a-aplicação)
- [Endpoints da API](#endpoints-da-api)
- [Documentação da API](#documentação-da-api)

---

## Sobre o Projeto 🚀

**ComexAPI** é uma aplicação backend desenvolvida com **ASP.NET Core** que permite a gestão de produtos, clientes e pedidos. A API fornece endpoints para operações CRUD (Criar, Ler, Atualizar, Deletar).

### Funcionalidades

- **Gerenciamento de Produtos**: Cadastro, listagem, edição e remoção de produtos.
- **Documentação com Swagger**.

---

## Tecnologias Utilizadas 🛠️

As principais tecnologias e bibliotecas utilizadas no projeto incluem:

- **.NET Core 8.0**
- **Entity Framework Core** para interação com o banco de dados.
- **PostgreSQL** como banco de dados.
- **Swagger** para documentação da API.
- **AutoMapper** para mapeamento de objetos.

---

## Deploy e Endpoints Disponíveis 🌐

- Está aplicação tem uma versão que já foi realizado o deploy.<br>
O deploy foi realizado na [Render](https://render.com/) ulilizando a o plano gratuito, por ser o plano gratuito a instância é compatilhada, então as vezes quando não tem acesso o link fica hibernando, então quando acessar e ficar carregando lento, é só aguardar que a vai carregar. 
- Caso queira visualizar os endpoints assim como testar, aceese o link abaixo:

```bash
https://comexapi.onrender.com/swagger/index.html
```
- o Deploy dessa aplicação foi feito ultilizando GitHub Actions. A cada vez que há um commit novo na branch main é relaizao o deploy.
---

## Pré-requisitos 📋

Antes de começar, certifique-se de ter o seguinte instalado em seu ambiente:

- [.NET SDK](https://dotnet.microsoft.com/download) (versão 6.0 ou superior)
- [PostgreSQL](https://www.postgresql.org/) configurado
- [Git](https://git-scm.com/) para controle de versão

---

## Configuração do Ambiente ⚙️

1. Clone o repositório:

   ```bash
   https://github.com/WillianMedeiros14/ComexApi.git
   cd ComexApi
   ```

2. Configure o arquivo appsettings.json com a sua string de conexão para o banco de dados:

   ```bash
    {
        "ConnectionStrings": {
            "DefaultConnection": "Server=seu_servidor;Database=sua_base_de_dados;User Id=seu_usuario;Password=sua_senha;"
        }
    }

   ```

## Instalação 🔧

1. Restaurar dependências:

   ```bash
   dotnet restore
   ```

2. Compilar o projeto:

   ```bash
   dotnet build
   ```

3. Aplicar as migrações:

   ```bash
   dotnet ef database update
   ```

## Rodando a Aplicação ▶️

1. Para rodar a aplicação, use o comando:

   ```bash
   dotnet run
   ```

2. A API estará disponível em http://localhost:5086

### Uso do docker

1. O docker é ultilizado apenas para processo de deploy, desconsiderar para uso local

## Endpoints da API 📡

Os endpoints disponíveis podem ser visualizados na url abaixo:

```bash
https://comexapi.onrender.com/swagger/index.html
```

## Documentação da API 📚

A documentação da API é gerada automaticamente utilizando o Swagger. Após rodar o projeto, você pode acessar a documentação interativa em:

```bash
 http://localhost:5086/swagger
```
