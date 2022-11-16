using ReGeneration.PaymentGateway.Api.Starter.Domain.Common;

namespace ReGeneration.PaymentGateway.Api.Starter.Domain
{
	public class Card : BaseEntity
	{
		public string CardNumber { get; set; } = default!;

		public short ExpiryMonth { get; set; }

		public short ExpiryYear { get; set; }

		public string CVV { get; set; } = default!;

		public IList<Payment> Payments { get; private set; } = new List<Payment>();
	}
}
