using System.Net;
using System.Net.Http.Json;
using Xunit;
using ComexAPI.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using ComexAPI.Data.Dtos;

namespace ComexAPI.Integration.Test
{
    public class Produto_POST : IClassFixture<ComexAPIWebApplicationFactory>
    {
        private readonly ComexAPIWebApplicationFactory app;

        public Produto_POST(ComexAPIWebApplicationFactory app)
        {
            this.app = app;
        }

        [Fact]
        public async Task Cadastra_Produto()
        {
            // Arrange
            using var client = app.CreateClient();
            var categoria = new Categoria()
            {
                Nome = "Alimentos"
            };
            var categoriaResponse = await client.PostAsJsonAsync("/Categoria", categoria);
            var categoriaContent = await categoriaResponse.Content.ReadAsStringAsync();
            var categoriaCriada = JsonConvert.DeserializeObject<Categoria>(categoriaContent);

            var produto = new Produto()
            {
                Nome = "Farinha",
                Descricao = "Farinha de MAUÉS",
                PrecoUnitario = 10.50f,
                Quantidade = 5,
                CategoriaId = categoriaCriada.Id
            };

            // Act
            var response = await client.PostAsJsonAsync("/Produto", produto);

            // Assert
            Assert.NotNull(response);
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);

            var responseBody = await response.Content.ReadAsStringAsync();
            var produtoCriado = JsonConvert.DeserializeObject<ProdutoResponseDto>(responseBody);

            Assert.NotNull(produtoCriado);
            Assert.Equal(produto.Nome, produtoCriado.Nome);
            Assert.Equal(produto.CategoriaId, produtoCriado.CategoriaId);

        }

        [Fact]
        public async Task Cadastra_Produto_Invalido_Retorna_Validacao_Mensagens()
        {
            // Arrange
            using var client = app.CreateClient();

            var categoria = new Categoria()
            {
                Nome = "Alimentos"
            };
            var categoriaResponse = await client.PostAsJsonAsync("/Categoria", categoria);
            var categoriaContent = await categoriaResponse.Content.ReadAsStringAsync();
            var categoriaCriada = JsonConvert.DeserializeObject<Categoria>(categoriaContent);

            var produtoInvalido = new Produto()
            {
                Nome = "",
                Descricao = "",
                PrecoUnitario = -10.50f,
                Quantidade = -5,
                CategoriaId = categoriaCriada.Id
            };

            // Act
            var response = await client.PostAsJsonAsync("/Produto", produtoInvalido);

            // Assert
            Assert.NotNull(response);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

            var responseBody = await response.Content.ReadAsStringAsync();
            var errorResponse = JsonConvert.DeserializeObject<ValidationErrorResponse>(responseBody);

            Assert.NotNull(errorResponse);
            Assert.True(errorResponse.Errors.ContainsKey("Nome"));
            Assert.Contains("O Nome do produto é obrigatório", errorResponse.Errors["Nome"]);

            Assert.True(errorResponse.Errors.ContainsKey("PrecoUnitario"));
            Assert.Contains("O preço deve ser maior que 0", errorResponse.Errors["PrecoUnitario"]);

            Assert.True(errorResponse.Errors.ContainsKey("Quantidade"));
            Assert.Contains("A quantidade deve ser maior que 0", errorResponse.Errors["Quantidade"]);
        }
    }
}
