namespace ComexAPI.Data.Dtos
{
    public class UpdateEnderecoDto
    {
        public string Bairro { get; set; }

        public string Cidade { get; set; }

        public string Complemento { get; set; }

        public string Estado { get; set; }

        public string Rua { get; set; }

        public int Numero { get; set; }
    }
}