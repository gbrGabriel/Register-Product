using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration;
public class ProdutoConfiguration : IEntityTypeConfiguration<Produto>
{
    public void Configure(EntityTypeBuilder<Produto> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id).HasDefaultValueSql("newsequentialid()");

        builder.Property(e => e.Nome).HasMaxLength(255);

        builder.Property(e => e.Descricao).HasMaxLength(255);

        builder.Property(e => e.Unidade).HasMaxLength(2);

        builder.HasOne(e => e.Estoque).
            WithOne().HasForeignKey<Estoque>(e => e.ProdutoId).
            IsRequired().
            OnDelete(DeleteBehavior.Cascade);
    }
}
