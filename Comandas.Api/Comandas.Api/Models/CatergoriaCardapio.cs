using System.ComponentModel.DataAnnotations;

public class CategoriaCardapio

{

    [Key]
    public int Id { get; set; }
    public string Nome { get; set; } = default!;
    public string? Descricao { get; set; }

}

