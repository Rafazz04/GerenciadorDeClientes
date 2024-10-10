using GerenciadorDeClientes.Domain.Entities;
using GerenciadorDeClientes.Domain.Interfaces;
using GerenciadorDeClientes.Infrastructure.DataAcess;

namespace GerenciadorDeClientes.Infrastructure.Repositories;

public class EmailRepository : RepositoryBase<Email>, IEmailRepository
{
    private readonly GerenciadorDeClientesDbContext _context;
    public EmailRepository(GerenciadorDeClientesDbContext context) : base(context)
	{
        _context = context;
	}

    public IEnumerable<Email> GetByClienteId(int clienteId)
    {
        return _context.EMAIL.Where(t => t.ClienteId == clienteId).ToList();
    }
}
