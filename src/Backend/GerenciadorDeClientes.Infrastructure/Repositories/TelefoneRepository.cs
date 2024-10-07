using GerenciadorDeClientes.Domain.Entities;
using GerenciadorDeClientes.Domain.Interfaces;
using GerenciadorDeClientes.Infrastructure.DataAcess;

namespace GerenciadorDeClientes.Infrastructure.Repositories;

public class TelefoneRepository : RepositoryBase<Telefone>, ITelefoneRepository
{
	public TelefoneRepository(GerenciadorDeClientesDbContext context) : base(context)
	{
	}
}
