using GerenciadorDeClientes.Domain.Interfaces;
using GerenciadorDeClientes.Infrastructure.DataAcess;

namespace GerenciadorDeClientes.Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private IClienteRepository _clienteRepo;
    public GerenciadorDeClientesDbContext _context;
    public UnitOfWork(GerenciadorDeClientesDbContext context)
    {
        _context = context;
    }
    public IClienteRepository ClienteRepository {  get { return _clienteRepo = _clienteRepo ?? new ClienteRepository(_context); } }

    public void Commit() => _context.SaveChanges();
    public void Dispose() => _context.Dispose();
}
