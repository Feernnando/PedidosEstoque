namespace PedidosEstoque.Application.DTOs;

public class ItemPedidoDto
{
    public int ProdutoId {get; set;}
    public string ProdutoNome {get; set;} = string.Empty;
    public int Quantidade {get; set;}
    public decimal PrecoUnitario {get; set;}
    public decimal Subtotal => Quantidade * PrecoUnitario;
    
}