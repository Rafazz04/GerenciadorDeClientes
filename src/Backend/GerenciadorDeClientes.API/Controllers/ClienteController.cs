using GerenciadorDeClientes.Application.DTOs;
using GerenciadorDeClientes.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GerenciadorDeClientes.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ClienteController : ControllerBase
{
    private readonly IClienteService _clienteService;

	public ClienteController(IClienteService clienteService)
	{
		_clienteService = clienteService;
	}

	[HttpGet]
	public async Task<ActionResult<IEnumerable<ClienteDTO>>> Get()
	{
		try
		{
			var clientes = await _clienteService.GetAll();
			if(clientes.Any()) 
				return Ok(clientes); 
			return NotFound();
		}
		catch (Exception ex)
		{
			return StatusCode(500, $"Erro interno do servidor: {ex.Message} - {ex.StackTrace}");
		}
	}
}
