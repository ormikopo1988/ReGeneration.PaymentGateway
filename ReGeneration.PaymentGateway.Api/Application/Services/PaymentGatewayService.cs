using Microsoft.EntityFrameworkCore;
using ReGeneration.PaymentGateway.Api.Application.Common.Exceptions;
using ReGeneration.PaymentGateway.Api.Application.Common.Interfaces;
using ReGeneration.PaymentGateway.Api.Application.Common.Models;
using ReGeneration.PaymentGateway.Api.Application.Payments.Dtos;
using ReGeneration.PaymentGateway.Api.Application.Payments.Extensions;
using ReGeneration.PaymentGateway.Api.Application.Payments.Options;
using ReGeneration.PaymentGateway.Api.Contracts.Requests;
using ReGeneration.PaymentGateway.Api.Entities;

namespace ReGeneration.PaymentGateway.Api.Application.Services
{
	/// <summary>
	/// The PaymentGatewayService to orchestrate the payment creation flow
	/// </summary>
	public class PaymentGatewayService : IPaymentGatewayService
	{
		private readonly IApplicationDbContext _applicationDbContext;
		private readonly ICardValidationService _cardValidationService;

		/// <summary>
		/// The PaymentGatewayService constructor.
		/// </summary>
		/// <param name="applicationDbContext"></param>
		/// <param name="cardValidationService"></param>
		public PaymentGatewayService(IApplicationDbContext applicationDbContext, ICardValidationService cardValidationService)
		{
			_applicationDbContext = applicationDbContext;
			_cardValidationService = cardValidationService;
		}

		/// <summary>
		/// Payment creation flow:
		/// - Validate payment's respective card details by calling the ValidateCardDetails of the ICardValidationService. In case of success we continue the flow, in case of error we immediately return a 400 bad request to the client, without saving the payment in the db.
		/// - If card is already saved in the db, the system firstly validates that the details in the new payment request match the ones we have stored during a previous valid payment transaction with the same saved card. If not, the system immediately returns a 400 bad request to the client.
		/// - Save the card with its respective details, if it was not already existing in our db.
		/// - Save the payment in our db.
		/// </summary>
		/// <param name="createPaymentOptions"></param>
		/// <param name="cancellationToken"></param>
		/// <returns>A Task of type Result<PaymentIncludingCardDto></returns>
		/// <exception cref="ValidationException"></exception>
		public async Task<Result<PaymentWithCardDto>> CreatePaymentAsync(CreatePaymentOptions createPaymentOptions, CancellationToken cancellationToken)
		{
			// TODO - Validate amount, currency, source, etc...
			var isCardValid = _cardValidationService.ValidateCardDetails(createPaymentOptions.Source.Number, createPaymentOptions.Source.CVV);

			if (!isCardValid)
			{
				throw new ValidationException(
					Enum.GetName(typeof(ErrorCode), ErrorCode.InvalidCard)!,
					"Invalid card number / CVV pair provided."
				);
			}

			var existingCard = await FetchCardFromDbIfExistsAndValidateAgainstNewPaymentRequestSourceDetailsAsync(createPaymentOptions.Source, cancellationToken);

			Guid cardIdToUseForPayment;
			if (existingCard != null)
			{
				cardIdToUseForPayment = existingCard.Id;
			}
			else
			{
				var newCard = await SaveCardAsync(createPaymentOptions.Source, cancellationToken);
				cardIdToUseForPayment = newCard.Id;
			}

			var payment = await SavePaymentAsync(
				createPaymentOptions,
				cardIdToUseForPayment,
				cancellationToken
			);

			await _applicationDbContext.SaveChangesAsync(cancellationToken);

			return new Result<PaymentWithCardDto>
			{
				Data = payment.ToPaymentIncludingCardDto()
			};
		}

		/// <summary>
		/// Deletes a specific payment.
		/// </summary>
		/// <param name="paymentId"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		/// <exception cref="NotImplementedException"></exception>
		public async Task<bool> DeletePaymentAsync(Guid paymentId, CancellationToken cancellationToken)
		{
			var payment = await _applicationDbContext
				.Payments
				.AsNoTracking()
				.SingleOrDefaultAsync(p => p.Id == paymentId, cancellationToken);

			if (payment == null)
			{
				throw new NotFoundException(nameof(Payment), paymentId);
			}

			_applicationDbContext.Payments.Remove(payment);

			await _applicationDbContext.SaveChangesAsync(cancellationToken);

			return true;
		}

		/// <summary>
		/// Get specific payment from db
		/// </summary>
		/// <param name="paymentId"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		/// <exception cref="NotFoundException"></exception>
		public async Task<Result<PaymentWithCardDto>> GetPaymentAsync(Guid paymentId, CancellationToken cancellationToken)
		{
			var payment = await _applicationDbContext
				.Payments
				.AsNoTracking()
				.Include(p => p.Card)
				.SingleOrDefaultAsync(p => p.Id == paymentId, cancellationToken);

			if (payment == null)
			{
				throw new NotFoundException(nameof(Payment), paymentId);
			}

			return new Result<PaymentWithCardDto>
			{
				Data = payment.ToPaymentIncludingCardDto()
			};
		}

		public async Task<Result<IList<PaymentBaseDto>>> GetPaymentsOfSpecificCard(Guid cardId, CancellationToken cancellationToken)
		{
			var card = await _applicationDbContext
				.Cards
				.AsNoTracking()
				.Include(p => p.Payments)
				.SingleOrDefaultAsync(p => p.Id == cardId, cancellationToken);

			if (card == null)
			{
				throw new NotFoundException(nameof(Card), cardId);
			}

			return new Result<IList<PaymentBaseDto>>
			{
				Data = card.Payments.ToListOfPaymentDtos()
			};
		}

		private async Task<Card?> FetchCardFromDbIfExistsAndValidateAgainstNewPaymentRequestSourceDetailsAsync(PaymentRequestSource paymentRequestSource, CancellationToken cancellationToken)
		{
			var existingCardFromDb = await _applicationDbContext.Cards.SingleOrDefaultAsync(c => c.CardNumber == paymentRequestSource.Number, cancellationToken);

			if (existingCardFromDb == null)
			{
				return null;
			}

			if (!ArePaymentRequestSourceDetailsEqualToExistingCardDetailsFromDb(paymentRequestSource, existingCardFromDb))
			{
				throw new ValidationException(
					Enum.GetName(typeof(ErrorCode), ErrorCode.MismatchingCardDetails)!,
					"The details (Expiry month, expiry year or CVV) of the card you provided are not consistent with the ones that we have saved from a previous transaction."
				);
			}

			return existingCardFromDb;
		}

		private async Task<Card> SaveCardAsync(PaymentRequestSource paymentRequestSource, CancellationToken cancellationToken)
		{
			var card = new Card
			{
				CardNumber = paymentRequestSource.Number,
				CVV = paymentRequestSource.CVV,
				ExpiryMonth = Convert.ToInt16(paymentRequestSource.ExpiryMonth),
				ExpiryYear = Convert.ToInt16(paymentRequestSource.ExpiryYear),
			};

			await _applicationDbContext.Cards.AddAsync(card, cancellationToken);

			return card;
		}

		private async Task<Payment> SavePaymentAsync(CreatePaymentOptions createPaymentOptions, Guid cardId, CancellationToken cancellationToken)
		{
			var payment = new Payment
			{
				Amount = createPaymentOptions.Amount,
				Currency = createPaymentOptions.Currency,
				CardId = cardId,
				Approved = true,
				Status = "Valid"
			};

			await _applicationDbContext.Payments.AddAsync(payment, cancellationToken);

			return payment;
		}

		private static bool ArePaymentRequestSourceDetailsEqualToExistingCardDetailsFromDb(PaymentRequestSource paymentRequestSource, Card existingCardFromDb)
		{
			return existingCardFromDb.ExpiryYear == paymentRequestSource.ExpiryYear
				&& existingCardFromDb.ExpiryMonth == paymentRequestSource.ExpiryMonth
				&& existingCardFromDb.CVV == paymentRequestSource.CVV;
		}
	}
}
