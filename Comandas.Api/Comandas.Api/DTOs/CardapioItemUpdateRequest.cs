namespace Comandas.Api.DTOs
{
    public class CardapioItemUpdateRequest
    {   
        public string Nome { get; set; } = default!;
        public string Descricao { get; set; } = default!;
        public decimal Preco { get; set; }
        public bool PossuiPreparo { get; set; }
        public int? CategoriaCardapioId { get; set; }
    }
}
