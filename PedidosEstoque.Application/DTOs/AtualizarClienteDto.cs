using System.ComponentModel.DataAnnotations;

namespace PedidosEstoque.Application.DTOs;

public class AtualizarClienteDto
{
    [Required(ErrorMessage = "O nome do cliente é obrigatório.")]
    [StringLength(150, MinimumLength = 2, ErrorMessage = "O nome deve ter entre 2 e 150 caracteres.")]
    public string Nome { get; set; } = string.Empty;

    [Required(ErrorMessage = "O e-mail é obrigatório.")]
    [EmailAddress(ErrorMessage = "O e-mail informado não é válido.")]
    [StringLength(200, ErrorMessage = "O e-mail não pode ter mais de 200 caracteres.")]
    public string Email { get; set; } = string.Empty;

    [StringLength(20, ErrorMessage = "O telefone não pode ter mais de 20 caracteres.")]
    public string? Telefone { get; set; }
}