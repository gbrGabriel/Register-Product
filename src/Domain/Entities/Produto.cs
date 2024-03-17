namespace Domain.Entities;
public class Produto : EntityBase
{
    public string Nome { get; set; } = null!;

    public string Descricao { get; set; } = null!;

    public decimal Preco { get; set; }

    public string Unidade { get; set; } = null!;

    public string SKU { get; set; } = null!;

    public bool Inativo { get; set; }

    public Estoque? Estoque { get; set; }

}
