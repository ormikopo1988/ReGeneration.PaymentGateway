using Microsoft.EntityFrameworkCore;
using ReGeneration.PaymentGateway.Api.Application.Common.Interfaces;

namespace ReGeneration.PaymentGateway.Api.Persistence
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddDbContext<ApplicationDbContext>(options =>
					options.UseSqlServer(
						configuration.GetConnectionString("DefaultConnection"),
						b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

			services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());

			return services;
		}
	}
}
