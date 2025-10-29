using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Comandas.Api.Models
{
    public class PedidoCozinha
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int ComandaItemIdComandaId { get; set; }
        public List<PedidoCozinhaItem> Itens { get; set; } = [];
    }
}
