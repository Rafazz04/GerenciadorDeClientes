namespace GerenciadorDeClientes.Infrastructure.Integrations.Responses;

public class ViaCepResponse
{
	public string? Cep { get; set; }
	public string? Logradouro { get; set; }
	public string? Complemento { get; set; }
	public string? Bairro { get; set; }
	public string? Localidade { get; set; }
	public string? Uf { get; set; }
    public string? Estado { get; set; }
	public string? Regiao { get; set; }
	public string? Ibge { get; set; }
}
