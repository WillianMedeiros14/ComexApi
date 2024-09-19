using System.Net;
using System.Net.Http.Json;
using Xunit;
using ComexAPI.Models;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace ComexAPI.Integration.Test
{
    public class Endereco_GET : IClassFixture<ComexAPIWebApplicationFactory>
    {
        private readonly ComexAPIWebApplicationFactory app;

        public Endereco_GET(ComexAPIWebApplicationFactory app)
        {
            this.app = app;
        }

        [Fact]
        public async Task Recupera_Endereco_Por_Id()
        {
            // Arrange
            using var client = app.CreateClient();
            var enderecoTeste = new Endereco()
            {
                Bairro = "Comunidade Vera Cruz",
                Cidade = "Maués",
                Complemento = "Rua principal",
                Estado = "AM",
                Rua = "Vera Cruz",
                Numero = 123
            };

            var enderecoResponse = await client.PostAsJsonAsync("/Endereco", enderecoTeste);
            Assert.Equal(HttpStatusCode.Created, enderecoResponse.StatusCode);

            var enderecoCriado = await enderecoResponse.Content.ReadFromJsonAsync<Endereco>();
            Assert.NotNull(enderecoCriado);

            var enderecoId = enderecoCriado.Id;

            // Act
            var response = await client.GetFromJsonAsync<Endereco>("/Endereco/" + enderecoId);

            // Assert
            Assert.NotNull(response);
            Assert.Equal(enderecoId, response.Id);
        }

    }
}
