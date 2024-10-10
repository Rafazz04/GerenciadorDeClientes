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
	public ActionResult<IEnumerable<ClienteDTO>> Get()
	{
		try
		{
			var clientes =  _clienteService.GetAll();
			if(clientes.Any()) 
				return Ok(clientes); 
			return NotFound();
		}
		catch (Exception ex)
		{
			return StatusCode(500, $"Erro interno do servidor: {ex.Message} - {ex.StackTrace}");
		}
	}

	[HttpGet("Paginated")]
	public ActionResult<IEnumerable<ClienteDTO>> GetPaginated([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
	{
		var paginatedClientes = _clienteService.GetAllPaginated(pageNumber, pageSize);

		if (paginatedClientes == null || !paginatedClientes.Any())
			return NotFound("Nenhum cliente encontrado!");

		return Ok(paginatedClientes);
	}

	[HttpPost]
	public ActionResult<ClienteDTO> Post(ClienteDTO clienteDTO)
	{
		var cliente = _clienteService.Create(clienteDTO);
		if (cliente == null)
			return BadRequest();
		return Ok(cliente);
	}

	[HttpPut("{cnpj}")]
	public ActionResult<ClienteDTO> Put(string cnpj, ClienteUpdateDTO clienteUpdateDTO)
	{
        var cliente = _clienteService.Update(cnpj, clienteUpdateDTO);
        if (cliente == null)
            return BadRequest();
        return Ok(cliente);
    }

	[HttpDelete]
	public ActionResult Delete(string cnpj) 
	{ 
		var cliente = _clienteService.Delete(cnpj);
		if(cliente)
			return Ok();
		return NotFound("Cliente não encontrado!");
	}
}
