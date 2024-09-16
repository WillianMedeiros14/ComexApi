using System.ComponentModel.DataAnnotations;
namespace ComexAPI.Data.Dtos
{
    public class CreateEnderecoDto
    {
        [Required(ErrorMessage = "O campo Bairro é obrigatário.")]
        public string Bairro { get; set; }

        [Required(ErrorMessage = "O campo Cidade é obrigatário.")]
        public string Cidade { get; set; }

        public string Complemento { get; set; }

        [Required(ErrorMessage = "O campo Estado é obrigatário.")]
        public string Estado { get; set; }

        [Required(ErrorMessage = "O campo Rua é obrigatário.")]
        public string Rua { get; set; }

        [Required(ErrorMessage = "O campo Número é obrigatário.")]
        public int Numero { get; set; }
    }
}