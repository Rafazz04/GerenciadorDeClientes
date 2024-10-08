﻿using GerenciadorDeClientes.Domain.Entities;

namespace GerenciadorDeClientes.Domain.Interfaces;

public interface ITelefoneRepository : IRepositoryBase<Telefone>
{
    IEnumerable<Telefone> GetByClienteId(int clienteId);
}
