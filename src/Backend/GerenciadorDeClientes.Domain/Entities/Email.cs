namespace GerenciadorDeClientes.Domain.Entities;

public class Email : EntityBase
{
    public int ClienteId { get; set; }
    public string EnderecoEmail { get; set; }
}
