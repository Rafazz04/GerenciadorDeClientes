using GerenciadorDeClientes.API.Controllers;
using GerenciadorDeClientes.Application.DTOs;
using GerenciadorDeClientes.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace GerenciadorDeClientes.Test.UnitTests.Telefone;

public class TelefoneControllerTest
{
    private readonly TelefoneController _controller;
	private readonly Mock<ITelefoneService> _mockTelefoneService;

	public TelefoneControllerTest()
	{
		_mockTelefoneService = new Mock<ITelefoneService>();
		_controller = new TelefoneController(_mockTelefoneService.Object);
	}

	[Fact]
	public void GetByCnpj_RetornaOk_QuandoTelefoneExistir()
	{
		var cnpj = "22222222000122";
		var telefones = new List<TelefoneDTO>
		{
			new TelefoneDTO { Cnpj = cnpj, Numero = "11958471265" },
			new TelefoneDTO { Cnpj = cnpj, Numero = "11958471266" }
		};

		_mockTelefoneService.Setup(s => s.GetByCnpj(cnpj)).Returns(telefones);

		var result = _controller.GetByCnpj(cnpj);

		var okResult = Assert.IsType<OkObjectResult>(result.Result);
		var returnedTelefones = Assert.IsAssignableFrom<IEnumerable<TelefoneDTO>>(okResult.Value);

		Assert.Equal(2, returnedTelefones.Count());
	}

	[Fact]
	public void GetByCnpj_RetornaNotFound_QuandoTelefoneNaoExistir()
	{
		var cnpj = "22222222000122";

		_mockTelefoneService.Setup(s => s.GetByCnpj(cnpj)).Returns(new List<TelefoneDTO>());

		var result = _controller.GetByCnpj(cnpj);

		var notFoundResult = Assert.IsType<NotFoundObjectResult>(result.Result);
		Assert.Equal("Telefone não encontrado para o cliente com o CNPJ fornecido.", notFoundResult.Value);
	}

	[Fact]
	public void Post_RetornaCreated_QuandoTelefoneForCriado()
	{
		var telefoneDTO = new TelefoneDTO { Cnpj = "22222222000122", Numero = "11958471265" };
		_mockTelefoneService.Setup(s => s.Create(telefoneDTO)).Returns(telefoneDTO);

		var result = _controller.Post(telefoneDTO);

		var createdResult = Assert.IsType<CreatedAtActionResult>(result.Result);
		var createdTelefone = Assert.IsType<TelefoneDTO>(createdResult.Value);
		Assert.Equal(telefoneDTO.Cnpj, createdTelefone.Cnpj);
	}

	[Fact]
	public void Post_RetornaBadRequest_QuandoErroAoCriarTelefone()
	{
		var telefoneDTO = new TelefoneDTO { Cnpj = "22222222000122", Numero = "11958471265" };
		_mockTelefoneService.Setup(s => s.Create(telefoneDTO)).Throws(new Exception("Erro ao criar telefone."));

		var result = _controller.Post(telefoneDTO);

		var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
		Assert.Equal("Erro ao criar telefone: Erro ao criar telefone.", badRequestResult.Value);
	}

	[Fact]
	public void Delete_RetornaNoContent_QuandoTelefoneForDeletado()
	{
		var id = 1;
		_mockTelefoneService.Setup(s => s.Delete(id)).Returns(true);

		var result = _controller.Delete(id);

		Assert.IsType<NoContentResult>(result);
	}

	[Fact]
	public void Delete_RetornaNotFound_QuandoTelefoneNaoExistir()
	{
		var id = 1;
		_mockTelefoneService.Setup(s => s.Delete(id)).Returns(false);

		var result = _controller.Delete(id);

		var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
		Assert.Equal("Telefone não encontrado ou já removido.", notFoundResult.Value);
	}
}
