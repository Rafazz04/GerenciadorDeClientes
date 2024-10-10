using AutoMapper;
using GerenciadorDeClientes.Application.DTOs;
using GerenciadorDeClientes.Application.Services.Interfaces;
using GerenciadorDeClientes.Communication.Utils;
using GerenciadorDeClientes.Domain.Entities;
using GerenciadorDeClientes.Domain.Interfaces;
using GerenciadorDeClientes.Infrastructure.Integrations.Services.Interfaces;
using System.Runtime.ConstrainedExecution;
using X.PagedList;

namespace GerenciadorDeClientes.Application.Services;

public class ClienteService : IClienteService
{
	private IClienteRepository _clienteRepository;
	private IEnderecoRepository _enderecoRepository;
	private ITelefoneRepository _telefoneRepository;
	private IEmailRepository _emailRepository;
	private IViaCepIntegracao _viaCepIntegracao;
    private IUnitOfWork _unitOfWork;
	private readonly IMapper _mapper;

	public ClienteService(IClienteRepository clienteRepository, IEmailRepository emailRepository, ITelefoneRepository telefoneRepository, IEnderecoRepository enderecoRepository, IViaCepIntegracao viaCepIntegracao, IUnitOfWork unitOfWork, IMapper mapper)
	{
		_clienteRepository = clienteRepository;
		_enderecoRepository = enderecoRepository;
		_emailRepository = emailRepository;
		_telefoneRepository = telefoneRepository;
		_viaCepIntegracao = viaCepIntegracao;
		_unitOfWork = unitOfWork;
		_mapper = mapper;
	}

	public IEnumerable<ClienteDTO> GetAll()
	{
		var cliente =  _clienteRepository.GetAll();
		return _mapper.Map<IEnumerable<ClienteDTO>>(cliente);
	}
	public IPagedList<ClienteDTO> GetAllPaginated(int pageNumber, int pageSize)
	{
		var clientes = _clienteRepository.GetAllPaginated(pageNumber, pageSize);
		return _mapper.Map<IPagedList<ClienteDTO>>(clientes);
	}

	public ClienteDTO GetById(int id)
	{
		var cliente =  _clienteRepository.GetById(id);
		return _mapper.Map<ClienteDTO>(cliente);
	}

	public  ClienteDTO GetByCnpj(string cnpj)
	{
		var cnpjLimpo = Util.LimpaCnpj(cnpj);
		var cliente =  _clienteRepository.GetByCnpj(cnpjLimpo);
		return _mapper.Map<ClienteDTO>(cliente);
	}

	public ClienteDTO Create(ClienteDTO clienteDTO)
	{
		try
		{
			var cliente =  CriaCliente(clienteDTO);
			if(_unitOfWork.Commit())
			{
				var endereco = CriaEndereco(clienteDTO.Cep, cliente.Id);

				var telefone = new Telefone
				{
					ClienteId = cliente.Id,
					Numero = clienteDTO.Celular
				};

                var email = new Email
                {
                    EnderecoEmail = clienteDTO.Email, 
                    ClienteId = cliente.Id              
                };

                 _telefoneRepository.Create(telefone);
                 _emailRepository.Create(email);

                if(_unitOfWork.Commit())
					return clienteDTO;
            }
			throw new Exception("Erro ao adicionar cliente");
		}
		catch (Exception ex) 
		{
			throw new Exception($"{ex.Message} - {ex.StackTrace}");
		}
		
	}

	public ClienteDTO Update(string cnpj, ClienteUpdateDTO clienteUpdateDTO)
	{
		try
		{
            var cliente =  AtualizaCliente(cnpj,clienteUpdateDTO);
            if (cliente != null &&  _unitOfWork.Commit())
			{
                var endereco =  AtualizaEndereco(clienteUpdateDTO.Cep, cliente.Id);

                var telefone = new Telefone
                {
                    ClienteId = cliente.Id,
                    Numero = clienteUpdateDTO.Celular
                };

                var email = new Email
                {
                    EnderecoEmail = clienteUpdateDTO.Email,
                    ClienteId = cliente.Id
                };

                 _telefoneRepository.Update(telefone);
                 _emailRepository.Update(email);

                if (_unitOfWork.Commit())
					return _mapper.Map<ClienteDTO>(clienteUpdateDTO); 
            }   
			throw new Exception("Erro ao atualizar cliente");
		}
		catch (Exception ex) 
		{
			throw new Exception($"{ex.Message} - {ex.StackTrace}");
		}

	}
	public bool Delete(string cnpj)
	{
		try
		{
			var cliente = _clienteRepository.GetByCnpj(cnpj);
			if (cliente != null)
				 _clienteRepository.Delete(cliente);
			throw new Exception($"Cliente não encontrado");
		}
		catch (Exception ex) 
		{
			throw new Exception($"{ex.Message} - {ex.StackTrace}");
		}
	}
    #region Métodos Adicionais
    private Endereco CriaEndereco(string cep, int clienteId)
	{
        var viaCepResponse = _viaCepIntegracao.ObterDadosViaCep(Util.LimpaCep(cep));
		var endereco = _mapper.Map<Endereco>(viaCepResponse);
		endereco.ClienteId = clienteId;
		return  _enderecoRepository.Create(endereco);
	}
    private Endereco AtualizaEndereco(string cep, int clienteId)
    {
        var viaCepResponse = _viaCepIntegracao.ObterDadosViaCep(Util.LimpaCep(cep));
        var endereco = _mapper.Map<Endereco>(viaCepResponse);
        endereco.ClienteId = clienteId;
        return _enderecoRepository.Update(endereco);
    }
    private Cliente CriaCliente(ClienteDTO clienteDTO)
	{
        clienteDTO.Cnpj = Util.LimpaCnpj(clienteDTO.Cnpj);
        var cliente = _mapper.Map<Cliente>(clienteDTO);
        return _clienteRepository.Create(cliente);
    }
    private Cliente AtualizaCliente(string cnpj, ClienteUpdateDTO clienteUpdateDTO)
    {
        cnpj = Util.LimpaCnpj(cnpj);
        var cliente = _mapper.Map<Cliente>(clienteUpdateDTO);
		if(_clienteRepository.GetByCnpj(cnpj) != null)
			return  _clienteRepository.Update(cliente);
		return null;
    }
    #endregion
}
