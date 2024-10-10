using AutoMapper;
using FluentValidation;
using GerenciadorDeClientes.Application.DTOs;
using GerenciadorDeClientes.Application.Services.Interfaces;
using GerenciadorDeClientes.Domain.Entities;
using GerenciadorDeClientes.Domain.Interfaces;

namespace GerenciadorDeClientes.Application.Services;

public class TelefoneService : ITelefoneService
{
    private readonly ITelefoneRepository _telefoneRepository;
    private readonly IClienteRepository _clienteRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
	private readonly IValidator<TelefoneDTO> _validator;

	public TelefoneService(ITelefoneRepository telefoneRepository,IClienteRepository clienteRepository,IUnitOfWork unitOfWork,IMapper mapper, IValidator<TelefoneDTO> validator)
    {
        _telefoneRepository = telefoneRepository;
        _clienteRepository = clienteRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _validator = validator;
    }

    public IEnumerable<TelefoneDTO> GetByCnpj(string cnpj)
    {
        var cliente = _clienteRepository.GetByCnpj(cnpj);
        if (cliente == null)
            return null;

        var telefone = _telefoneRepository.GetByClienteId(cliente.Id);
        return _mapper.Map<IEnumerable<TelefoneDTO>>(telefone);
    }

    public TelefoneDTO Create(TelefoneDTO telefoneDTO)
    {
        try
        {
            if (_validator.Validate(telefoneDTO).IsValid)
            {
				var cliente = _clienteRepository.GetByCnpj(telefoneDTO.Cnpj);
				if (cliente == null)
					throw new Exception("Cliente não encontrado!");

				var telefone = _mapper.Map<Telefone>(telefoneDTO);
				telefone.Cliente = cliente;

				_telefoneRepository.Create(telefone);
				if (_unitOfWork.Commit())
					return telefoneDTO;
				throw new Exception("Erro ao salvar telefone");
			}
			var validationErrors = string.Join(", ", _validator.Validate(telefoneDTO).Errors.Select(e => e.ErrorMessage));
			throw new ValidationException($"Erro de validação: {validationErrors}");
		}
        catch (Exception ex) 
        {
			throw new Exception($"{ex.Message} - {ex.StackTrace}");
		}
        
    }

    public bool Delete(int id)
    {
        var telefone = _telefoneRepository.GetById(id);
        if (telefone == null)
            return false;

        _telefoneRepository.Delete(telefone);
        return _unitOfWork.Commit();
    }
}
