using GerenciadorDeClientes.Application.Mappings;
using GerenciadorDeClientes.Application.Services;
using GerenciadorDeClientes.Application.Services.Interfaces;
using GerenciadorDeClientes.Domain.Interfaces;
using GerenciadorDeClientes.Infrastructure.DataAcess;
using GerenciadorDeClientes.Infrastructure.Integrations.Refit;
using GerenciadorDeClientes.Infrastructure.Integrations.Services;
using GerenciadorDeClientes.Infrastructure.Integrations.Services.Interfaces;
using GerenciadorDeClientes.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Refit;

namespace GerenciadorDeClientes.CrossCutting.IoC;

public static class DependencyInjectionExtensions
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        AddDbContext_SqlServer(services, configuration);
        AddIntegrations_ViaCep(services, configuration);
        AddRepositories(services);
        AddServices(services);
    }
    private static void AddDbContext_SqlServer(IServiceCollection services, IConfiguration configuration)
    {
        var connectiontring = configuration.GetConnectionString("DefaultConnection");
        services.AddDbContext<GerenciadorDeClientesDbContext>(ctx =>
        {
            ctx.UseSqlServer(connectiontring, opt => opt.MigrationsAssembly("GerenciadorDeClientes.Infrastructure"));
        });
    }
    private static void AddIntegrations_ViaCep(IServiceCollection services, IConfiguration configuration) 
    {
        var baseUrl = configuration["ViaCepSettings:BaseUrl"];
        services.AddRefitClient<IViaCepIntegracaoRefit>().ConfigureHttpClient(opt =>
        {
            opt.BaseAddress = new Uri(baseUrl);
        });   

        services.AddScoped<IViaCepIntegracao, ViaCepIntegracao>();
    }
    private static void AddRepositories(IServiceCollection services)
    {
        services.AddScoped(typeof(IRepositoryBase<>), typeof(RepositoryBase<>));
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IClienteRepository, ClienteRepository>();
        services.AddScoped<IEnderecoRepository, EnderecoRepository>();
        services.AddScoped<ITelefoneRepository, TelefoneRepository>();
        services.AddScoped<IEmailRepository, EmailRepository>();
    }

    private static void AddServices(IServiceCollection services)
    {
        services.AddScoped<IClienteService, ClienteService>();
        //services.AddScoped<ITelefoneService, TelefoneService>();
        //services.AddScoped<IEmailService, EmailService>();
        //services.AddScoped<IEnderecoService, EnderecoService>();
        services.AddAutoMapper(typeof(GerenciadorDeClientesMappingProfile));
    }
}
