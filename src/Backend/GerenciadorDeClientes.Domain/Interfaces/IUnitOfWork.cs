namespace GerenciadorDeClientes.Domain.Interfaces;

public interface IUnitOfWork
{
    IClienteRepository ClienteRepository { get; }
    IEnderecoRepository EnderecoRepository { get; }
    Task<bool> Commit();
}
