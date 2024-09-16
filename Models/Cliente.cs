using System.ComponentModel.DataAnnotations;

namespace ComexAPI.Models;

public class Cliente 
{
    [Key]
    [Required]
    public int Id { get; set; }

    [Required(ErrorMessage = "O campo de nome é obrigatório.")]
    public string Nome { get; set; }

    [Required(ErrorMessage = "O campo de CPF é obrigatório.")]
    public string CPF { get; set; }

    [Required(ErrorMessage = "O campo de Email é obrigatório.")]
    public string Email { get; set; }

    [Required(ErrorMessage = "O campo de Profissão é obrigatório.")]
    public string Profissao { get; set; }

    [Required(ErrorMessage = "O campo de Telefone é obrigatório.")]
    public string Telefone { get; set; }

    
}
