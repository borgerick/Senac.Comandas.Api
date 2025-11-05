using Microsoft.EntityFrameworkCore;

namespace Comandas.Api;
public class ComandasDbContext : DbContext
{
    public ComandasDbContext(
        DbContextOptions<ComandasDbContext> options
    ) : base(options)
    { }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Models.Usuario>()
            .HasData(
            new Models.Usuario()
                {
                    Id = 1,
                    Nome = "Admin",
                    Email = "admin!admin.com",
                    Senha = "admin123"
                }
            );
        modelBuilder.Entity<Models.Mesa>()
            .HasData(
            new Models.Mesa()
            {
                Id = 1,
                NumeroMesa = "1",
                SituacaoMesa = 2
            }, new Models.Mesa()
            {
                Id = 2,
                NumeroMesa = "2",
                SituacaoMesa = 1
            }
            );
        modelBuilder.Entity<Models.CardapioItem>()
            .HasData(
            new Models.CardapioItem()
            {
                Id = 1,
                Titulo = "Coxinha",
                Descricao = "Coxinha de frango com catupiry",
                Preco = 6.50m,
                PossuiPreparo = true
            },
            new Models.CardapioItem()
            {
                Id = 2,
                Titulo = "Refrigerante Lata",
                Descricao = "Refrigerante em lata 350ml",
                Preco = 5.00m,
                PossuiPreparo = false
            }
            );
        base.OnModelCreating(modelBuilder);
    }
    public DbSet<Models.Usuario> Usuarios { get; set; } = default!;
    public DbSet<Models.Mesa> Mesas { get; set; } = default!;
    public DbSet<Models.Reserva> Reservas { get; set; } = default!;
    public DbSet<Models.Comanda> Comandas { get; set; } = default!;
    public DbSet<Models.ComandaItem> ComandaItens { get; set; } = default!;
    public DbSet<Models.PedidoCozinha> PedidoCozinhas { get; set; } = default!;
    public DbSet<Models.PedidoCozinhaItem> PedidoCozinhaItens { get; set; } = default!;
    public DbSet<Models.CardapioItem> CardapioItems { get; set; } = default!;
}