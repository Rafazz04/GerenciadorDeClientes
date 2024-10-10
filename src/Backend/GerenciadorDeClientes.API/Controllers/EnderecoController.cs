using GerenciadorDeClientes.Application.DTOs;
using GerenciadorDeClientes.Application.Services.Interfaces;
using GerenciadorDeClientes.Infrastructure.Integrations.Responses;
using GerenciadorDeClientes.Infrastructure.Integrations.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GerenciadorDeClientes.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EnderecoController : ControllerBase
{
	private readonly IViaCepIntegracao _viaCepIntegracao;
    private readonly IEnderecoService _enderecoService;
	public EnderecoController(IViaCepIntegracao viaCepIntegracao, IEnderecoService enderecoService)
	{
		_viaCepIntegracao = viaCepIntegracao;
        _enderecoService = enderecoService;
	}

	[HttpGet]
	public ActionResult<ViaCepResponse> GetDadosDoEndereco(string cep)
	{
		var responseData =  _viaCepIntegracao.ObterDadosViaCep(cep);
		if (responseData != null)
			return Ok(responseData);
		return BadRequest("Cep não encontrado!");
	}

    [HttpGet("{cnpj}")]
    public ActionResult<IEnumerable<EnderecoDTO>> GetByCnpj(string cnpj)
    {
        var enderecos = _enderecoService.GetByCnpj(cnpj);
        if (enderecos == null || !enderecos.Any())
            return NotFound("Nenhum endereço encontrado para este cliente.");

        return Ok(enderecos);
    }

    [HttpPost]
    public ActionResult<EnderecoDTO> Post(EnderecoDTO enderecoDTO)
    {
        try
        {
            var endereco = _enderecoService.Create(enderecoDTO);
            return CreatedAtAction(nameof(GetByCnpj), new { cnpj = endereco.Cnpj }, endereco);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Erro ao salvar endereço: {ex.Message}");
        }
    }

    [HttpDelete("{id}")]
    public ActionResult Delete(int id)
    {
        if (_enderecoService.Delete(id))
            return NoContent();

        return NotFound("Endereço não encontrado.");
    }
}
