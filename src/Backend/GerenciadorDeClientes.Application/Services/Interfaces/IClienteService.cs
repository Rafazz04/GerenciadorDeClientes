using GerenciadorDeClientes.Application.DTOs;
using GerenciadorDeClientes.Domain.Entities;
using X.PagedList;

namespace GerenciadorDeClientes.Application.Services.Interfaces;

public interface IClienteService
{
	IEnumerable<ClienteDTO> GetAll();
	IPagedList<ClienteDTO> GetAllPaginated(int pageNumber, int pageSize);
	ClienteDTO GetById(int id);
	ClienteDTO GetByCnpj(string cnpj);
	ClienteDTO Create(ClienteDTO clienteDTO);
	ClienteDTO Update(string cnpj, ClienteUpdateDTO clienteUpdateDTO);
	bool Delete(string cnpj);

}
