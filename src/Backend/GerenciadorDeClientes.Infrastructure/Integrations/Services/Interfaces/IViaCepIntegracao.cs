using GerenciadorDeClientes.Infrastructure.Integrations.Responses;

namespace GerenciadorDeClientes.Infrastructure.Integrations.Services.Interfaces;

public interface IViaCepIntegracao
{
	Task<ViaCepResponse> ObterDadosViaCep(string cep);
}
