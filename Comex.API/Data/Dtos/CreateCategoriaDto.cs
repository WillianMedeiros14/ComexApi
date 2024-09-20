using System.ComponentModel.DataAnnotations;
namespace ComexAPI.Data.Dtos
{
    public class CreateCategoriaDto
    {
        [Required(ErrorMessage = "O campo de nome é obrigatório.")]
        public string Nome { get; set; }
    }
}