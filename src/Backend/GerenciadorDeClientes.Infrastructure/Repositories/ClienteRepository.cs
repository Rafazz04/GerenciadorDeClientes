using GerenciadorDeClientes.Domain.Entities;
using GerenciadorDeClientes.Domain.Interfaces;
using GerenciadorDeClientes.Infrastructure.DataAcess;
using Microsoft.EntityFrameworkCore;
using X.PagedList;

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

	public IEnumerable<Cliente> GetAll() =>  _repositoryBase.GetAll();
	public  IPagedList<Cliente> GetAllPaginated(int pageNumber, int pageSize)
	{
		var clientes = _context.CLIENTE.AsQueryable();
		return clientes.ToPagedList(pageNumber, pageSize);
	}
	public Cliente GetById(int id) =>  _repositoryBase.GetById(id);
	public Cliente GetByCnpj(string cnpj) =>  _context.CLIENTE.FirstOrDefault(x => x.Cnpj == cnpj);

	public Cliente Create(Cliente	cliente) =>  _repositoryBase.Create(cliente);
	public Cliente Update(Cliente cliente) =>  _repositoryBase.Update(cliente);

	public void Delete(Cliente cliente) => _repositoryBase.Delete(cliente);
}
