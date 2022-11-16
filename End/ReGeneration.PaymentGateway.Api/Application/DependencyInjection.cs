using ReGeneration.PaymentGateway.Api.Application.Common.Interfaces;
using ReGeneration.PaymentGateway.Api.Application.Services;

namespace ReGeneration.PaymentGateway.Api.Application
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddApplication(this IServiceCollection services)
		{
			services.AddScoped<IPaymentGatewayService, PaymentGatewayService>();
			services.AddSingleton<ICardValidationService, CreditCardValidationService>();

			return services;
		}
	}
}
