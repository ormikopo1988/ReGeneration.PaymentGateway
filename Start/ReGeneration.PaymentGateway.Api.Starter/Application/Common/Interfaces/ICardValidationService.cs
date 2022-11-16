namespace ReGeneration.PaymentGateway.Api.Starter.Application.Common.Interfaces
{
	public interface ICardValidationService
	{
		bool ValidateCardDetails(string cardNumber, string cvv);
	}
}
