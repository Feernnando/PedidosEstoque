namespace PedidosEstoque.Application.DTOs
{
    public class PedidoDto
    {
        public int Id {get; set;}
        public DateTime DataPedido {get; set;}
        public string Status {get; set;} = string.Empty;
        public int ClienteId {get; set;}
        public string ClienteNome {get; set;} = string.Empty;
        public List<ItemPedidoDto> Itens {get; set;} = new();
        public decimal ValorTotal {get; set;}
    }
}