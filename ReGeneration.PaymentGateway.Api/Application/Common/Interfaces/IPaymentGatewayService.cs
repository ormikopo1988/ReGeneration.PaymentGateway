using ReGeneration.PaymentGateway.Api.Application.Common.Models;
using ReGeneration.PaymentGateway.Api.Application.Payments.Dtos;
using ReGeneration.PaymentGateway.Api.Application.Payments.Options;

namespace ReGeneration.PaymentGateway.Api.Application.Common.Interfaces
{
    public interface IPaymentGatewayService
	{
		Task<Result<PaymentWithCardDto>> CreatePaymentAsync(CreatePaymentOptions createPaymentOptions, CancellationToken cancellationToken);
		Task<Result<PaymentWithCardDto>> GetPaymentAsync(Guid paymentId, CancellationToken cancellationToken);
		Task<Result<IList<PaymentBaseDto>>> GetPaymentsOfSpecificCard(Guid cardId, CancellationToken cancellationToken);
		Task<bool> DeletePaymentAsync(Guid paymentId, CancellationToken cancellationToken);
	}
}
