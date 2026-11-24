using PedidosEstoque.Application.DTOs;
using PedidosEstoque.Application.Exceptions;
using PedidosEstoque.Application.Interfaces;
using PedidosEstoque.Domain.Entities;
using PedidosEstoque.Domain.Interfaces;

namespace PedidosEstoque.Application.Services;

public class ClienteService : IClienteService
{
    private readonly IClienteRepository _clienteRepository;

    public ClienteService(IClienteRepository clienteRepository)
    {
        _clienteRepository = clienteRepository;
    }

    public async Task<IEnumerable<ClienteDto>> ObterTodosAsync()
    {
        var clientes = await _clienteRepository.ObterTodosAsync();
        return clientes.Select(Mapear);
    }

    public async Task<ClienteDto?> ObterPorIdAsync(int id)
    {
        var cliente = await _clienteRepository.ObterPorIdAsync(id);
        return cliente is null ? null : Mapear(cliente);
    }

    public async Task<ClienteDto> CriarAsync(CriarClienteDto dto)
    {
        var email = dto.Email.Trim();

        if (await _clienteRepository.EmailJaExisteAsync(email))
            throw new ConflitoException("Já existe um cliente cadastrado com este e-mail.");

            var cliente = new Cliente
            {
                Nome = dto.Nome.Trim(),
                Email = email,
                Telefone = dto.Telefone?.Trim()
            };

            var criado = await _clienteRepository.AdicionarAsync(cliente);
            return Mapear(criado);
    }

    public async Task<ClienteDto?> AtualizarAsync(int id, AtualizarClienteDto dto)
    {
        var cliente = await _clienteRepository.ObterPorIdAsync(id);

        if (cliente is null)
            return null;

        var email = dto.Email.Trim();

        if (await _clienteRepository.EmailJaExisteAsync(email, ignorarClientId: id))
            throw new ConflitoException("Já existe outrocliente cadastrado com este e-mail.");

            cliente.Nome = dto.Nome.Trim();
            cliente.Email = email;
            cliente.Telefone = dto.Telefone?.Trim();

            await _clienteRepository.AtualizarAsync(cliente);
            return Mapear(cliente);
    }

    public async Task<bool> RemoverAsync(int id)
    {
        var cliente = await _clienteRepository.ObterPorIdAsync(id);

        if (cliente is null)
            return false;

            if (await _clienteRepository.PossuiPedidosAsync(id))
                throw new ConflitoException("Não é possível remover o cliente, pois ele possui pedidos cadastrados.");

            return await _clienteRepository.RemoverAsync(id);
    }

    private static ClienteDto Mapear(Cliente cliente)
    {
        return new ClienteDto
        {
            Id = cliente.Id,
            Nome = cliente.Nome,
            Email = cliente.Email,
            Telefone = cliente.Telefone
        };
    }
}