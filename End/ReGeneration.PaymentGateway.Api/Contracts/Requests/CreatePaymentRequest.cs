namespace ReGeneration.PaymentGateway.Api.Contracts.Requests
{
	/// <summary>
	/// Request for creating a new payment
	/// </summary>
	public class CreatePaymentRequest
	{
		/// <summary>
		/// The request payment amount
		/// </summary>
		public decimal Amount { get; init; }

		/// <summary>
		/// The payment request currency (e.g. "EUR", "USD", etc.)
		/// </summary>
		public string Currency { get; init; } = default!;

		/// <summary>
		/// The details of the payment request source
		/// </summary>
		public PaymentRequestSource Source { get; init; } = default!;
	}

	/// <summary>
	/// Details of the payment request source
	/// </summary>
	public class PaymentRequestSource
	{
		/// <summary>
		/// The type of payment (e.g. card)
		/// </summary>
		public string Type { get; init; } = default!;

		/// <summary>
		/// The card number
		/// </summary>
		public string Number { get; init; } = default!;

		/// <summary>
		/// The card expiry month
		/// </summary>
		public int ExpiryMonth { get; init; }

		/// <summary>
		/// The card expiry year
		/// </summary>
		public int ExpiryYear { get; init; }

		/// <summary>
		/// The card CVV
		/// </summary>
		public string CVV { get; init; } = default!;
	}
}
