namespace ReGeneration.PaymentGateway.Api.Application.Common.Models
{
	public class Result<T> where T : class
	{
		public Error Error { get; set; } = default!;

		public T Data { get; set; } = default!;

		public Result()
		{
		}
	}

	public class Error
	{
		public ErrorCode ErrorCode { get; set; }

		public string Message { get; set; } = default!;
	}

	public enum ErrorCode
	{
		Unspecified = 0,
		UnsuccessfulPaymentValidation = 1,
		MismatchingCardDetails = 2,
		InsufficientFunds = 3,
		InvalidCard = 4
	}
}
