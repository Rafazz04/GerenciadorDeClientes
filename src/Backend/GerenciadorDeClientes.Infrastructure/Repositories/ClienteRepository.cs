using GerenciadorDeClientes.Domain.Entities;
using GerenciadorDeClientes.Domain.Interfaces;
using GerenciadorDeClientes.Infrastructure.DataAcess;

namespace GerenciadorDeClientes.Infrastructure.Repositories;

public class ClienteRepository : RepositoryBase<Cliente>, IClienteRepository
{
    public ClienteRepository(GerenciadorDeClientesDbContext context) : base(context) {}
}
