using PedidosEstoque.Application.DTOs;

namespace PedidosEstoque.Application.Interfaces;
public interface IClienteService
{
    Task<IEnumerable<ClienteDto>> ObterTodosAsync();
    Task<ClienteDto?> ObterPorIdAsync(int id);
    Task<ClienteDto> CriarAsync(CriarClienteDto criarClienteDto);
    Task<ClienteDto?> AtualizarAsync(int id, AtualizarClienteDto atualizarClienteDto);
    Task<bool> RemoverAsync(int id);
}