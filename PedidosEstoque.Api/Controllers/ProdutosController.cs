using Microsoft.AspNetCore.Mvc;
using PedidosEstoque.Application.Interfaces;
using PedidosEstoque.Application.DTOs;

namespace PedidosEstoque.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProdutosController : ControllerBase
{
    private readonly IProdutoService _produtoService;

    public ProdutosController(IProdutoService produtoService)
    {
        _produtoService = produtoService;
    }

    [HttpGet]
    public async Task<IActionResult> ObterTodos()
    {
        var produtos = await _produtoService.ObterTodosAsync();
        return Ok(produtos);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> ObterPorId(int id)
    {
        var produto = await _produtoService.ObterPorIdAsync(id);

        if (produto is null)
            return NotFound();

        return Ok(produto);
    }

    [HttpPost]
    public async Task<IActionResult> Criar([FromBody] CriarProdutoDto criarProdutoDto)
    {
        var produtoCriado = await _produtoService.CriarAsync(criarProdutoDto);
        return CreatedAtAction(nameof(ObterPorId), new { id = produtoCriado.Id }, produtoCriado);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Atualizar(int id, [FromBody] AtualizarProdutoDto atualizarProdutoDto)
    {
        var produtoAtualizado = await _produtoService.AtualizarAsync(id, atualizarProdutoDto);

        if (produtoAtualizado is null)
            return NotFound();

        return Ok(produtoAtualizado);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Remover(int id)
    {
        var removido = await _produtoService.RemoverAsync(id);

        if (!removido)
            return NotFound();

        return NoContent();
    }
}