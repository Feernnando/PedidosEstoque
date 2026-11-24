using System.ComponentModel.DataAnnotations;

namespace PedidosEstoque.Application.DTOs;

public class AtualizarProdutoDto
{
    [Required(ErrorMessage = "O campo Nome é obrigatório.")]
    [StringLength(15, MinimumLength = 2, ErrorMessage = "O nome deve ter entre 2 e 15 caracteres.")]
    public string Nome { get; set; } = string.Empty;

    [StringLength (500, ErrorMessage = "A descrição não pode ter mais de 500 caracteres.")]
    public string? Descricao { get; set; }

    [Range(0.01, double.MaxValue, ErrorMessage = "O preço deve ser maior que zero.")]
    public decimal Preco { get; set; }

    [Range(0, int.MaxValue, ErrorMessage = "O estoque não pode ser negativo.")]
    public int QuantidadeEmEstoque { get; set; }
}