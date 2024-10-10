using GerenciadorDeClientes.Application.DTOs;

namespace GerenciadorDeClientes.Application.Services.Interfaces;

public interface IEmailService
{
    IEnumerable<EmailDTO> GetByCnpj(string cnpj);
    EmailDTO Create(EmailDTO emailDTO);
    bool Delete(int id);
}
