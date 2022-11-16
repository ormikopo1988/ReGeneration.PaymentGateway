using Microsoft.EntityFrameworkCore;
using ReGeneration.PaymentGateway.Api.Starter.Application.Common.Interfaces;
using ReGeneration.PaymentGateway.Api.Starter.Domain;
using System.Reflection;

namespace ReGeneration.PaymentGateway.Api.Starter.Persistence
{
	public class ApplicationDbContext : DbContext, IApplicationDbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
			: base(options)
		{
		}

		public DbSet<Payment> Payments => Set<Payment>();

		public DbSet<Card> Cards => Set<Card>();

		protected override void OnModelCreating(ModelBuilder builder)
		{
			builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

			base.OnModelCreating(builder);
		}
	}
}
