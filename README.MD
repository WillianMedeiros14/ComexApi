# ComexAPI

## Índice

- [Sobre o Projeto](#sobre-o-projeto)
- [Tecnologias Utilizadas](#tecnologias-utilizadas)
- [Deploy e Endpoints Disponíveis](#Deploy-e-Endpoints-Disponíveis)
- [Pré-requisitos](#pré-requisitos)
- [Configuração do Ambiente](#configuração-do-ambiente)
- [Instalação](#instalação)
- [Rodando a Aplicação](#rodando-a-aplicação)
- [Testes Automatizados](#Testes-automatizados)
- [Endpoints da API](#endpoints-da-api)
- [Documentação da API](#documentação-da-api)

---

## Sobre o Projeto 🚀

**ComexAPI** é uma aplicação backend desenvolvida com **ASP.NET Core** que permite a gestão de produtos, clientes e pedidos. A API fornece endpoints para operações CRUD (Criar, Ler, Atualizar, Deletar).

### Funcionalidades

- **Gerenciamento de Produtos**: Cadastro, listagem, edição e remoção de produtos.
- **Gerenciamento de Clientes**: Cadastro, listagem, edição e remoção de clientes.
- **Gerenciamento de Endereços**: Cadastro, listagem, edição e remoção de enderecos.
- **Gerenciamento de Categoria**: Cadastro, listagem, edição e remoção de categoria.
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

## Instalação 🔧

1. Restaurar dependências:

   ```bash
   dotnet restore
   ```

2. Compilar o projeto:

   ```bash
   dotnet build
   ```

3. Setar as variáveis de ambiente:

- Para rodar a aplicação em modo de desenvolvimento, use o comando:

- Prompt de Comando ou o PowerShell:<br>
  altere o valor de porta_postegre e suasenha

  ```bash
   set ASPNETCORE_ENVIRONMENT=Development
   set DATABASE_URL="Server=localhost;Port=porta_postegre;Database=produtos;Username=postgres;Password=suasenha"

  ```

- No Linux/macOS:<br>
  altere o valor de porta_postegre e suasenha

  ```bash
   export ASPNETCORE_ENVIRONMENT=Development
   export DATABASE_URL="Server=localhost;Port=porta_postegre;Database=produtos;Username=postgres;Password=suasenha"

  ```

4. Aplicar as migrações:

   ```bash
   dotnet ef database update
   ```

## Rodando a Aplicação ▶️

- Execute o comando abaixo:

  ```bash
  dotnet run
  ```

2. A API estará disponível em http://localhost:5086

## Testes Automatizados 🧪

Os testes automatizados foram configurados para garantir que algumas das principais funcionalidades da ComexAPI estejam funcionando corretamente. Neste projeto, existem testes de integração que podem ser executados de maneira simples utilizando o Docker.

1. Certifique-se de que o [Docker](https://docs.docker.com/get-started/get-docker/) esteja instalado e em execução no seu ambiente.
2. Para rodar os testes no [Visual Studio](https://visualstudio.microsoft.com/pt-br/vs/community/), abra o Gerenciador de Testes e execute todos os testes ou os específicos que desejar.
3. Se preferir rodar os testes em algum outro editor pela linha de comando, como o [Visual Studio Code](https://code.visualstudio.com/Download) por exemplo, entre na pasta "ComexAPI.Integration.Test" pelo terminal, e execute o seguinte comando:

```bash
 dotnet test
```

### Uso do docker

1. O docker é ultilizado apenas para processo de deploy e testes altomatizados, desconsiderar para outro uso.

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
