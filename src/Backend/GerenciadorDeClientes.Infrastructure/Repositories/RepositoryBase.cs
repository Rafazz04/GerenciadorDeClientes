using GerenciadorDeClientes.Domain.Repositories;
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
    public IEnumerable<Entity> GetAll() => _context.Set<Entity>().AsNoTracking().ToList();
    public Entity GetById(int id) => _context.Set<Entity>().FirstOrDefault(x => EF.Property<int>(x,"Id") == id);
    public Entity Create(Entity entity)
    {
        _context.Set<Entity>().Add(entity);
        return entity;
    }
    public Entity Update(Entity entity)
    {
        _context.Entry(entity).State = EntityState.Modified;
        return entity;
    }
    public Entity Delete(Entity entity)
    {
        _context.Set<Entity>().Remove(entity);
        return entity;
    }

}
