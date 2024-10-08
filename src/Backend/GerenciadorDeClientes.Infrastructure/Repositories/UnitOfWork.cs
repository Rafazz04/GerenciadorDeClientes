using GerenciadorDeClientes.Domain.Interfaces;
using GerenciadorDeClientes.Infrastructure.DataAcess;

namespace GerenciadorDeClientes.Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private IClienteRepository _clienteRepo;
    private IEnderecoRepository _enderecoRepo;
    public GerenciadorDeClientesDbContext _context;
    public UnitOfWork(GerenciadorDeClientesDbContext context)
    {
        _context = context;
    }
    public IClienteRepository ClienteRepository {  get { return _clienteRepo = _clienteRepo ?? new ClienteRepository(_context); } }
    public IEnderecoRepository EnderecoRepository { get { return _enderecoRepo ?? new EnderecoRepository(_context); } }

	public async Task<bool> Commit() => await _context.SaveChangesAsync() > 0;
    public void Dispose() => _context.DisposeAsync();
}
