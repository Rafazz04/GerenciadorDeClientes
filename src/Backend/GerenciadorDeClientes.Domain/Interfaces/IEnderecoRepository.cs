using GerenciadorDeClientes.Domain.Entities;

namespace GerenciadorDeClientes.Domain.Interfaces;

public interface IEnderecoRepository : IRepositoryBase<Endereco>
{
    IEnumerable<Endereco> GetByClienteId(int clienteId);
}
