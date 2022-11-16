using ReGeneration.PaymentGateway.Api.Starter.Application.Common.Interfaces;
using ReGeneration.PaymentGateway.Api.Starter.Application.Services;

namespace ReGeneration.PaymentGateway.Api.Starter.Application
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddApplication(this IServiceCollection services)
		{
			services.AddSingleton<ICardValidationService, CreditCardValidationService>();

			return services;
		}
	}
}
