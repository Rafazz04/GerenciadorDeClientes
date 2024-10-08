using GerenciadorDeClientes.Domain.Entities;

namespace GerenciadorDeClientes.Application.DTOs;

public class ClienteDTO
{
	public string Nome { get; set; }
	public string Cnpj { get; set; }
	public bool FlaAtivo { get; set; }
	public ICollection<Endereco> Enderecos { get; set; }
	public ICollection<Telefone> Telefones { get; set; }
	public ICollection<Email> Emails { get; set; }
}
