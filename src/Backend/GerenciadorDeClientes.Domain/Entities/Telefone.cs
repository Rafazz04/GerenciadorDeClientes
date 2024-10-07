namespace GerenciadorDeClientes.Domain.Entities;

public class Telefone : EntityBase
{
	public string Numero { get; set; }
	public int ClienteId { get; set; }
	public Cliente Cliente { get; set; }
}
