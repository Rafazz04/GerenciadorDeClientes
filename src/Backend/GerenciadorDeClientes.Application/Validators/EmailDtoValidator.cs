using FluentValidation;
using GerenciadorDeClientes.Application.DTOs;

namespace GerenciadorDeClientes.Application.Validators;

public class EmailDtoValidator : AbstractValidator<EmailDTO>
{
    public EmailDtoValidator()
    {
		RuleFor(email => email.EnderecoEmail)
				.NotEmpty().WithMessage("O endereço de e-mail é obrigatório")
				.EmailAddress().WithMessage("O e-mail deve ser válido")
				.MaximumLength(100).WithMessage("O e-mail deve ter no máximo 100 caracteres");
		RuleFor(email => email.Cnpj).NotEmpty().WithMessage("Informe o cnpj que deseja incluir este email");
	}
}
