using GerenciadorDeClientes.Domain.Interfaces;
using GerenciadorDeClientes.Infrastructure.DataAcess;
using Microsoft.EntityFrameworkCore;

namespace GerenciadorDeClientes.Infrastructure.Repositories;

public class RepositoryBase<Entity> : IRepositoryBase<Entity> where Entity : class
{
    protected readonly GerenciadorDeClientesDbContext _context;

    public RepositoryBase(GerenciadorDeClientesDbContext context)
    {
        _context = context;
    }
    public async Task<IEnumerable<Entity>> GetAll() => await _context.Set<Entity>().AsNoTracking().ToListAsync();
	public async Task<Entity> GetById(int id) => await _context.Set<Entity>().FindAsync(id);
    public async Task<Entity> Create(Entity entity)
    {
        await _context.Set<Entity>().AddAsync(entity);
        return entity;
    }
    public async Task<Entity> Update(Entity entity)
    {
        _context.Set<Entity>().Update(entity);
        return entity;
    }
    public async Task<Entity> Delete(Entity entity)
    {
        _context.Set<Entity>().Remove(entity);
        return entity;
    }
}
