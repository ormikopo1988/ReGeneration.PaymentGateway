namespace ReGeneration.PaymentGateway.Api.Application.Common.Interfaces
{
	public interface ICardValidationService
	{
		bool ValidateCardDetails(string cardNumber, string cvv);
	}
}
