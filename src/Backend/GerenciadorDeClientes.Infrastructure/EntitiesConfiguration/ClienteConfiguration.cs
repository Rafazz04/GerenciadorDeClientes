using GerenciadorDeClientes.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GerenciadorDeClientes.Infrastructure.EntitiesConfiguration;

public class ClienteConfiguration : IEntityTypeConfiguration<Cliente>
{
	public void Configure(EntityTypeBuilder<Cliente> builder)
	{

		builder.HasKey(c => c.Id);

		builder.Property(c => c.Nome)
			.HasMaxLength(100)
			.IsRequired();

		builder.Property(c => c.Cnpj)
			.HasColumnType("CHAR(14)")
			.IsUnicode()
			.IsRequired();
		builder.HasIndex(c => c.Cnpj)
			.IsUnique();

		builder.Property(c => c.FlaAtivo).IsRequired();

		builder.HasMany(c => c.Enderecos)
			.WithOne(e => e.Cliente)
			.HasForeignKey(e => e.ClienteId)
			.OnDelete(DeleteBehavior.Cascade);

		builder.HasMany(c => c.Telefones)
			.WithOne(t => t.Cliente)
			.HasForeignKey(t => t.ClienteId)
			.OnDelete(DeleteBehavior.Cascade);

		builder.HasMany(c => c.Emails)
			   .WithOne(e => e.Cliente)
			   .HasForeignKey(e => e.ClienteId)
			   .OnDelete(DeleteBehavior.Cascade);

	}
}
