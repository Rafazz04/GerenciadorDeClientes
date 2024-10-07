namespace GerenciadorDeClientes.Domain.Entities;

public class Email : EntityBase
{
    public string EnderecoEmail { get; set; }
	public int ClienteId { get; set; }
	public Cliente Cliente { get; set; }
}
