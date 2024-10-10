using GerenciadorDeClientes.API.Controllers;
using GerenciadorDeClientes.Application.DTOs;
using GerenciadorDeClientes.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace GerenciadorDeClientes.Test.UnitTests.Email;

public class EmailControllerTest
{
	private readonly EmailController _controller;
	private readonly Mock<IEmailService> _mockEmailService;

	public EmailControllerTest()
	{
		_mockEmailService = new Mock<IEmailService>();
		_controller = new EmailController(_mockEmailService.Object);
	}

	[Fact]
	public void GetByCnpj_RetornaOk_QuandoEmailExistir()
	{
		var cnpj = "22222222000122";
		var emails = new List<EmailDTO>
			{
				new EmailDTO { Cnpj = cnpj, EnderecoEmail = "teste@gmail.com" },
				new EmailDTO { Cnpj = cnpj, EnderecoEmail = "teste2@gmail.com" }
			};
		_mockEmailService.Setup(s => s.GetByCnpj(cnpj)).Returns(emails);

		var result = _controller.GetByCnpj(cnpj);

		var okResult = Assert.IsType<OkObjectResult>(result.Result);
		var returnedEmails = Assert.IsType<List<EmailDTO>>(okResult.Value);
		Assert.Equal(2, returnedEmails.Count); 
	}

	[Fact]
	public void GetByCnpj_RetornaNotFound_QuandoEmailNaoExistir()
	{
		var cnpj = "22222222000122";
		_mockEmailService.Setup(s => s.GetByCnpj(cnpj)).Returns(new List<EmailDTO>());

		var result = _controller.GetByCnpj(cnpj);

		var notFoundResult = Assert.IsType<NotFoundObjectResult>(result.Result);
		Assert.Equal("Email não encontrado para o cliente com o CNPJ fornecido.", notFoundResult.Value);
	}

	[Fact]
	public void Post_RetornaCreated_QuandoEmailForCriado()
	{
		var emailDTO = new EmailDTO { Cnpj = "22222222000122", EnderecoEmail = "teste@gmail.com" };
		_mockEmailService.Setup(s => s.Create(emailDTO)).Returns(emailDTO);

		var result = _controller.Post(emailDTO);

		var createdResult = Assert.IsType<CreatedAtActionResult>(result.Result);
		var createdEmail = Assert.IsType<EmailDTO>(createdResult.Value);
		Assert.Equal(emailDTO.Cnpj, createdEmail.Cnpj);
	}

	[Fact]
	public void Post_RetornaBadRequest_QuandoErroAoCriarEmail()
	{
		var emailDTO = new EmailDTO { Cnpj = "22222222000122", EnderecoEmail = "teste@gmail.com" };
		_mockEmailService.Setup(s => s.Create(emailDTO)).Throws(new Exception("Erro ao criar email"));

		var result = _controller.Post(emailDTO);

		var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
		Assert.Equal("Erro ao criar e-mail: Erro ao criar email", badRequestResult.Value);
	}

	[Fact]
	public void Delete_RetornaNoContent_QuandoEmailForDeletado()
	{
		var id = 1;
		_mockEmailService.Setup(s => s.Delete(id)).Returns(true);

		var result = _controller.Delete(id);

		Assert.IsType<NoContentResult>(result);
	}

	[Fact]
	public void Delete_RetornaNotFound_QuandoEmailNaoExistir()
	{
		var id = 1;
		_mockEmailService.Setup(s => s.Delete(id)).Returns(false);

		var result = _controller.Delete(id);

		var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
		Assert.Equal("Email não encontrado ou já removido.", notFoundResult.Value);
	}
}