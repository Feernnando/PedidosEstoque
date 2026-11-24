using PedidosEstoque.Application.DTOs;
using PedidosEstoque.Application.Exceptions;
using PedidosEstoque.Application.Interfaces;
using PedidosEstoque.Domain.Entities;
using PedidosEstoque.Domain.Interfaces;

namespace PedidosEstoque.Application.Services;
public class PedidoService : IPedidoService
{
    private readonly IPedidoRepository _pedidoRepository;
    private readonly IClienteRepository _clienteRepository;
    private readonly IProdutoRepository _produtoRepository;

    public PedidoService(IPedidoRepository pedidoRepository, IClienteRepository clienteRepository, IProdutoRepository produtoRepository)
    {
        _pedidoRepository = pedidoRepository;
        _clienteRepository = clienteRepository;
        _produtoRepository = produtoRepository;
    }

    public async Task<IEnumerable<PedidoDto>> ObterTodosAsync()
    {
        var pedidos = await _pedidoRepository.ObterTodosAsync();
        return pedidos.Select(Mapear);
    }
    public async Task<PedidoDto?> ObterPorIdAsync(int id)
    {
        var pedido = await _pedidoRepository.ObterPorIdAsync(id);
        return pedido is null ? null : Mapear(pedido);
        
    }

     public async Task<PedidoDto> CriarAsync(CriarPedidoDto dto)
    {
        var cliente = await _clienteRepository.ObterPorIdAsync(dto.ClienteId);

        if (cliente is null)
            throw new RegraDeNegocioException("Cliente não encontrado.");

        // Junta itens repetidos: o mesmo produto duas vezes vira uma só quantidade somada
        var quantidadesPorProduto = dto.Itens
            .GroupBy(i => i.ProdutoId)
            .ToDictionary(g => g.Key, g => g.Sum(i => i.Quantidade));

        // 1ª passada: só valida, sem alterar nada
        var produtos = new List<Produto>();

        foreach (var (produtoId, quantidade) in quantidadesPorProduto)
        {
            var produto = await _produtoRepository.ObterPorIdAsync(produtoId);

            if (produto is null)
                throw new RegraDeNegocioException($"Produto {produtoId} não encontrado.");

            if (produto.QuantidadeEmEstoque < quantidade)
                throw new ConflitoException(
                    $"Estoque insuficiente para '{produto.Nome}'. " +
                    $"Disponível: {produto.QuantidadeEmEstoque}, solicitado: {quantidade}.");

            produtos.Add(produto);
        }

        // 2ª passada: tudo validado, agora debita o estoque e monta o pedido
        var pedido = new Pedido { ClienteId = cliente.Id };

        foreach (var produto in produtos)
        {
            var quantidade = quantidadesPorProduto[produto.Id];

            produto.QuantidadeEmEstoque -= quantidade;

            pedido.Itens.Add(new ItemPedido
            {
                ProdutoId = produto.Id,
                Quantidade = quantidade,
                PrecoUnitario = produto.Preco
            });
        }

        // Um único SaveChanges: grava o pedido, os itens e o novo estoque na mesma transação
        await _pedidoRepository.AdicionarAsync(pedido);

        var criado = await _pedidoRepository.ObterPorIdAsync(pedido.Id);
        return Mapear(criado!);
    }

  private static PedidoDto Mapear(Pedido pedido)
    {
        return new PedidoDto
        {
            Id = pedido.Id,
            DataPedido = pedido.DataPedido,
            Status = pedido.Status.ToString(),
            ClienteId = pedido.ClienteId,
            ClienteNome = pedido.Cliente?.Nome ?? string.Empty,
            Itens = pedido.Itens.Select(i => new ItemPedidoDto
            {
                ProdutoId = i.ProdutoId,
                ProdutoNome = i.Produto?.Nome ?? string.Empty,
                Quantidade = i.Quantidade,
                PrecoUnitario = i.PrecoUnitario
            }).ToList(),
            ValorTotal = pedido.ValorTotal
        };
    }
}
    