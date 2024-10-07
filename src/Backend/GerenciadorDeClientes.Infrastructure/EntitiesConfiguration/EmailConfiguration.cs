using GerenciadorDeClientes.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GerenciadorDeClientes.Infrastructure.EntitiesConfiguration;

public class EmailConfiguration : IEntityTypeConfiguration<Email>
{
	public void Configure(EntityTypeBuilder<Email> builder)
	{
		builder.HasKey(e => e);

		builder.Property(e => e.EnderecoEmail)
			.HasMaxLength(100)
			.IsRequired();

		builder.HasOne(e => e.Cliente)
			   .WithMany(c => c.Emails)
			   .HasForeignKey(e => e.ClienteId)
			   .OnDelete(DeleteBehavior.Cascade);
	}
}
