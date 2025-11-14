using Comandas.Api.Models;

namespace Comandas.Api.DTOs
{
    public class ComandaUpdateRequest
    {
        public string? NomeCliente { get; set; }
        public int NumeroMesa { get; set; }
        public ComandaItemUpdateRequest[] Itens { get; set; } = [];
    }

    public class ComandaItemUpdateRequest
    {
        public int id { get; set; } // id da comanda item
        public bool Remove { get; set; } // indica se o item deve ser removido
        public int CardapioItemId { get; set; } // id do item do cardapio
    }
}
