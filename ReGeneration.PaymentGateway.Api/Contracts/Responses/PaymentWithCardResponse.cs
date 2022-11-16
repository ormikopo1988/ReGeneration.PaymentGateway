namespace ReGeneration.PaymentGateway.Api.Contracts.Responses
{
	/// <summary>
	/// The payment (including card details) response
	/// </summary>
	public class PaymentWithCardResponse : PaymentBaseResponse
	{
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
