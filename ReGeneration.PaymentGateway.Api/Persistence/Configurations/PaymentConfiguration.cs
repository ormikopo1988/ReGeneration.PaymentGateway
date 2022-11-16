using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using ReGeneration.PaymentGateway.Api.Entities;

namespace ReGeneration.PaymentGateway.Api.Persistence.Configurations
{
	public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
	{
		public void Configure(EntityTypeBuilder<Payment> builder)
		{
			builder.ToTable("Payment");

			builder.Property(p => p.Amount)
				.IsRequired();

			builder.Property(p => p.Currency)
				.IsRequired()
				.HasMaxLength(3);

			builder.Property(p => p.Approved)
				.IsRequired();

			builder.Property(p => p.Status)
				.IsRequired()
				.HasMaxLength(50);

			builder
				.HasOne(p => p.Card)
				.WithMany(c => c.Payments)
				.HasForeignKey(p => p.CardId);
		}
	}
}
