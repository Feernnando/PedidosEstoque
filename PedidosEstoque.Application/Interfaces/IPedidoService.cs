using PedidosEstoque.Application.DTOs;

namespace PedidosEstoque.Application.Interfaces;

public interface IPedidoService
{
    Task<IEnumerable<PedidoDto>> ObterTodosAsync();
    Task<PedidoDto?> ObterPorIdAsync(int id);
    Task<PedidoDto> CriarAsync(CriarPedidoDto dto);
}