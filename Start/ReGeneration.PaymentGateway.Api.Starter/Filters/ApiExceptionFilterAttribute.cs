using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using ReGeneration.PaymentGateway.Api.Starter.Application.Common.Exceptions;

namespace ReGeneration.PaymentGateway.Api.Starter.Filters
{
	/// <summary>
	/// Global exception handler for our API registered as ExceptionFilterAttribute.
	/// </summary>
	public class ApiExceptionFilterAttribute : ExceptionFilterAttribute
	{
		private readonly IDictionary<Type, Action<ExceptionContext>> _exceptionHandlers;

		/// <summary>
		/// The constructor of ApiExceptionFilterAttribute
		/// </summary>
		public ApiExceptionFilterAttribute()
		{
			// Register known exception types and handlers.
			_exceptionHandlers = new Dictionary<Type, Action<ExceptionContext>>
			{
				{ typeof(ValidationException), HandleValidationException },
				{ typeof(NotFoundException), HandleNotFoundException }
			};
		}

		/// <summary>
		/// OnException will get called whenever there is an exception thrown in our system and will respectively handle
		/// the exception based on its type.
		/// </summary>
		/// <param name="context"></param>
		public override void OnException(ExceptionContext context)
		{
			HandleException(context);

			base.OnException(context);
		}

		private void HandleException(ExceptionContext context)
		{
			Type type = context.Exception.GetType();

			if (_exceptionHandlers.ContainsKey(type))
			{
				_exceptionHandlers[type].Invoke(context);
				return;
			}

			HandleUnknownException(context);
		}

		private static void HandleValidationException(ExceptionContext context)
		{
			var exception = (ValidationException)context.Exception;

			var details = new ValidationProblemDetails(exception.Errors)
			{
				Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1"
			};

			context.Result = new BadRequestObjectResult(details);

			context.ExceptionHandled = true;
		}

		private static void HandleNotFoundException(ExceptionContext context)
		{
			var exception = (NotFoundException)context.Exception;

			var details = new ProblemDetails
			{
				Type = "https://tools.ietf.org/html/rfc7231#section-6.5.4",
				Title = "The specified resource was not found.",
				Detail = exception.Message
			};

			context.Result = new NotFoundObjectResult(details);

			context.ExceptionHandled = true;
		}

		private static void HandleUnknownException(ExceptionContext context)
		{
			var details = new ProblemDetails
			{
				Status = StatusCodes.Status500InternalServerError,
				Title = "An error occurred while processing your request.",
				Type = "https://tools.ietf.org/html/rfc7231#section-6.6.1"
			};

			context.Result = new ObjectResult(details)
			{
				StatusCode = StatusCodes.Status500InternalServerError
			};

			context.ExceptionHandled = true;
		}
	}
}
