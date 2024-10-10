using GerenciadorDeClientes.Infrastructure.Integrations.Refit;
using GerenciadorDeClientes.Infrastructure.Integrations.Responses;
using GerenciadorDeClientes.Infrastructure.Integrations.Services.Interfaces;

namespace GerenciadorDeClientes.Infrastructure.Integrations.Services;

public class ViaCepIntegracao : IViaCepIntegracao
{
	private readonly IViaCepIntegracaoRefit _viaCepRefit;

	public ViaCepIntegracao(IViaCepIntegracaoRefit refit)
	{
		_viaCepRefit = refit;
	}

	public ViaCepResponse ObterDadosViaCep(string cep)
	{
		var responseData = _viaCepRefit.GetViaCep(cep).Result;
		if (responseData != null && responseData.IsSuccessStatusCode)
			return responseData.Content;
		return null;
	}
}
