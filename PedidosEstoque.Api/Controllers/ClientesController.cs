using Microsoft.AspNetCore.Mvc;
using PedidosEstoque.Application.DTOs;
using PedidosEstoque.Application.Interfaces;
using PedidosEstoque.Application.Exceptions;

namespace PedidosEstoque.Api.Controllers;

[ApiController]
[Route("api/[controller]")]

public class ClientesController : ControllerBase
{
    private readonly IClienteService _clienteService;

    public ClientesController(IClienteService clienteService)
    {
        _clienteService = clienteService;
    }

    [HttpGet]
    public async Task<ActionResult> ObterTodos()
    {
        var clientes = await _clienteService.ObterTodosAsync();
        return Ok(clientes);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> ObterPorId(int id)
    {
        var cliente = await _clienteService.ObterPorIdAsync(id);

        if (cliente is null)
            return NotFound();

        return Ok(cliente);
    }

    [HttpPost]
    public async Task<ActionResult> Criar([FromBody] CriarClienteDto dto)
    {
        try
        {
            var criado = await _clienteService.CriarAsync(dto);
            return CreatedAtAction(nameof(ObterPorId), new { id = criado.Id }, criado);
        }
        catch (ConflitoException ex)
        {
            return Conflict(new { message = ex.Message });
        }
    }

    [HttpPut("{id}")]
        public async Task<ActionResult> Atualizar(int id, [FromBody] AtualizarClienteDto dto)
        {
            try
            {
                var atualizado = await _clienteService.AtualizarAsync(id, dto);
                
                if(atualizado is null)
                    return NotFound();

                return Ok(atualizado);
            }
            catch (ConflitoException ex)
            {
                return Conflict(new { message = ex.Message });
            }
        }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Remover(int id)
    {
        try
        {
            var removido = await _clienteService.RemoverAsync(id);

            if (!removido)
                return NotFound();

            return NoContent();
        }
        catch (ConflitoException ex)
        {
            return Conflict(new { message = ex.Message });
        }
    }
}