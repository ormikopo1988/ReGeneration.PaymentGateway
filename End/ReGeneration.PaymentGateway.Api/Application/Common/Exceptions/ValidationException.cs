﻿namespace ReGeneration.PaymentGateway.Api.Application.Common.Exceptions
{
	public class ValidationException : Exception
	{
		public IDictionary<string, string[]> Errors { get; }

		public ValidationException()
			: base("One or more validation failures have occurred.")
		{
			Errors = new Dictionary<string, string[]>();
		}

		public ValidationException(string failureCode, string failureMessage)
			: this()
		{
			Errors.Add(failureCode, new string[] { failureMessage });
		}
	}
}
