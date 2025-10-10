namespace Comandas.Api.Models
{
    public class PedidoCozinha
    {
        public int Id { get; set; }
        public int ComandaItemIdComandaId { get; set; }
        public List<PedidoCozinhaItem> Itens { get; set; } = [];
    }
}
