using System.ComponentModel.DataAnnotations;

namespace PedidosEstoque.Application.DTOs;

public class CriarItemPedidoDto
{
    [Range(1, int.MaxValue, ErrorMessage = "O produto do item é obrigatório.")]
    public int ProdutoId { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "A quantidade deve ser de pelo menos 1.")]
    public int Quantidade { get; set; }
}