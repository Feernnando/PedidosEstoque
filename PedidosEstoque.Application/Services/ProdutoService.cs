using PedidosEstoque.Application.Interfaces;
using PedidosEstoque.Domain.Interfaces;
using PedidosEstoque.Domain.Entities;
using PedidosEstoque.Application.DTOs;

namespace PedidosEstoque.Application.Services;

public class ProdutoService : IProdutoService
{
    private readonly IProdutoRepository _produtoRepository;
    public ProdutoService(IProdutoRepository produtoRepository)
    {
        _produtoRepository = produtoRepository;
    }

    public async Task<IEnumerable<ProdutoDto>> ObterTodosAsync()
    {
        var produtos = await _produtoRepository.ObterTodosAsync();

        return produtos.Select(p => new ProdutoDto
        {
            Id = p.Id,
            Nome = p.Nome,
            Descricao = p.Descricao,
            Preco = p.Preco,
            QuantidadeEmEstoque = p.QuantidadeEmEstoque
        });
    }

    public async Task<ProdutoDto?> ObterPorIdAsync(int id)
    {
        var produto = await _produtoRepository.ObterPorIdAsync(id);

        if (produto is null)
            return null;

        {
            return new ProdutoDto
            {
                Id = produto.Id,
                Nome = produto.Nome,
                Descricao = produto.Descricao,
                Preco = produto.Preco,
                QuantidadeEmEstoque = produto.QuantidadeEmEstoque
            };
        }
    }

    public async Task<ProdutoDto> CriarAsync(CriarProdutoDto criarProdutoDto)
    {
        var produto = new Produto
        {
            Nome = criarProdutoDto.Nome,
            Descricao = criarProdutoDto.Descricao,
            Preco = criarProdutoDto.Preco,
            QuantidadeEmEstoque = criarProdutoDto.QuantidadeEmEstoque
        };

        var produtoCriado = await _produtoRepository.AdicionarAsync(produto);

        return new ProdutoDto
        {
            Id = produtoCriado.Id,
            Nome = produtoCriado.Nome,
            Descricao = produtoCriado.Descricao,
            Preco = produtoCriado.Preco,
            QuantidadeEmEstoque = produtoCriado.QuantidadeEmEstoque
        };
    }

    public async Task<ProdutoDto?> AtualizarAsync(int id, AtualizarProdutoDto atualizarProdutoDto)
    {
        var produto = await _produtoRepository.ObterPorIdAsync(id);

        if (produto is null)
            return null;

        produto.Nome = atualizarProdutoDto.Nome;
        produto.Descricao = atualizarProdutoDto.Descricao;
        produto.Preco = atualizarProdutoDto.Preco;
        produto.QuantidadeEmEstoque = atualizarProdutoDto.QuantidadeEmEstoque;

        await _produtoRepository.AtualizarAsync(produto);

        return new ProdutoDto
        {
            Id = produto.Id,
            Nome = produto.Nome,
            Descricao = produto.Descricao,
            Preco = produto.Preco,
            QuantidadeEmEstoque = produto.QuantidadeEmEstoque
        };
    }

    public async Task<bool> RemoverAsync(int id)
    {
        return await _produtoRepository.RemoverAsync(id);
    }
}