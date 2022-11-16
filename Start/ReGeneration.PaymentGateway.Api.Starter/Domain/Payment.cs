using ReGeneration.PaymentGateway.Api.Starter.Domain.Common;

namespace ReGeneration.PaymentGateway.Api.Starter.Domain
{
	public class Payment : BaseEntity
	{
		public decimal Amount { get; set; }

		public string Currency { get; set; } = default!;

		public bool Approved { get; set; }

		public string Status { get; set; } = default!;

		public Guid CardId { get; set; }

		public Card Card { get; set; } = default!;
	}
}
