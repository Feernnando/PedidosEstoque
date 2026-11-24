using System.ComponentModel.DataAnnotations;

namespace PedidosEstoque.Application.DTOs;

public class CriarPedidoDto
{
    [Range(1, int.MaxValue, ErrorMessage = "O cliente do pedido é obrigatório.")]
    public int ClienteId { get; set; }

    [Required(ErrorMessage = "O pedido precisa de itens.")]
    [MinLength(1, ErrorMessage = "O pedido precisa ter pelo menos um item.")]
    public List<CriarItemPedidoDto> Itens { get; set; } = new();
}