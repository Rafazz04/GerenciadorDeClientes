using GerenciadorDeClientes.Domain.Entities;

namespace GerenciadorDeClientes.Application.DTOs;

public class TelefoneDTO
{
	public string Numero { get; set; }
	public int ClienteId { get; set; }
	public Cliente Cliente { get; set; }
}
