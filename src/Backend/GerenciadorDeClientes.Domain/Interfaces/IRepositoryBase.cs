namespace GerenciadorDeClientes.Domain.Interfaces;

public interface IRepositoryBase<Entity> where Entity : class
{
    Task<IEnumerable<Entity>> GetAll();
    Task<Entity> GetById(int id);
    Task<Entity> Create(Entity entity);
    Task<Entity> Update(Entity entity);
    Task<Entity> Delete(Entity entity);
}
