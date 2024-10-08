using AutoMapper;
using GerenciadorDeClientes.Application.DTOs;
using GerenciadorDeClientes.Domain.Entities;

namespace GerenciadorDeClientes.Application.Mappings;

public class GerenciadorDeClientesMappingProfile : Profile
{
    public GerenciadorDeClientesMappingProfile()
    {
        CreateMap<Cliente, ClienteDTO>().ReverseMap();
        CreateMap<Endereco, EnderecoDTO>().ReverseMap();
        CreateMap<Telefone, TelefoneDTO>().ReverseMap();
        CreateMap<Email, EmailDTO>().ReverseMap();
    }
}
