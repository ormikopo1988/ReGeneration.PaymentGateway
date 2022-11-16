using Microsoft.AspNetCore.Mvc;
using ReGeneration.PaymentGateway.Api.Application.Common.Interfaces;
using ReGeneration.PaymentGateway.Api.Application.Payments.Extensions;
using ReGeneration.PaymentGateway.Api.Contracts.Requests;
using ReGeneration.PaymentGateway.Api.Contracts.Responses;

namespace ReGeneration.PaymentGateway.Api.Controllers
{
	/// <summary>
	/// The controller responsible for Cards resource
	/// </summary>
	[Route("api/cards")]
	public class CardsController : ApiControllerBase
	{
		/// <summary>
		/// The CardsController constructor
		/// </summary>
		/// <param name="mediator"></param>
		public CardsController(IPaymentGatewayService paymentGatewayService)
			: base(paymentGatewayService)
		{
		}

		/// <summary>
		/// Returns the details of the requested payment
		/// </summary>
		/// <param name="id"></param>
		/// <param name="cancellationToken"></param>
		/// <returns>An ActionResult of IList of PaymentResponse</returns>
		/// <response code="200">Returns the requested payment</response>
		/// <response code="404">Returns 404 if the payment was not found</response>
		/// <response code="500">Returns 500 if error has occured on the API side</response>
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		[HttpGet("{id}/payments", Name = "GetPaymentsOfSpecificCard")]
		public async Task<ActionResult<IList<PaymentBaseResponse>>> GetPaymentsOfSpecificCard(Guid id, CancellationToken cancellationToken)
		{
			var payments = await PaymentGatewayService.GetPaymentsOfSpecificCard(id, cancellationToken);

			return Ok(payments.Data.ToListOfPaymentResponse());
		}
	}
}
