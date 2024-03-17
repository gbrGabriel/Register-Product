using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class RepositoryEstoque(DatabaseContext context) : RepositoryBase<Estoque>(context), IRepositoryEstoque
{
    public override async Task<IEnumerable<Estoque>> GetAllAsync()
        => await _context.Estoques.AsNoTracking().ToListAsync();

    public override async Task<Estoque> GetByIdAsync(Guid id)
        => await _context.Estoques.FirstOrDefaultAsync(e => e.Id == id);
}
