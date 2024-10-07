using GerenciadorDeClientes.Domain.Entities;
using GerenciadorDeClientes.Domain.Interfaces;
using GerenciadorDeClientes.Infrastructure.DataAcess;

namespace GerenciadorDeClientes.Infrastructure.Repositories;

public class EnderecoRepository : RepositoryBase<Endereco>, IEnderecoRepository
{
	public EnderecoRepository(GerenciadorDeClientesDbContext context) : base(context)
	{
	}
}
