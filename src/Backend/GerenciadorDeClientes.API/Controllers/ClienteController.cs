using AutoMapper;
using GerenciadorDeClientes.Application.DTOs;
using GerenciadorDeClientes.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace GerenciadorDeClientes.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ClienteController : ControllerBase
{
    private readonly IClienteService _clienteService;
	IMapper _mapper;

	public ClienteController(IClienteService clienteService, IMapper mapper)
	{
		_clienteService = clienteService;
		_mapper = mapper;
	}

	[HttpGet]
	public ActionResult<IEnumerable<ClienteDTO>> Get([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
	{
		try
		{
			var clientes =  _clienteService.GetAll();

			if(clientes is null)
				return NotFound();

			var clientesPaginados = clientes.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
			var clientesDto = _mapper.Map<IEnumerable<ClienteDTO>>(clientesPaginados);

			var metadata = new
			{
				TotalCount = clientes.Count(),
				PageSize = pageSize,
				CurrentPage = pageNumber,
				TotalPages = (int)Math.Ceiling(clientes.Count() / (double)pageSize),
				HasNext = pageNumber < (int)Math.Ceiling(clientes.Count() / (double)pageSize),
				HasPrevious = pageNumber > 1
			};

			Response.Headers.Append("X-Pagination", JsonConvert.SerializeObject(metadata));
			return Ok(clientesDto);
		}
		catch (Exception ex)
		{
			return StatusCode(500, $"Erro interno do servidor: {ex.Message} - {ex.StackTrace}");
		}
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
