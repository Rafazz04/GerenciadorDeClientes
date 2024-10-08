using GerenciadorDeClientes.Domain.Entities;

namespace GerenciadorDeClientes.Application.DTOs;

public class EmailDTO
{
	public string EnderecoEmail { get; set; }
	public int ClienteId { get; set; }
	public Cliente Cliente { get; set; }
}
