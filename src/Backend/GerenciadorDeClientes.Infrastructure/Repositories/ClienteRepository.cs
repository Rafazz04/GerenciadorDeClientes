using GerenciadorDeClientes.Domain.Entities;
using GerenciadorDeClientes.Domain.Interfaces;
using GerenciadorDeClientes.Infrastructure.DataAcess;
using Microsoft.EntityFrameworkCore;

namespace GerenciadorDeClientes.Infrastructure.Repositories;

public class ClienteRepository : IClienteRepository
{
	private GerenciadorDeClientesDbContext _context;
	private readonly IRepositoryBase<Cliente> _repositoryBase;

	public ClienteRepository(GerenciadorDeClientesDbContext context,IRepositoryBase<Cliente> repositoryBase)
	{
		_context = context;
		_repositoryBase = repositoryBase;
	}
	public ClienteRepository(GerenciadorDeClientesDbContext context)
	{
		_context = context;
	}

	public async Task<IEnumerable<Cliente>> GetAll() => await _repositoryBase.GetAll();

	public async Task<Cliente> GetById(int id) => await _repositoryBase.GetById(id);
	public async Task<Cliente> GetByCnpj(string cnpj) => await _context.Clientes.FirstOrDefaultAsync(x => x.Cnpj == cnpj);

	public async Task<Cliente> Create(Cliente	cliente) => await _repositoryBase.Create(cliente);
	public async Task<Cliente> Update(Cliente cliente) => await _repositoryBase.Update(cliente);

	public Task<Cliente> Delete(Cliente cliente) => _repositoryBase.Delete(cliente);

}
