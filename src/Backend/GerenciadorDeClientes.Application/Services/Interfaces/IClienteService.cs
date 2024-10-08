using GerenciadorDeClientes.Application.DTOs;

namespace GerenciadorDeClientes.Application.Services.Interfaces;

public interface IClienteService
{
	Task<IEnumerable<ClienteDTO>> GetAll();
	Task<ClienteDTO> GetById(int id);
	Task<ClienteDTO> GetByCnpj(string cnpj);
	Task<ClienteDTO> Create(ClienteDTO clienteDTO);
	Task<ClienteDTO> Update(ClienteDTO clienteDTO);
	Task Delete(int id);

}
