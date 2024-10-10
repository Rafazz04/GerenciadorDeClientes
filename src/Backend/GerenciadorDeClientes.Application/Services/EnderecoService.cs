using AutoMapper;
using GerenciadorDeClientes.Application.DTOs;
using GerenciadorDeClientes.Application.Services.Interfaces;
using GerenciadorDeClientes.Domain.Entities;
using GerenciadorDeClientes.Domain.Interfaces;

namespace GerenciadorDeClientes.Application.Services;

public class EnderecoService : IEnderecoService
{
    private readonly IEnderecoRepository _enderecoRepository;
    private readonly IClienteRepository _clienteRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public EnderecoService(IEnderecoRepository enderecoRepository, IClienteRepository clienteRepository, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _enderecoRepository = enderecoRepository;
        _clienteRepository = clienteRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    public IEnumerable<EnderecoDTO> GetByCnpj(string cnpj)
    {
        var cliente = _clienteRepository.GetByCnpj(cnpj);
        if (cliente == null)
            return null;

        var enderecos = _enderecoRepository.GetByClienteId(cliente.Id);
        return _mapper.Map<IEnumerable<EnderecoDTO>>(enderecos);
    }

    public EnderecoDTO Create(EnderecoDTO enderecoDTO)
    {
        var cliente = _clienteRepository.GetByCnpj(enderecoDTO.Cnpj);
        if (cliente == null)
            throw new Exception("Cliente não encontrado!");

        var endereco = _mapper.Map<Endereco>(enderecoDTO);
        endereco.Cliente = cliente;

        _enderecoRepository.Create(endereco);
        if (_unitOfWork.Commit())
            return enderecoDTO;

        throw new Exception("Erro ao salvar endereço");
    }

    public bool Delete(int id)
    {
        var endereco = _enderecoRepository.GetById(id);
        if (endereco == null)
            return false;

        _enderecoRepository.Delete(endereco);
        return _unitOfWork.Commit();
    }
}
