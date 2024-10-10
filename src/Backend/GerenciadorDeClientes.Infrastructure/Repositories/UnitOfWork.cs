using GerenciadorDeClientes.Domain.Interfaces;
using GerenciadorDeClientes.Infrastructure.DataAcess;

namespace GerenciadorDeClientes.Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly GerenciadorDeClientesDbContext _context;
    private readonly IClienteRepository _clienteRepo;
    private readonly IEnderecoRepository _enderecoRepo;

    public UnitOfWork(GerenciadorDeClientesDbContext context, IClienteRepository clienteRepo,IEnderecoRepository enderecoRepo)
    {
        _context = context;
        _clienteRepo = clienteRepo;
        _enderecoRepo = enderecoRepo;
    }

    public IClienteRepository ClienteRepository => _clienteRepo;
    public IEnderecoRepository EnderecoRepository => _enderecoRepo;

    public bool Commit() =>  _context.SaveChanges() > 0;

}
