namespace Domain.Entities;
public abstract class EntityBase
{
    public Guid Id { get; set; }
    public DateTime DataCadastro { get; set; }
}
