using AutoMapper;
using GerenciadorDeClientes.Application.DTOs;
using GerenciadorDeClientes.Domain.Entities;
using GerenciadorDeClientes.Infrastructure.Integrations.Responses;

namespace GerenciadorDeClientes.Application.Mappings;

public class GerenciadorDeClientesMappingProfile : Profile
{
    public GerenciadorDeClientesMappingProfile()
    {
        CreateMap<Cliente, ClienteDTO>().ReverseMap();
        CreateMap<Cliente, ClienteUpdateDTO>().ReverseMap();
        CreateMap<ClienteDTO, ClienteUpdateDTO>().ReverseMap();
        CreateMap<Endereco, EnderecoDTO>()
            .ForMember(dest => dest.Cnpj, opt => opt.MapFrom(src => src.Cliente.Cnpj)).ReverseMap();
        CreateMap<ViaCepResponse, EnderecoDTO>().ReverseMap();
        CreateMap<ViaCepResponse, Endereco>().ReverseMap();
        CreateMap<Telefone, TelefoneDTO>()
            .ForMember(dest => dest.Cnpj, opt => opt.MapFrom(src => src.Cliente.Cnpj)).ReverseMap();
        CreateMap<Email, EmailDTO>()
            .ForMember(dest => dest.Cnpj, opt => opt.MapFrom(src => src.Cliente.Cnpj))
            .ForMember(dest => dest.EnderecoEmail, opt => opt.MapFrom(src => src.EnderecoEmail))
            .ReverseMap();
    }
}
