using GerenciadorDeClientes.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GerenciadorDeClientes.Infrastructure.DataAcess;

public class GerenciadorDeClientesDbContext : DbContext
{
    public GerenciadorDeClientesDbContext(DbContextOptions opt) : base(opt) {}
    public DbSet<Cliente> CLIENTE { get; set; }
    public DbSet<Endereco> ENDERECO { get; set; }
    public DbSet<Telefone> TELEFONE { get; set; }
    public DbSet<Email> EMAIL { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(typeof(GerenciadorDeClientesDbContext).Assembly);
    }
}
