using GerenciadorDeClientes.Domain.Entities;
using GerenciadorDeClientes.Domain.Interfaces;
using GerenciadorDeClientes.Infrastructure.DataAcess;

namespace GerenciadorDeClientes.Infrastructure.Repositories;

public class TelefoneRepository : RepositoryBase<Telefone>, ITelefoneRepository
{
    private readonly GerenciadorDeClientesDbContext _context;
    public TelefoneRepository(GerenciadorDeClientesDbContext context) : base(context)
	{
        _context = context;
	}
    public IEnumerable<Telefone> GetByClienteId(int clienteId)
    {
        return _context.TELEFONE.Where(t => t.ClienteId == clienteId).ToList();
    }
}
