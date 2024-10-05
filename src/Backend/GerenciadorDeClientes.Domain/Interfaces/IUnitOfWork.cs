namespace GerenciadorDeClientes.Domain.Interfaces;

public interface IUnitOfWork
{
    IClienteRepository ClienteRepository { get; }
    void Commit();
}
