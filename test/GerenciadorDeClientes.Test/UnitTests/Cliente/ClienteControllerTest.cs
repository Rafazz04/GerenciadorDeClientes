using AutoMapper;
using GerenciadorDeClientes.API.Controllers;
using GerenciadorDeClientes.Application.DTOs;
using GerenciadorDeClientes.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace GerenciadorDeClientes.Test.UnitTests.Cliente;

public class ClienteControllerTest
{
	private readonly Mock<IClienteService> _mockClienteService;
	private readonly Mock<IMapper> _mockMapper;
	private readonly ClienteController _controller;

	public ClienteControllerTest()
	{
		_mockClienteService = new Mock<IClienteService>();
		_mockMapper = new Mock<IMapper>();
		_controller = new ClienteController(_mockClienteService.Object, _mockMapper.Object);
	}

	[Fact]
	public void Get_RetornaNotFound_QuandoNãoExistirCliente()
	{
		_mockClienteService.Setup(s => s.GetAll()).Returns((IEnumerable<ClienteDTO>)null);

		var result = _controller.Get(pageNumber: 1, pageSize: 10);

		Assert.IsType<NotFoundResult>(result.Result);
	}

	[Fact]
	public void Post_RetornaOk_QuandoClienteForCriado()
	{    
		var clienteDto = new ClienteDTO { Cnpj = "22222222000122", Nome = "Cliente 2", Celular = "11958472262", Cep = "05836402", Email = "teste2@gmail.com", FlaAtivo = true };

		_mockClienteService.Setup(s => s.Create(It.IsAny<ClienteDTO>())).Returns(clienteDto);

		var result = _controller.Post(clienteDto);

		var okResult = Assert.IsType<OkObjectResult>(result.Result);
		var createdCliente = Assert.IsType<ClienteDTO>(okResult.Value);

		Assert.Equal("Cliente 2", createdCliente.Nome);
	}

	[Fact]
	public void Post_RetornaBadRequest_QuandoClienteNãoForCriado()
	{
		var clienteDto = new ClienteDTO { Cnpj = "25252525000144", Nome = "Cliente 24", Celular = "11958472284", Cep = "05836424", Email = "teste24@gmail.com", FlaAtivo = true };
		_mockClienteService.Setup(s => s.Create(It.IsAny<ClienteDTO>())).Returns((ClienteDTO)null);

		var result = _controller.Post(clienteDto);

		Assert.IsType<BadRequestResult>(result.Result);
	}

	[Fact]
	public void Put_RetornaOk_QuandoClienteForAtualizado()
	{
		var updateDto = new ClienteUpdateDTO { Nome = "Cliente 24", Celular = "11958472284", Cep = "05836424", Email = "teste24@gmail.com", FlaAtivo = true };
		var updatedClienteDto = new ClienteDTO { Cnpj = "11111111000111", Nome = "Cliente Atualizado" };
		_mockClienteService.Setup(s => s.Update(It.IsAny<string>(), It.IsAny<ClienteUpdateDTO>())).Returns(updatedClienteDto);

		var result = _controller.Put("11111111000111", updateDto);

		var okResult = Assert.IsType<OkObjectResult>(result.Result);
		var updatedCliente = Assert.IsType<ClienteDTO>(okResult.Value);
		Assert.Equal("Cliente Atualizado", updatedCliente.Nome);
	}

	[Fact]
	public void Put_RetornaBadRequest_QuandoClienteaoForAtualizado()
	{
		var updateDto = new ClienteUpdateDTO { Nome = "Cliente Atualizado" };
		_mockClienteService.Setup(s => s.Update(It.IsAny<string>(), It.IsAny<ClienteUpdateDTO>())).Returns((ClienteDTO)null);

		var result = _controller.Put("11111111000111", updateDto);

		Assert.IsType<BadRequestResult>(result.Result);
	}

	[Fact]
	public void Delete_RetornaOk_QuandoClienteForDeletado()
	{
		_mockClienteService.Setup(s => s.Delete(It.IsAny<string>())).Returns(true);

		var result = _controller.Delete("11111111000111");

		Assert.IsType<OkResult>(result);
	}

	[Fact]
	public void Delete_RetornaNotFound_QuandoClienteNaoForDeletado()
	{
		_mockClienteService.Setup(s => s.Delete(It.IsAny<string>())).Returns(false);

		var result = _controller.Delete("11111111000111");

		var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
		Assert.Equal("Cliente não encontrado!", notFoundResult.Value);
	}
}
