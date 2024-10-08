using GerenciadorDeClientes.Domain.Entities;

namespace GerenciadorDeClientes.Domain.Interfaces;

public interface IClienteRepository 
{
	Task<IEnumerable<Cliente>> GetAll();
	Task<Cliente> GetById(int id);
	Task<Cliente> GetByCnpj(string cnpj);
	Task<Cliente> Create(Cliente cliente);
	Task<Cliente> Update(Cliente cliente);
	Task<Cliente> Delete(Cliente cliente);
}
