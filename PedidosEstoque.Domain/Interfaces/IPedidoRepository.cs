using PedidosEstoque.Domain.Entities;

namespace PedidosEstoque.Domain.Interfaces;
public interface IPedidoRepository
{
    Task<IEnumerable<Pedido>> ObterTodosAsync();
    
    Task<Pedido?> ObterPorIdAsync(int id);
    
    Task<Pedido> AdicionarAsync(Pedido pedido);
}