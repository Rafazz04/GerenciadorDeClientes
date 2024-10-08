using AutoMapper;
using GerenciadorDeClientes.Application.DTOs;
using GerenciadorDeClientes.Application.Services.Interfaces;
using GerenciadorDeClientes.Communication.Utils;
using GerenciadorDeClientes.Domain.Entities;
using GerenciadorDeClientes.Domain.Interfaces;

namespace GerenciadorDeClientes.Application.Services;

public class ClienteService : IClienteService
{
	private IClienteRepository _clienteRepository;
	private IUnitOfWork _unitOfWork;
	private readonly IMapper _mapper;

	public ClienteService(IClienteRepository clienteRepository, IUnitOfWork unitOfWork, IMapper mapper)
	{
		_clienteRepository = clienteRepository;
		_unitOfWork = unitOfWork;
		_mapper = mapper;
	}

	public async Task<IEnumerable<ClienteDTO>> GetAll()
	{
		var cliente = await _clienteRepository.GetAll();
		return _mapper.Map<IEnumerable<ClienteDTO>>(cliente);
	} 

	public async Task<ClienteDTO> GetById(int id)
	{
		var cliente = await _clienteRepository.GetById(id);
		return _mapper.Map<ClienteDTO>(cliente);
	}

	public async Task<ClienteDTO> GetByCnpj(string cnpj)
	{
		var cnpjLimpo = Util.LimpaCnpj(cnpj);
		var cliente = await _clienteRepository.GetByCnpj(cnpjLimpo);
		return _mapper.Map<ClienteDTO>(cliente);
	}

	public async Task<ClienteDTO> Create(ClienteDTO clienteDTO)
	{
		try
		{
			clienteDTO.Cnpj = Util.LimpaCnpj(clienteDTO.Cnpj);
			var cliente = _mapper.Map<Cliente>(clienteDTO);
			await _clienteRepository.Create(cliente);
			if(await _unitOfWork.Commit())
				return clienteDTO;
			throw new Exception("Erro ao adicionar cliente");
		}
		catch (Exception ex) 
		{
			throw new Exception($"{ex.Message} - {ex.StackTrace}");
		}
		
	}

	public async Task<ClienteDTO> Update(ClienteDTO clienteDTO)
	{
		try
		{
			clienteDTO.Cnpj = Util.LimpaCnpj(clienteDTO.Cnpj);
			var cliente = _mapper.Map<Cliente>(clienteDTO);
			await _clienteRepository.Update(cliente);
			if (await _unitOfWork.Commit())
				return clienteDTO;
			throw new Exception("Erro ao atualizar cliente");
		}
		catch (Exception ex) 
		{
			throw new Exception($"{ex.Message} - {ex.StackTrace}");
		}

	}

	public async Task Delete(int id)
	{
		try
		{
			var cliente = _clienteRepository.GetById(id).Result;
			if (cliente != null)
				await _clienteRepository.Delete(cliente);
			throw new Exception($"Cliente não encontrado");
		}
		catch (Exception ex) 
		{
			throw new Exception($"{ex.Message} - {ex.StackTrace}");
		}
	}
}
