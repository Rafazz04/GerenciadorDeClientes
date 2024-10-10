using GerenciadorDeClientes.Domain.Entities;
using X.PagedList;

namespace GerenciadorDeClientes.Domain.Interfaces;

public interface IClienteRepository 
{
	IEnumerable<Cliente> GetAll();
	IPagedList<Cliente> GetAllPaginated(int pageNumber, int pageSize);
	Cliente GetById(int id);
	Cliente GetByCnpj(string cnpj);
	Cliente Create(Cliente cliente);
	Cliente Update(Cliente cliente);
	void Delete(Cliente cliente);
}
