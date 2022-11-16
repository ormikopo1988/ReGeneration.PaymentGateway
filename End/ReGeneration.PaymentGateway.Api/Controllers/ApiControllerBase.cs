using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Mvc;
using ReGeneration.PaymentGateway.Api.Application.Common.Interfaces;

namespace ReGeneration.PaymentGateway.Api.Controllers
{
	/// <summary>
	/// ApiControllerBase is an abstract base class that all the controllers of our API
	/// will inherit from. This is done in order for every controller to have access 
	/// to the IPaymentGatewayService and not having to inject it in every 
	/// single controller we create.
	/// </summary>
	[Produces("application/json")]
	[ApiController]
	public abstract class ApiControllerBase : ControllerBase
	{
		/// <summary>
		/// The IPaymentGatewayService property.
		/// </summary>
		protected readonly IPaymentGatewayService PaymentGatewayService;

		/// <summary>
		/// The ApiControllerBase constructor.
		/// </summary>
		/// <param name="paymentGatewayService"></param>
		protected ApiControllerBase(IPaymentGatewayService paymentGatewayService)
		{
			PaymentGatewayService = paymentGatewayService;
		}
	}
}
