using GerenciadorDeClientes.Domain.Entities;

namespace GerenciadorDeClientes.Application.DTOs;

public class ClienteDTO
{
	public string Nome { get; set; }
	public string Cnpj { get; set; }
	public bool FlaAtivo { get; set; }
    public string Cep { get; set; }
    public string Celular { get; set; }
    public string Email { get; set; }
}
