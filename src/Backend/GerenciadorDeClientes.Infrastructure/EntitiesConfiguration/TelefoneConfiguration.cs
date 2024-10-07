using GerenciadorDeClientes.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GerenciadorDeClientes.Infrastructure.EntitiesConfiguration;

public class TelefoneConfiguration : IEntityTypeConfiguration<Telefone>
{
	public void Configure(EntityTypeBuilder<Telefone> builder)
	{
		builder.HasKey(t => t.Id);

		builder.Property(t => t.Numero)
			.IsRequired()
			.HasMaxLength(15); 

		builder.HasOne(t => t.Cliente)
			   .WithMany(c => c.Telefones)
			   .HasForeignKey(t => t.ClienteId)
			   .OnDelete(DeleteBehavior.Cascade);
	}
}
