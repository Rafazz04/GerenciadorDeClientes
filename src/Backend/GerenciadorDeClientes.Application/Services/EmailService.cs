using AutoMapper;
using FluentValidation;
using GerenciadorDeClientes.Application.DTOs;
using GerenciadorDeClientes.Application.Services.Interfaces;
using GerenciadorDeClientes.Domain.Entities;
using GerenciadorDeClientes.Domain.Interfaces;

namespace GerenciadorDeClientes.Application.Services;

public class EmailService : IEmailService
{
    private readonly IEmailRepository _emailRepository;
    private readonly IClienteRepository _clienteRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
	private readonly IValidator<EmailDTO> _validator;

	public EmailService(IEmailRepository emailRepository, IClienteRepository clienteRepository, IUnitOfWork unitOfWork, IMapper mapper, IValidator<EmailDTO> validator)
    {
        _emailRepository = emailRepository;
        _clienteRepository = clienteRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _validator = validator;
    }

    public IEnumerable<EmailDTO> GetByCnpj(string cnpj)
    {
        var cliente = _clienteRepository.GetByCnpj(cnpj);
        if (cliente == null)
            return null;

        var email = _emailRepository.GetByClienteId(cliente.Id);
        return _mapper.Map<IEnumerable<EmailDTO>>(email);
    }

    public EmailDTO Create(EmailDTO emailDTO)
    {
        try
        {
            if (_validator.Validate(emailDTO).IsValid)
            {
				var cliente = _clienteRepository.GetByCnpj(emailDTO.Cnpj);
				if (cliente == null)
					throw new Exception("Cliente não encontrado!");

				var email = _mapper.Map<Email>(emailDTO);
				email.Cliente = cliente;

				_emailRepository.Create(email);
				if (_unitOfWork.Commit())
					return emailDTO;

				throw new Exception("Erro ao salvar e-mail");
			}
			var validationErrors = string.Join(", ", _validator.Validate(emailDTO).Errors.Select(e => e.ErrorMessage));
			throw new ValidationException($"Erro de validação: {validationErrors}");
		}
        catch (Exception ex) 
        {
			throw new Exception($"{ex.Message} - {ex.StackTrace}");
		}
        
    }

    public bool Delete(int id)
    {
        var email = _emailRepository.GetById(id);
        if (email == null)
            return false;

        _emailRepository.Delete(email);
        return _unitOfWork.Commit();
    }
}
