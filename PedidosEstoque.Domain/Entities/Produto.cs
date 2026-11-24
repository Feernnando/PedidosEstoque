namespace PedidosEstoque.Domain.Entities;

public class Produto
{
    public int Id {get; set;}
    public string Nome{get; set;} = string.Empty;
    public string? Descricao {get; set; }
    public decimal Preco {get; set;}
    public int QuantidadeEmEstoque {get; set;}
    public DateTime DataCriacao {get; set;}
}