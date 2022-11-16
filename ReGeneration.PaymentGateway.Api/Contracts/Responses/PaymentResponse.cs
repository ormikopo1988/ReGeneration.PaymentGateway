namespace ReGeneration.PaymentGateway.Api.Contracts.Responses
{
	/// <summary>
	/// The create payment response
	/// </summary>
	public class PaymentResponse
	{
		/// <summary>
		/// The id of the created payment
		/// </summary>
		public Guid Id { get; init; }

		/// <summary>
		/// The amount of the created payment
		/// </summary>
		public decimal Amount { get; init; }

		/// <summary>
		/// The currency of the created payment
		/// </summary>
		public string Currency { get; init; } = default!;

		/// <summary>
		/// Whether the payment request was approved or not
		/// </summary>
		public bool Approved { get; init; }

		/// <summary>
		/// The status of the payment (e.g. Valid)
		/// </summary>
		public string Status { get; init; } = default!;

		/// <summary>
		/// The card details of the created payment
		/// </summary>
		public CardDetails Source { get; init; } = default!;
	}

	/// <summary>
	/// The card details
	/// </summary>
	public class CardDetails
	{
		/// <summary>
		/// The masked card number
		/// </summary>
		public string MaskedCardNumber { get; init; } = default!;

		/// <summary>
		/// The card expiry month
		/// </summary>
		public int ExpiryMonth { get; init; }

		/// <summary>
		/// The card expiry year
		/// </summary>
		public int ExpiryYear { get; init; }

		/// <summary>
		/// The masked card CVV
		/// </summary>
		public string MaskedCVV { get; init; } = default!;
	}
}
