namespace ReGeneration.PaymentGateway.Api.Contracts.Responses
{
	/// <summary>
	/// The payment details response
	/// </summary>
	public class PaymentBaseResponse
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
	}
}
