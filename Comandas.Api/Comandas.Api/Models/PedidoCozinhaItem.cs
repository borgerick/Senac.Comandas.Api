using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Comandas.Api.Models
{
    public class PedidoCozinhaItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int PedidoCozinhaId { get; set; }
        public PedidoCozinha PedidoCozinha { get; set; }
        public int ComandaItemId { get; set; }
        public ComandaItem ComandaItem { get; set; }
    }
}
