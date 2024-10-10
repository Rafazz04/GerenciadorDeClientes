using FluentValidation;
using GerenciadorDeClientes.Application.DTOs;

namespace GerenciadorDeClientes.Application.Validators;

public class ClienteDtoValidator : AbstractValidator<ClienteDTO>
{
    public ClienteDtoValidator()
    {
        RuleFor(cliente => cliente.Nome).NotEmpty().WithMessage("O nome é um campo obrigatório");
        RuleFor(cliente => cliente.Cnpj).NotEmpty().WithMessage("O Cnpj é um campo obrigatório");
        RuleFor(cliente => cliente.Email).EmailAddress().NotEmpty().WithMessage("O campo email é obrigatório");
        RuleFor(cliente => cliente.FlaAtivo).NotEmpty().WithMessage("Informe se o cliente estará Ativo");
		RuleFor(cliente => cliente.Celular).NotEmpty().WithMessage("O Celular é um campo obrigatório");

	}
}
