using Microsoft.AspNetCore.Mvc;
using ReGeneration.PaymentGateway.Api.Application.Common.Interfaces;
using ReGeneration.PaymentGateway.Api.Application.Payments.Extensions;
using ReGeneration.PaymentGateway.Api.Contracts.Requests;
using ReGeneration.PaymentGateway.Api.Contracts.Responses;

namespace ReGeneration.PaymentGateway.Api.Controllers
{
	/// <summary>
	/// The controller responsible for Payments resource
	/// </summary>
	[Produces("application/json")]
	[ApiController]
	[Route("api/payments")]
	public class PaymentsController : ControllerBase
	{
		private readonly IPaymentGatewayService _paymentGatewayService;

		/// <summary>
		/// The PaymentsController constructor
		/// </summary>
		/// <param name="paymentGatewayService"></param>
		public PaymentsController(IPaymentGatewayService paymentGatewayService)
		{
			_paymentGatewayService = paymentGatewayService;
		}

		/// <summary>
		/// Create a payment
		/// </summary>
		/// <param name="request"></param>
		/// <param name="cancellationToken"></param>
		/// <returns>An ActionResult of CreatePaymentResponse</returns>
		/// <response code="201">Returns the created payment id</response>
		/// <response code="400">Returns 400 if the provided payment request details were invalid</response>
		/// <response code="500">Returns 500 if error has occured on the API side</response>
		[Consumes("application/json")]
		[ProducesResponseType(StatusCodes.Status201Created)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		[HttpPost]
		public async Task<ActionResult<PaymentResponse>> Create(CreatePaymentRequest request, CancellationToken cancellationToken)
		{
			var newPaymentResult = await _paymentGatewayService.CreatePaymentAsync(request.ToCreatePaymentOptions(), cancellationToken);

			return CreatedAtRoute("GetPaymentById", new { id = newPaymentResult.Data.Id }, newPaymentResult.Data.ToPaymentResponse());
		}

		/// <summary>
		/// Returns no content if the deletion of the payment has been successful.
		/// </summary>
		/// <param name="id"></param>
		/// <param name="cancellationToken"></param>
		/// <returns>An ActionResult of CreatePaymentResponse</returns>
		/// <response code="204">Returns 204 if the payment was successfully deleted</response>
		/// <response code="404">Returns 404 if the payment was not found</response>
		/// <response code="500">Returns 500 if error has occured on the API side</response>
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		[HttpDelete("{id}", Name = "DeletePaymentById")]
		public async Task<ActionResult> Delete(Guid id, CancellationToken cancellationToken)
		{
			await _paymentGatewayService.DeletePaymentAsync(id, cancellationToken);

			return NoContent();
		}

		/// <summary>
		/// Returns the details of the requested payment
		/// </summary>
		/// <param name="id"></param>
		/// <param name="cancellationToken"></param>
		/// <returns>An ActionResult of CreatePaymentResponse</returns>
		/// <response code="200">Returns the requested payment</response>
		/// <response code="404">Returns 404 if the payment was not found</response>
		/// <response code="500">Returns 500 if error has occured on the API side</response>
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		[HttpGet("{id}", Name = "GetPaymentById")]
		public async Task<ActionResult<PaymentResponse>> Get(Guid id, CancellationToken cancellationToken)
		{
			var payment = await _paymentGatewayService.GetPaymentAsync(id, cancellationToken);

			return Ok(payment.Data.ToPaymentResponse());
		}
	}
}
