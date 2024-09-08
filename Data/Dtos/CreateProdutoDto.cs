using System.ComponentModel.DataAnnotations;

namespace ComexAPI.Data.Dtos;

public class CreateProdutoDto
{
    [Required(ErrorMessage = "O Nome do produto é obrigatório")]
    [MaxLength(100, ErrorMessage = "O tamanho do nome não pode exceder 100 caracteres")]
    public string Nome { get; set; }

    [MaxLength(500, ErrorMessage = "A descrição não pode exceder 500 caracteres")]
    public string Descricao { get; set; }

    [Required(ErrorMessage = "O Preço do produto é obrigatório")]
    [Range(0.01, double.MaxValue, ErrorMessage = "O preço deve ser maior que 0")]
    public float PrecoUnitario { get; set; }

    [Required(ErrorMessage = "A quantidade do produto é obrigatório")]
    [Range(1, int.MaxValue, ErrorMessage = "A quantidade deve ser maior que 0")]
    public int Quantidade { get; set; }
}