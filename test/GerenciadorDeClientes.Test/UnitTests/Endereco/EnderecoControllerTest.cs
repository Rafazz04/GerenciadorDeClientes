using GerenciadorDeClientes.API.Controllers;
using GerenciadorDeClientes.Application.DTOs;
using GerenciadorDeClientes.Application.Services.Interfaces;
using GerenciadorDeClientes.Infrastructure.Integrations.Responses;
using GerenciadorDeClientes.Infrastructure.Integrations.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace GerenciadorDeClientes.Test.UnitTests.Endereco;

public class EnderecoControllerTest
{
	private readonly Mock<IViaCepIntegracao> _mockViaCepIntegracao;
	private readonly Mock<IEnderecoService> _mockEnderecoService;
	private readonly EnderecoController _controller;

	public EnderecoControllerTest()
	{
		_mockViaCepIntegracao = new Mock<IViaCepIntegracao>();
		_mockEnderecoService = new Mock<IEnderecoService>();
		_controller = new EnderecoController(_mockViaCepIntegracao.Object, _mockEnderecoService.Object);
	}

	[Fact]
	public void GetDadosDoEndereco_RetornaOk_QuandoCepValido()
	{
		var cep = "05836400";
		var response = new ViaCepResponse { Cep = cep, Logradouro = "Rua Exemplo" };
		_mockViaCepIntegracao.Setup(s => s.ObterDadosViaCep(cep)).Returns(response);

		var result = _controller.GetDadosDoEndereco(cep);

		var okResult = Assert.IsType<OkObjectResult>(result.Result);
		var returnValue = Assert.IsType<ViaCepResponse>(okResult.Value);
		Assert.Equal(cep, returnValue.Cep);
	}

	[Fact]
	public void GetDadosDoEndereco_RetornaBadRequest_QuandoCepInvalido()
	{
		var cep = "00000000";
		_mockViaCepIntegracao.Setup(s => s.ObterDadosViaCep(cep)).Returns((ViaCepResponse)null);

		var result = _controller.GetDadosDoEndereco(cep);

		var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
		Assert.Equal("Cep não encontrado!", badRequestResult.Value);
	}

	[Fact]
	public void GetByCnpj_RetornaOk_QuandoExistiremEnderecos()
	{
		var cnpj = "22222222000122";
		var enderecos = new List<EnderecoDTO>
		{
			new EnderecoDTO{ Cnpj = "22222222000122", Logradouro = "Avenida Paulista", Bairro = "Bela Vista", Cep = "01310-100", Cidade = "São Paulo", Complemento = "Sala 501", Estado = "SP", Numero = "456" }
		};
		_mockEnderecoService.Setup(s => s.GetByCnpj(cnpj)).Returns(enderecos);

		var result = _controller.GetByCnpj(cnpj);

		var okResult = Assert.IsType<OkObjectResult>(result.Result);
		var returnValue = Assert.IsAssignableFrom<IEnumerable<EnderecoDTO>>(okResult.Value);
		Assert.Single(returnValue);
	}

	[Fact]
	public void GetByCnpj_RetornaNotFound_QuandoNaoExistiremEnderecos()
	{
		var cnpj = "22222222000122";
		_mockEnderecoService.Setup(s => s.GetByCnpj(cnpj)).Returns(new List<EnderecoDTO>());

		var result = _controller.GetByCnpj(cnpj);

		var notFoundResult = Assert.IsType<NotFoundObjectResult>(result.Result);
		Assert.Equal("Nenhum endereço encontrado para este cliente.", notFoundResult.Value);
	}

	[Fact]
	public void Post_RetornaCreated_QuandoEnderecoForCriado()
	{
		var enderecoDto = new EnderecoDTO { Cnpj = "66666666000166", Logradouro = "Rua das Flores", Bairro = "Centro", Cep = "88015-300", Cidade = "Florianópolis", Complemento = "Sala 202", Estado = "SC", Numero = "987" };
		_mockEnderecoService.Setup(s => s.Create(enderecoDto)).Returns(enderecoDto);

		var result = _controller.Post(enderecoDto);

		var createdResult = Assert.IsType<CreatedAtActionResult>(result.Result);
		var returnValue = Assert.IsType<EnderecoDTO>(createdResult.Value);
		Assert.Equal(enderecoDto.Cnpj, returnValue.Cnpj);
	}

	[Fact]
	public void Post_RetornaInternalServerError_QuandoErroAoSalvar()
	{
		var enderecoDto = new EnderecoDTO { Cnpj = "00000000000100", Logradouro = "Avenida das Américas", Bairro = "Barra da Tijuca", Cep = "22640-102", Cidade = "Rio de Janeiro", Complemento = "Casa 4", Estado = "RJ", Numero = "963" };
		_mockEnderecoService.Setup(s => s.Create(enderecoDto)).Throws(new Exception("Erro ao salvar."));

		var result = _controller.Post(enderecoDto);

		var statusCodeResult = Assert.IsType<ObjectResult>(result.Result);
		Assert.Equal(500, statusCodeResult.StatusCode);
		Assert.Contains("Erro ao salvar endereço", statusCodeResult.Value.ToString());
	}

	[Fact]
	public void Delete_RetornaNoContent_QuandoEnderecoForDeletado()
	{
		var id = 1;
		_mockEnderecoService.Setup(s => s.Delete(id)).Returns(true);

		var result = _controller.Delete(id);

		Assert.IsType<NoContentResult>(result);
	}

	[Fact]
	public void Delete_RetornaNotFound_QuandoEnderecoNaoForDeletado()
	{
		var id = 1;
		_mockEnderecoService.Setup(s => s.Delete(id)).Returns(false);

		var result = _controller.Delete(id);

		var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
		Assert.Equal("Endereço não encontrado.", notFoundResult.Value);
	}
}
