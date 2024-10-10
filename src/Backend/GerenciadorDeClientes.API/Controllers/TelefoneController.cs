using GerenciadorDeClientes.Application.DTOs;
using GerenciadorDeClientes.Application.Services;
using GerenciadorDeClientes.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GerenciadorDeClientes.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TelefoneController : ControllerBase
{
    private readonly ITelefoneService _telefoneService;
    public TelefoneController(ITelefoneService telefoneService)
    {
        _telefoneService = telefoneService;
    }

    [HttpGet("{cnpj}")]
    public ActionResult<TelefoneDTO> GetByCnpj(string cnpj)
    {
        try
        {
            var telefones = _telefoneService.GetByCnpj(cnpj);
            if (telefones == null || !telefones.Any())
                return NotFound("Telefone não encontrado para o cliente com o CNPJ fornecido.");
            return Ok(telefones);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Erro interno do servidor: {ex.Message} - {ex.StackTrace}");
        }
    }

    [HttpPost]
    public ActionResult<TelefoneDTO> Post(TelefoneDTO telefoneDTO)
    {
        try
        {
            var createdTelefone = _telefoneService.Create(telefoneDTO);
            return CreatedAtAction(nameof(GetByCnpj), new { cnpj = createdTelefone.Cnpj }, createdTelefone);
        }
        catch (Exception ex)
        {
            return BadRequest($"Erro ao criar telefone: {ex.Message}");
        }
    }

    [HttpDelete("{id}")]
    public ActionResult Delete(int id)
    {
        try
        {
            var success = _telefoneService.Delete(id);
            if (!success)
                return NotFound("Telefone não encontrado ou já removido.");
            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Erro interno do servidor: {ex.Message}");
        }

    }
}
