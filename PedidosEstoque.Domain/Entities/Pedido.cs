namespace PedidosEstoque.Domain.Entities;

public enum StatusPedido
{
    Pendente,
    Confirmado,
    Cancelado,
}

public class Pedido
{
    public int Id {get; set;}
    public DateTime DataPedido {get; set;} = DateTime.Now;
    public StatusPedido Status {get; set;} = StatusPedido.Pendente;

    public int ClienteId {get; set;}
    public Cliente? Cliente {get; set;}

    public ICollection<ItemPedido> Itens {get; set;} = new List<ItemPedido>();

    public decimal ValorTotal => Itens.Sum (i => i.Quantidade * i.PrecoUnitario);
}