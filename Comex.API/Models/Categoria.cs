using System.ComponentModel.DataAnnotations;

namespace ComexAPI.Models;

public class Categoria
{
    [Key]
    [Required]
    public int Id { get; set; }

    [Required(ErrorMessage = "O campo de nome é obrigatório.")]
    public string Nome { get; set; }

    public virtual ICollection<Produto> Produtos { get; set; }
}
