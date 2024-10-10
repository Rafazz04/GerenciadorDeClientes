using GerenciadorDeClientes.Application.DTOs;
using GerenciadorDeClientes.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GerenciadorDeClientes.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EmailController : ControllerBase
{
    private readonly IEmailService _emailService;

    public EmailController(IEmailService emailService)
    {
        _emailService = emailService;
    }

    [HttpGet("{cnpj}")]
    public ActionResult<EmailDTO> GetByCnpj(string cnpj)
    {
        try
        {
            var emails = _emailService.GetByCnpj(cnpj);
            if (emails == null || !emails.Any())
                return NotFound("Email não encontrado para o cliente com o CNPJ fornecido.");
            return Ok(emails);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Erro interno do servidor: {ex.Message} - {ex.StackTrace}");
        }
    }

    [HttpPost]
    public ActionResult<EmailDTO> Post(EmailDTO emailDTO)
    {
        try
        {
            var createdEmail = _emailService.Create(emailDTO);
            return CreatedAtAction(nameof(GetByCnpj), new { cnpj = createdEmail.Cnpj }, createdEmail);
        }
        catch (Exception ex)
        {
            return BadRequest($"Erro ao criar e-mail: {ex.Message}");
        }
    }

    [HttpDelete("{id}")]
    public ActionResult Delete(int id)
    {
        try
        {
            var success = _emailService.Delete(id);
            if (!success)
                return NotFound("Email não encontrado ou já removido.");
            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Erro interno do servidor: {ex.Message}");
        }
    }
}
