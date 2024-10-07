using GerenciadorDeClientes.Domain.Interfaces;
using GerenciadorDeClientes.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace GerenciadorDeClientes.CrossCutting.IoC;

public static class DependencyInjectionExtensions
{
    public static void AddRepositories(this IServiceCollection services)
    {
        services.AddScoped(typeof(IRepositoryBase<>), typeof(RepositoryBase<>));
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IClienteRepository, ClienteRepository>();
        services.AddScoped<IEnderecoRepository, EnderecoRepository>();
        services.AddScoped<ITelefoneRepository, TelefoneRepository>();
        services.AddScoped<IEmailRepository, EmailRepository>();
    }

    public static void AddServices(this IServiceCollection services)
    {

    }
}
