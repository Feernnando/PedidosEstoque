using PedidosEstoque.Application.DTOs;

namespace PedidosEstoque.Application.Interfaces;

public interface IProdutoService
{
    Task<IEnumerable<ProdutoDto>> ObterTodosAsync();
    Task<ProdutoDto?> ObterPorIdAsync(int id);
    Task<ProdutoDto> CriarAsync(CriarProdutoDto criarProdutoDto);
    Task<ProdutoDto?> AtualizarAsync(int id, AtualizarProdutoDto atualizarProdutoDto);
    Task<bool> RemoverAsync(int id);
}