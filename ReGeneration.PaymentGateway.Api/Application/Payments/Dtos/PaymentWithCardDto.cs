namespace ReGeneration.PaymentGateway.Api.Application.Payments.Dtos
{
	public class PaymentWithCardDto : PaymentBaseDto
	{
		public CardDto Source { get; set; } = default!;
	}
}
