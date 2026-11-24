using Microsoft.EntityFrameworkCore;
using PedidosEstoque.Domain.Entities;
using PedidosEstoque.Domain.Interfaces;
using PedidosEstoque.Infrastructure.Data;

namespace PedidosEstoque.Infrastructure.Repositories;

public class PedidoRepository : IPedidoRepository
{
    private readonly AppDbContext _context;
    public PedidoRepository(AppDbContext context)
    {
        _context = context;
    }
 public async Task<IEnumerable<Pedido>> ObterTodosAsync()
    {
        return await _context.Pedidos
            .AsNoTracking()
            .Include(p => p.Cliente)
            .Include(p => p.Itens)
                .ThenInclude(i => i.Produto)
            .OrderByDescending(p => p.DataPedido)
            .ToListAsync();
    }

    public async Task<Pedido?> ObterPorIdAsync(int id)
    {
        return await _context.Pedidos
            .AsNoTracking()
            .Include(p => p.Cliente)
            .Include(p => p.Itens)
            .ThenInclude(i => i.Produto)
            .FirstOrDefaultAsync(p => p.Id == id);
    }

  public async Task<Pedido> AdicionarAsync(Pedido pedido)
    {
        _context.Pedidos.Add(pedido);
        await _context.SaveChangesAsync();
        return pedido;
    }
}