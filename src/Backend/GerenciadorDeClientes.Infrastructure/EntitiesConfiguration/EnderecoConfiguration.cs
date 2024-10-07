using GerenciadorDeClientes.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GerenciadorDeClientes.Infrastructure.EntitiesConfiguration;

public class EnderecoConfiguration : IEntityTypeConfiguration<Endereco>
{
	public void Configure(EntityTypeBuilder<Endereco> builder)
	{
		builder.HasKey(e => e.Id);

		builder.Property(e => e.Logradouro)
			.HasMaxLength(200)
			.IsRequired();

		builder.Property(e => e.Numero)
			.HasMaxLength(10);

		builder.Property(e => e.Complemento)
			.HasMaxLength(50);

		builder.Property(e => e.Bairro)
			.HasMaxLength(200);

		builder.Property(e => e.Cidade)
			.IsRequired()
			.HasMaxLength(100);

		builder.Property(e => e.Estado)
			.IsRequired()
			.HasMaxLength(100);

		builder.Property(e => e.Cep)
			.HasMaxLength(9)
			.IsRequired();

		builder.HasOne(e => e.Cliente)
			   .WithMany(c => c.Enderecos)
			   .HasForeignKey(e => e.ClienteId)
			   .OnDelete(DeleteBehavior.Cascade);
	}
}
