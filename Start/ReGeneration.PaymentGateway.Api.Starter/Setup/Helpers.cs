using Microsoft.EntityFrameworkCore;
using ReGeneration.PaymentGateway.Api.Starter.Persistence;

namespace ReGeneration.PaymentGateway.Api.Starter.Setup
{
	/// <summary>
	/// Helper methods
	/// </summary>
	public static class Helpers
	{
		/// <summary>
		/// Migrate and seed sample data into the db
		/// </summary>
		/// <param name="app"></param>
		/// <returns></returns>
		public static async Task MigrateDbIfNeededAndSeedItWithSampleDataAsync(WebApplication app)
		{
			var serviceScopeFactory = app.Services.GetRequiredService<IServiceScopeFactory>();

			using var scope = serviceScopeFactory.CreateScope();

			var services = scope.ServiceProvider;

			var context = services.GetRequiredService<ApplicationDbContext>();

			if (context.Database.IsSqlServer())
			{
				context.Database.Migrate();
			}

			if (app.Environment.IsDevelopment())
			{
				await ApplicationDbContextSeed.SeedSampleDataAsync(context);
			}
		}
	}
}
