namespace GerenciadorDeClientes.Application.DTOs;

public class ClienteUpdateDTO
{
    public string Nome { get; set; }
    public bool FlaAtivo { get; set; }
    public string Cep { get; set; }
    public string Celular { get; set; }
    public string Email { get; set; }
}
