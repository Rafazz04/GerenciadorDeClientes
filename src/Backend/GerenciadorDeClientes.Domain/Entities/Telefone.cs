namespace GerenciadorDeClientes.Domain.Entities;

public class Telefone : EntityBase
{
    public int ClienteId { get; set; }
	public string Celular { get; set; }
}
