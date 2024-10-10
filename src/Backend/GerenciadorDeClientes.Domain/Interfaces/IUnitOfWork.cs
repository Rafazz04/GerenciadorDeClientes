namespace GerenciadorDeClientes.Domain.Interfaces;

public interface IUnitOfWork
{
    IClienteRepository ClienteRepository { get; }
    IEnderecoRepository EnderecoRepository { get; }
    bool Commit();
}
