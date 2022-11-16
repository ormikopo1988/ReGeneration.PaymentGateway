using ReGeneration.PaymentGateway.Api.Application.Common.Models;
using ReGeneration.PaymentGateway.Api.Application.Payments.Dtos;
using ReGeneration.PaymentGateway.Api.Application.Payments.Options;

namespace ReGeneration.PaymentGateway.Api.Application.Common.Interfaces
{
    public interface IPaymentGatewayService
	{
		Task<Result<PaymentDto>> CreatePaymentAsync(CreatePaymentOptions createPaymentOptions, CancellationToken cancellationToken);
		Task<Result<PaymentDto>> GetPaymentAsync(Guid paymentId, CancellationToken cancellationToken);
		Task<bool> DeletePaymentAsync(Guid paymentId, CancellationToken cancellationToken);
	}
}
