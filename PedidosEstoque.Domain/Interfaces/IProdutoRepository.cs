using PedidosEstoque.Domain.Entities;

namespace PedidosEstoque.Domain.Interfaces;

public interface IProdutoRepository
{
    Task<IEnumerable<Produto>> ObterTodosAsync();
    
    Task<Produto?> ObterPorIdAsync(int id);
    
    Task<Produto> AdicionarAsync(Produto produto);
    Task AtualizarAsync(Produto produto);
    
    Task<bool> RemoverAsync(int id);
}