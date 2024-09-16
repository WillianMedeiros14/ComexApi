namespace ComexAPI.Data.Dtos
{
    public class ReadClienteDto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string CPF { get; set; }
        public string Email { get; set; }
        public string Profissao { get; set; }
        public string Telefone { get; set; }
        public int EnderecoId { get; set; }

        public ReadEnderecoDto Endereco { get; set; }
    }
}