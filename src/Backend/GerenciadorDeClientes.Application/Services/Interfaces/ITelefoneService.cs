using GerenciadorDeClientes.Application.DTOs;

namespace GerenciadorDeClientes.Application.Services.Interfaces;

public interface ITelefoneService
{
    IEnumerable<TelefoneDTO> GetByCnpj(string cnpj);
    TelefoneDTO Create(TelefoneDTO telefoneDTO);
    bool Delete(int id);
}
