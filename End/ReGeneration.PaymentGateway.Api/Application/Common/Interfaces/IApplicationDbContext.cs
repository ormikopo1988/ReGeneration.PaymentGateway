using Microsoft.EntityFrameworkCore;
using ReGeneration.PaymentGateway.Api.Domain;

namespace ReGeneration.PaymentGateway.Api.Application.Common.Interfaces
{
	public interface IApplicationDbContext : IDisposable
	{
		DbSet<TEntity> Set<TEntity>() where TEntity : class;

		DbSet<Payment> Payments { get; }

		DbSet<Card> Cards { get; }

		Task<int> SaveChangesAsync(CancellationToken cancellationToken);
	}
}
