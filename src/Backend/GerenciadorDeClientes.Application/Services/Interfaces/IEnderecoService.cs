using GerenciadorDeClientes.Application.DTOs;

namespace GerenciadorDeClientes.Application.Services.Interfaces;

public interface IEnderecoService
{
    IEnumerable<EnderecoDTO> GetByCnpj(string cnpj);
    EnderecoDTO Create(EnderecoDTO enderecoDTO);
    bool Delete(int id);
}
