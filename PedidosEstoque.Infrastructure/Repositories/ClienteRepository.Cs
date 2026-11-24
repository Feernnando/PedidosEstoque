using Microsoft.EntityFrameworkCore;
using PedidosEstoque.Domain.Entities;
using PedidosEstoque.Domain.Interfaces;
using PedidosEstoque.Infrastructure.Data;

namespace PedidosEstoque.Infrastructure.Repositories;

public class ClienteRepository : IClienteRepository
{
    private readonly AppDbContext _context;

    public ClienteRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Cliente>> ObterTodosAsync()
    {
        return await _context.Clientes.ToListAsync();
    }

    public async Task<Cliente?> ObterPorIdAsync(int id)
    {
        return await _context.Clientes.FindAsync(id);
    }

    public async Task<bool> EmailJaExisteAsync(string email, int? ignorarClienteId = null)
    {
        return await _context.Clientes.AnyAsync(c => c.Email == email && (ignorarClienteId == null || c.Id != ignorarClienteId));
    }

    public async Task<bool> PossuiPedidosAsync(int id)
    {
        return await _context.Pedidos.AnyAsync(p => p.ClienteId == id);
    }

    public async Task<Cliente> AdicionarAsync(Cliente cliente)
    {
        _context.Clientes.Add(cliente);
        await _context.SaveChangesAsync();
        return cliente;
    }

    public async Task AtualizarAsync(Cliente cliente)
    {
        _context.Clientes.Update(cliente);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> RemoverAsync(int id)
    {
        var cliente = await _context.Clientes.FindAsync(id);
        if (cliente is null)
        return false;

        _context.Clientes.Remove(cliente);
        await _context.SaveChangesAsync();
        return true;
    }
}