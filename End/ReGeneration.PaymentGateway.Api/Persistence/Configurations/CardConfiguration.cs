using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using ReGeneration.PaymentGateway.Api.Domain;

namespace ReGeneration.PaymentGateway.Api.Persistence.Configurations
{
	public class CardConfiguration : IEntityTypeConfiguration<Card>
	{
		public void Configure(EntityTypeBuilder<Card> builder)
		{
			builder.ToTable("Card");

			builder.Property(c => c.CardNumber)
				.IsRequired()
				.HasMaxLength(19);

			builder.Property(c => c.ExpiryMonth)
				.IsRequired();

			builder.Property(c => c.ExpiryYear)
				.IsRequired();

			builder.Property(c => c.CVV)
				.IsRequired()
				.HasMaxLength(4);
		}
	}
}
