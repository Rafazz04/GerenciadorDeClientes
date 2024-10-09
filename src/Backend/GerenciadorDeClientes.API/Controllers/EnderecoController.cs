using GerenciadorDeClientes.Infrastructure.Integrations.Responses;
using GerenciadorDeClientes.Infrastructure.Integrations.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GerenciadorDeClientes.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EnderecoController : ControllerBase
{
	private readonly IViaCepIntegracao _viaCepIntegracao;

	public EnderecoController(IViaCepIntegracao viaCepIntegracao)
	{
		_viaCepIntegracao = viaCepIntegracao;
	}

	[HttpGet]
	public async Task<ActionResult<ViaCepResponse>> GetDadosDoEndereco(string cep)
	{
		var responseData = await _viaCepIntegracao.ObterDadosViaCep(cep);
		if (responseData != null)
			return Ok(responseData);
		return BadRequest("Cep não encontrado!");
	}
}
