using ComexAPI.Models;
using ComexAPI.Integration.Test;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Xunit;
using Microsoft.EntityFrameworkCore;
using System.Net.Http.Json;

namespace ComexAPI.Integration.Test
{
    public class Cliente_DELETE : IClassFixture<ComexAPIWebApplicationFactory>
    {
        private readonly ComexAPIWebApplicationFactory app;

        public Cliente_DELETE(ComexAPIWebApplicationFactory app)
        {
            this.app = app;
        }

        [Fact]
        public async Task Deletar_Cliente_PorId()
        {
            // Arrange
            using var client = app.CreateClient();

            var endereco = new Endereco() { Bairro = "Jardim", Rua = "Avenida Brasil", Numero = 456, Cidade = "Manaus", Estado = "AM" };

            var enderecoResponse = await client.PostAsJsonAsync("/Endereco", endereco);
            Assert.Equal(HttpStatusCode.Created, enderecoResponse.StatusCode);

            var enderecoCriado = await enderecoResponse.Content.ReadFromJsonAsync<Endereco>();
            Assert.NotNull(enderecoCriado);

            var enderecoId = enderecoCriado.Id;

            var cliente = new Cliente() { Nome = "Maria Souza", CPF = "987.654.321-00", Email = "maria.souza@example.com", Profissao = "Arquiteta", Telefone = "91234-5678", EnderecoId = enderecoId };


            var clienteResponse = await client.PostAsJsonAsync("/Cliente", cliente);
            Assert.Equal(HttpStatusCode.Created, clienteResponse.StatusCode);

            var clienteExistente = await clienteResponse.Content.ReadFromJsonAsync<Cliente>();
            Assert.NotNull(clienteExistente);


            
            var response = await client.GetFromJsonAsync<Cliente>("/Cliente/" + clienteExistente.Id);

            // Act
            var responseDelete = await client.DeleteAsync("/Cliente/" + response.Id);


            //Assert
            Assert.NotNull(response);
            Assert.Equal(HttpStatusCode.NoContent, responseDelete.StatusCode);
        }
    }
}
