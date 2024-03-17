namespace Domain.Entities;
public class Estoque : EntityBase
{
    public Guid ProdutoId { get; set; }

    public decimal Quantidade { get; set; }

    public bool Inativo { get; set; }

}
