using Microsoft.AspNetCore.Mvc;
using PedidosEstoque.Application.DTOs;
using PedidosEstoque.Application.Exceptions;
using PedidosEstoque.Application.Interfaces;

namespace PedidosEstoque.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PedidosController : ControllerBase
{
    private readonly IPedidoService _pedidoService;

    public PedidosController(IPedidoService pedidoService)
    {
        _pedidoService = pedidoService;
    }

    [HttpGet]
    public async Task<IActionResult> ObterTodos()
    {
        var pedidos = await _pedidoService.ObterTodosAsync();
        return Ok(pedidos);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> ObterPorId(int id)
    {
        var pedido = await _pedidoService.ObterPorIdAsync(id);

        if (pedido is null)
            return NotFound();

        return Ok(pedido);
    }

    [HttpPost]
    public async Task<IActionResult> Criar([FromBody] CriarPedidoDto dto)
    {
        try
        {
            var criado = await _pedidoService.CriarAsync(dto);
            return CreatedAtAction(nameof(ObterPorId), new { id = criado.Id }, criado);
        }
        catch (ConflitoException ex)
        {
            return Conflict(new { mensagem = ex.Message });
        }
        catch (RegraDeNegocioException ex)
        {
            return UnprocessableEntity(new { mensagem = ex.Message });
        }
    }
}