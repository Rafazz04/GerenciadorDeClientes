using GerenciadorDeClientes.Domain.Entities;
using GerenciadorDeClientes.Domain.Interfaces;
using GerenciadorDeClientes.Infrastructure.DataAcess;

namespace GerenciadorDeClientes.Infrastructure.Repositories;

public class EmailRepository : RepositoryBase<Email>, IEmailRepository
{
	public EmailRepository(GerenciadorDeClientesDbContext context) : base(context)
	{
	}
}
