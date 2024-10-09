using GerenciadorDeClientes.Application.DTOs;
using GerenciadorDeClientes.Domain.Entities;
using X.PagedList;

namespace GerenciadorDeClientes.Application.Services.Interfaces;

public interface IClienteService
{
	Task<IEnumerable<ClienteDTO>> GetAll();
	IPagedList<ClienteDTO> GetAllPaginated(int pageNumber, int pageSize);
	Task<ClienteDTO> GetById(int id);
	Task<ClienteDTO> GetByCnpj(string cnpj);
	Task<ClienteDTO> Create(ClienteDTO clienteDTO);
	Task<ClienteDTO> Update(ClienteDTO clienteDTO);
	Task Delete(int id);

}
