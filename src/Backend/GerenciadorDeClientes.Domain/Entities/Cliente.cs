namespace GerenciadorDeClientes.Domain.Entities;

public class Cliente : EntityBase
{
    public string Nome { get; set; }
    public string Cnpj { get; set; }
    public bool FlaAtivo { get; set; }
    public ICollection<Endereco> Enderecos { get; set; }
    public ICollection<Telefone> Telefones { get; set; }
    public ICollection<Email> Emails { get; set; }
}
