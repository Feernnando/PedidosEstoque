using PedidosEstoque.Domain.Entities;

namespace PedidosEstoque.Domain.Interfaces;

public interface IClienteRepository
{
    Task<IEnumerable<Cliente>> ObterTodosAsync();

    Task<Cliente?> ObterPorIdAsync(int id);

    Task<bool> EmailJaExisteAsync(string email, int? ignorarClientId = null);
    Task<bool> PossuiPedidosAsync(int id);

    Task<Cliente> AdicionarAsync(Cliente cliente);
    Task AtualizarAsync(Cliente cliente);

    Task<bool> RemoverAsync(int id);
}