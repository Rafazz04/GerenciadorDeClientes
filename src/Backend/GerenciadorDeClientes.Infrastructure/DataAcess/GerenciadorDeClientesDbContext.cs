using GerenciadorDeClientes.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GerenciadorDeClientes.Infrastructure.DataAcess;

public class GerenciadorDeClientesDbContext : DbContext
{
    public GerenciadorDeClientesDbContext(DbContextOptions opt) : base(opt) {}
    public DbSet<Cliente> Clientes { get; set; }
    public DbSet<Endereco> Enderecos { get; set; }
    public DbSet<Telefone> Telefones { get; set; }
    public DbSet<Email> Emails { get; set; }
}
