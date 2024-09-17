using System.Net;
using System.Net.Http.Json;
using Xunit;
using ComexAPI.Models;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace ComexAPI.Integration.Test
{
    public class Endereco_POST : IClassFixture<ComexAPIWebApplicationFactory>
    {
        private readonly ComexAPIWebApplicationFactory app;

        public Endereco_POST(ComexAPIWebApplicationFactory app)
        {
            this.app = app;


        }

        [Fact]
        public async Task Cadastra_Endereco_Invalido_Retorna_Validacao_Mensagens()
        {
            // Arrange
            using var client = app.CreateClient();
            var enderecoInvalido = new Endereco()
            {
                Bairro = "",
                Cidade = "",
                Complemento = "",
                Estado = "",
                Rua = "",
                Numero = 2
            };

            // Act
            var response = await client.PostAsJsonAsync("/Endereco", enderecoInvalido);

            // Assert
            Assert.NotNull(response);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

            var responseBody = await response.Content.ReadAsStringAsync();
            var errorResponse = JsonConvert.DeserializeObject<ValidationErrorResponse>(responseBody);

            Assert.NotNull(errorResponse);
            Assert.True(errorResponse.Errors.ContainsKey("Rua"));
            Assert.Contains("O campo Rua é obrigatário.", errorResponse.Errors["Rua"]);

            Assert.True(errorResponse.Errors.ContainsKey("Bairro"));
            Assert.Contains("O campo Bairro é obrigatário.", errorResponse.Errors["Bairro"]);

            Assert.True(errorResponse.Errors.ContainsKey("Cidade"));
            Assert.Contains("O campo Cidade é obrigatário.", errorResponse.Errors["Cidade"]);

            Assert.True(errorResponse.Errors.ContainsKey("Estado"));
            Assert.Contains("O campo Estado é obrigatário.", errorResponse.Errors["Estado"]);
        }


        [Fact]
        public async Task Cadastra_Endereco()
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

            // Act
            var response = await client.PostAsJsonAsync("/Endereco", enderecoTeste);

            // Assert
            Assert.NotNull(response);
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);

            var createdEndereco = await response.Content.ReadFromJsonAsync<Endereco>();
            Assert.NotNull(createdEndereco);
            Assert.True(createdEndereco.Id > 0);

            // Registro dos dados em um arquivo para depuração
            var logFilePath = "test_log.txt";
            var logMessage = $"Dados Cadastrados:\n" +
                             $"ID: {createdEndereco.Id}\n" +
                             $"Bairro: {createdEndereco.Bairro}\n" +
                             $"Cidade: {createdEndereco.Cidade}\n" +
                             $"Complemento: {createdEndereco.Complemento}\n" +
                             $"Estado: {createdEndereco.Estado}\n" +
                             $"Rua: {createdEndereco.Rua}\n" +
                             $"Número: {createdEndereco.Numero}\n";

            await File.WriteAllTextAsync(logFilePath, logMessage);
        }

    }
}
