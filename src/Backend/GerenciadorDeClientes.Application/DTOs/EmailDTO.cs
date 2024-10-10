using GerenciadorDeClientes.Domain.Entities;

namespace GerenciadorDeClientes.Application.DTOs;

public class EmailDTO
{
	public string EnderecoEmail { get; set; }
	public string Cnpj {  get; set; }	
}
