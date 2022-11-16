using Microsoft.EntityFrameworkCore;
using ReGeneration.PaymentGateway.Api.Entities;
using System.Reflection;
using ReGeneration.PaymentGateway.Api.Application.Common.Interfaces;

namespace ReGeneration.PaymentGateway.Api.Persistence
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
