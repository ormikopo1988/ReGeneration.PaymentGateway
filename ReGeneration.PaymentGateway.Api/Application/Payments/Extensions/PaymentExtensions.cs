using ReGeneration.PaymentGateway.Api.Application.Payments.Dtos;
using ReGeneration.PaymentGateway.Api.Application.Payments.Options;
using ReGeneration.PaymentGateway.Api.Contracts.Requests;
using ReGeneration.PaymentGateway.Api.Contracts.Responses;
using ReGeneration.PaymentGateway.Api.Entities;

namespace ReGeneration.PaymentGateway.Api.Application.Payments.Extensions
{
    public static class PaymentExtensions
    {
        public static PaymentWithCardResponse ToPaymentIncludingCardResponse(this PaymentWithCardDto paymentIncludingCardDto)
        {
            return new PaymentWithCardResponse
            {
                Amount = paymentIncludingCardDto.Amount,
                Currency = paymentIncludingCardDto.Currency,
                Id = paymentIncludingCardDto.Id,
                Approved = paymentIncludingCardDto.Approved,
                Status = paymentIncludingCardDto.Status,
                Source = new CardDetails
                {
                    MaskedCVV = paymentIncludingCardDto.Source.MaskedCVV,
                    MaskedCardNumber = paymentIncludingCardDto.Source.MaskedCardNumber,
                    ExpiryMonth = paymentIncludingCardDto.Source.ExpiryMonth,
                    ExpiryYear = paymentIncludingCardDto.Source.ExpiryYear
                }
            };
        }

		public static IList<PaymentBaseResponse> ToListOfPaymentResponse(this IList<PaymentBaseDto> paymentDtos)
		{
			var result = new List<PaymentBaseResponse>();

			if (paymentDtos is not null)
			{
				foreach (var paymentDto in paymentDtos)
				{
					result.Add(new PaymentBaseResponse
					{
						Amount = paymentDto.Amount,
						Currency = paymentDto.Currency,
						Id = paymentDto.Id,
						Approved = paymentDto.Approved,
						Status = paymentDto.Status
					});
				}
			}

			return result;
		}

		public static PaymentBaseDto ToPaymentDto(this Payment payment)
		{
			return new PaymentBaseDto
			{
				Amount = payment.Amount,
				Approved = payment.Approved,
				Currency = payment.Currency,
				Id = payment.Id,
				Status = payment.Status
			};
		}

		public static IList<PaymentBaseDto> ToListOfPaymentDtos(this IList<Payment> payments)
		{
			var result = new List<PaymentBaseDto>();

			if (payments is not null)
			{
				foreach (var payment in payments)
				{
					result.Add(payment.ToPaymentDto());
				}
			}

			return result;
		}

		public static PaymentWithCardDto ToPaymentIncludingCardDto(this Payment payment)
        {
            return new PaymentWithCardDto
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

			if (createPaymentRequest.Source is not null)
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
