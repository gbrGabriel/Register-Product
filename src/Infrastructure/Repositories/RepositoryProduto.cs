using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class RepositoryProduto(DatabaseContext context) : RepositoryBase<Produto>(context), IRepositoryProduto
{
    public override async Task<Produto> GetByIdAsync(Guid id)
        => await _context.Produtos.Include(e => e.Estoque).FirstOrDefaultAsync(e => e.Id == id);

    public override async Task<IEnumerable<Produto>> GetAllAsync()
        => await _context.Produtos.Include(e => e.Estoque).ToListAsync();
}
