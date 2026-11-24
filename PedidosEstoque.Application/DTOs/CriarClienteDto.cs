using System.ComponentModel.DataAnnotations;

namespace PedidosEstoque.Application.DTOs;
public class CriarClienteDto
{
    [Required(ErrorMessage = "O nome do cliente é obrigatório.")]
    [StringLength(150, MinimumLength = 2, ErrorMessage = "O nome deve ter entre 2 e 150 caracteres.")]
    public string Nome {get; set;} = string.Empty;

    [Required(ErrorMessage = "O email é obrigatório.")]
    [EmailAddress(ErrorMessage = " O email informado não é valido.")]
    [StringLength(200, ErrorMessage = "O email deve ter no máximo 200 caracteres.")]
    public string Email {get; set;} = string.Empty;

    [StringLength(20, ErrorMessage = "O telefone deve ter no máximo 20 caracteres.")]
    public string? Telefone {get; set;} 
}