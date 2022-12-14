using ReGeneration.PaymentGateway.Api.Starter.Domain;

namespace ReGeneration.PaymentGateway.Api.Starter.Persistence
{
	public static class ApplicationDbContextSeed
	{
		public static async Task SeedSampleDataAsync(ApplicationDbContext context)
		{
			// Seed cards, if needed
			if (!context.Cards.Any())
			{
				context.Cards.Add(new Card
				{
					CardNumber = "4305894378655005",
					CVV = "123",
					ExpiryMonth = 2,
					ExpiryYear = 2025
				});

				await context.SaveChangesAsync();
			}
		}
	}
}
