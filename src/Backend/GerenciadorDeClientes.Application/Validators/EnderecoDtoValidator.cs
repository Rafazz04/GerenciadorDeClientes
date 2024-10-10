using FluentValidation;
using GerenciadorDeClientes.Application.DTOs;

namespace GerenciadorDeClientes.Application.Validators;

public class EnderecoDtoValidator : AbstractValidator<EnderecoDTO>
{
    public EnderecoDtoValidator()
	{
		RuleFor(endereco => endereco.Cnpj).NotEmpty().WithMessage("Informe o cnpj que deseja incluir este endereço");

		RuleFor(endereco => endereco.Logradouro)
			.NotEmpty().WithMessage("Logradouro é obrigatório")
			.MaximumLength(200).WithMessage("Logradouro deve ter no máximo 200 caracteres");

		RuleFor(endereco => endereco.Numero)
			.MaximumLength(10).WithMessage("Número deve ter no máximo 10 caracteres");

		RuleFor(endereco => endereco.Complemento)
			.MaximumLength(50).WithMessage("Complemento deve ter no máximo 50 caracteres")
			.When(endereco => !string.IsNullOrWhiteSpace(endereco.Complemento));

		RuleFor(endereco => endereco.Bairro)
			.MaximumLength(200).WithMessage("Bairro deve ter no máximo 200 caracteres");

		RuleFor(endereco => endereco.Cidade)
			.MaximumLength(100).WithMessage("Cidade deve ter no máximo 100 caracteres");

		RuleFor(endereco => endereco.Estado)
			.MaximumLength(100).WithMessage("Estado deve ter no máximo 100 caracteres");

		RuleFor(endereco => endereco.Cep)
			.NotEmpty().WithMessage("CEP é obrigatório")
			.MaximumLength(9).WithMessage("CEP deve ter no máximo 9 caracteres");
	}
}
