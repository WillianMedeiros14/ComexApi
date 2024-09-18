using System.Net;
using System.Net.Http.Json;
using Xunit;
using ComexAPI.Models;

namespace ComexAPI.Integration.Test
{
    public class Cliente_POST : IClassFixture<ComexAPIWebApplicationFactory>
    {
        private readonly ComexAPIWebApplicationFactory app;

        public Cliente_POST(ComexAPIWebApplicationFactory app)
        {
            this.app = app;
        }

        [Fact]
        public async Task Cadastra_Cliente()
        {
            // Arrange
            using var client = app.CreateClient();

            // Primeiro, criar um endereço
            var endereco = new Endereco
            {
                Bairro = "Centro",
                Rua = "Rua das Flores",
                Numero = 123,
                Cidade = "Maués",
                Estado = "AM",
            };

            var enderecoResponse = await client.PostAsJsonAsync("/Endereco", endereco);
            Assert.Equal(HttpStatusCode.Created, enderecoResponse.StatusCode);

            var enderecoCriado = await enderecoResponse.Content.ReadFromJsonAsync<Endereco>();
            var enderecoId = enderecoCriado.Id;

            var cliente = new Cliente
            {
                Nome = "João Silva",
                CPF = "123.456.789-00",
                Email = "joao.silva@example.com",
                Profissao = "Engenheiro",
                Telefone = "98765-4321",
                EnderecoId = enderecoId 
            };

            // Act
            var response = await client.PostAsJsonAsync("/Cliente", cliente);

            // Assert
            Assert.NotNull(response);
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        }
    }
}
