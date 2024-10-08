﻿using GerenciadorDeClientes.Domain.Entities;

namespace GerenciadorDeClientes.Domain.Interfaces;

public interface IEmailRepository : IRepositoryBase<Email>
{
    IEnumerable<Email> GetByClienteId(int clienteId);
}
