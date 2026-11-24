using System.ComponentModel.DataAnnotations;

namespace PedidosEstoque.Application.DTOs;

public class CriarProdutoDto
{
    [Required (ErrorMessage= "O nome do produto é obrigatório.")]
    [StringLength(150, MinimumLength = 2, ErrorMessage = "O nome deve ter entre 2 e 150 caracteres.")]
    public string Nome {get; set;} = string.Empty;

    [StringLength(500, ErrorMessage = "A descrição não pode ter mais de 500 caracteres.")]
    public string? Descricao {get; set;}

    [Range(0.01, double.MaxValue, ErrorMessage = "O preço deve ser maior que zero .")]
    public decimal Preco {get; set;}

    [Range(0, int.MaxValue, ErrorMessage = "A quantidade em estoque não pode ser negativa.")]
    public int QuantidadeEmEstoque {get; set;}
}