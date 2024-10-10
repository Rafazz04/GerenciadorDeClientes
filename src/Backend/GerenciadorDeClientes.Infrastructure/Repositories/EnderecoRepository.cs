using GerenciadorDeClientes.Domain.Entities;
using GerenciadorDeClientes.Domain.Interfaces;
using GerenciadorDeClientes.Infrastructure.DataAcess;

namespace GerenciadorDeClientes.Infrastructure.Repositories;

public class EnderecoRepository : RepositoryBase<Endereco>, IEnderecoRepository
{
    private readonly GerenciadorDeClientesDbContext _context;
    public EnderecoRepository(GerenciadorDeClientesDbContext context) : base(context)
    {
        _context = context;
    }

    public IEnumerable<Endereco> GetByClienteId(int clienteId)
    {
        return _context.ENDERECO.Where(t => t.ClienteId == clienteId).ToList();
    }
}
