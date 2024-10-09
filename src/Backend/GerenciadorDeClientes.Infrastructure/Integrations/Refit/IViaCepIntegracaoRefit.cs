using GerenciadorDeClientes.Infrastructure.Integrations.Responses;
using Refit;

namespace GerenciadorDeClientes.Infrastructure.Integrations.Refit;

public interface IViaCepIntegracaoRefit
{
	[Get("/{cep}/json")]
	Task<ApiResponse<ViaCepResponse>> GetViaCep(string cep);
}
