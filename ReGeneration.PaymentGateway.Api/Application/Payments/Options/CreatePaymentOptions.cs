using ReGeneration.PaymentGateway.Api.Contracts.Requests;

namespace ReGeneration.PaymentGateway.Api.Application.Payments.Options
{
	/// <summary>
	/// The CreatePaymentOptions with all the details for creating a new payment
	/// </summary>
	public class CreatePaymentOptions
	{
		public decimal Amount { get; init; }

		public string Currency { get; init; } = default!;

		public PaymentRequestSource Source { get; set; } = default!;
	}
}
