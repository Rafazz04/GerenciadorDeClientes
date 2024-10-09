using GerenciadorDeClientes.Domain.Entities;
using X.PagedList;

namespace GerenciadorDeClientes.Domain.Interfaces;

public interface IClienteRepository 
{
	Task<IEnumerable<Cliente>> GetAll();
	IPagedList<Cliente> GetAllPaginated(int pageNumber, int pageSize);
	Task<Cliente> GetById(int id);
	Task<Cliente> GetByCnpj(string cnpj);
	Task<Cliente> Create(Cliente cliente);
	Task<Cliente> Update(Cliente cliente);
	Task<Cliente> Delete(Cliente cliente);
}
