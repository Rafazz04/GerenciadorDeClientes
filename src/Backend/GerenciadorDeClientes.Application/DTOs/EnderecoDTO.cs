using GerenciadorDeClientes.Domain.Entities;

namespace GerenciadorDeClientes.Application.DTOs;

public class EnderecoDTO
{
	public string Cep { get; set; }
	public string Estado { get; set; }
	public string Cidade { get; set; }
	public string Logradouro { get; set; }
	public string? Bairro { get; set; }
	public string? Numero { get; set; }
	public string? Complemento { get; set; }
	public int ClienteId { get; set; }
	public Cliente Cliente { get; set; }
}
