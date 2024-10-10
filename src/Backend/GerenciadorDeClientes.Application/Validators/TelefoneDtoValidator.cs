using FluentValidation;
using GerenciadorDeClientes.Application.DTOs;

namespace GerenciadorDeClientes.Application.Validators;

public class TelefoneDtoValidator : AbstractValidator<TelefoneDTO>
{
    public TelefoneDtoValidator()
    {
		RuleFor(telefone => telefone.Numero)
				.NotEmpty().WithMessage("Número de telefone é obrigatório")
				.MaximumLength(15).WithMessage("Número de telefone deve ter no máximo 15 caracteres");
		RuleFor(telefone => telefone.Cnpj).NotEmpty().WithMessage("Informe o cnpj que deseja incluir o numero");
	}
}
