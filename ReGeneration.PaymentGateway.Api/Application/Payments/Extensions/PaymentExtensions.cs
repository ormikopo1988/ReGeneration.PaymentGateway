using ReGeneration.PaymentGateway.Api.Application.Payments.Dtos;
using ReGeneration.PaymentGateway.Api.Application.Payments.Options;
using ReGeneration.PaymentGateway.Api.Contracts.Requests;
using ReGeneration.PaymentGateway.Api.Contracts.Responses;
using ReGeneration.PaymentGateway.Api.Entities;

namespace ReGeneration.PaymentGateway.Api.Application.Payments.Extensions
{
    public static class PaymentExtensions
    {
        public static PaymentResponse ToPaymentResponse(this PaymentDto paymentDto)
        {
            return new PaymentResponse
            {
                Amount = paymentDto.Amount,
                Currency = paymentDto.Currency,
                Id = paymentDto.Id,
                Approved = paymentDto.Approved,
                Status = paymentDto.Status,
                Source = new CardDetails
                {
                    MaskedCVV = paymentDto.Source.MaskedCVV,
                    MaskedCardNumber = paymentDto.Source.MaskedCardNumber,
                    ExpiryMonth = paymentDto.Source.ExpiryMonth,
                    ExpiryYear = paymentDto.Source.ExpiryYear
                }
            };
        }

        public static PaymentDto ToPaymentDto(this Payment payment)
        {
            return new PaymentDto
            {
                Amount = payment.Amount,
                Approved = payment.Approved,
                Currency = payment.Currency,
                Id = payment.Id,
                Status = payment.Status,
                Source = payment.Card.ToCardDto()
            };
        }

		public static CreatePaymentOptions ToCreatePaymentOptions(this CreatePaymentRequest createPaymentRequest)
		{
			if (createPaymentRequest == null)
			{
				return new CreatePaymentOptions();
			}

			var createPaymentOptions = new CreatePaymentOptions
			{
				Amount = createPaymentRequest.Amount,
				Currency = createPaymentRequest.Currency
			};

			if (createPaymentRequest.Source != null)
			{
				createPaymentOptions.Source = new PaymentRequestSource
				{
					CVV = createPaymentRequest.Source.CVV,
					ExpiryMonth = createPaymentRequest.Source.ExpiryMonth,
					ExpiryYear = createPaymentRequest.Source.ExpiryYear,
					Number = createPaymentRequest.Source.Number,
					Type = createPaymentRequest.Source.Type
				};
			}

			return createPaymentOptions;
		}
	}
}
