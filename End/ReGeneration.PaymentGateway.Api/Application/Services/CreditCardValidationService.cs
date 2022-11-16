using ReGeneration.PaymentGateway.Api.Application.Common.Interfaces;
using System.Text.RegularExpressions;

namespace ReGeneration.PaymentGateway.Api.Application.Services
{
	/// <summary>
	/// The service responsible for validating credit cards.
	/// It will validate cards based on Luhn Algorithm validity, whether they are in the supported card types
	/// and finally if card cvv is valid according to the card type it belongs.
	/// </summary>
	public class CreditCardValidationService : ICardValidationService
	{
		private static readonly Dictionary<string, Regex> _acceptedCreditCards = new()
		{
			{ "visa", new Regex(@"^4[0-9]{12}(?:[0-9]{3})?$") },
			{ "mastercard", new Regex(@"^5[1-5][0-9]{14}$|^2(?:2(?:2[1-9]|[3-9][0-9])|[3-6][0-9][0-9]|7(?:[01][0-9]|20))[0-9]{12}$") },
			{ "amex", new Regex(@"^3[47][0-9]{13}$") },
			{ "discover", new Regex(@"^65[4-9][0-9]{13}|64[4-9][0-9]{13}|6011[0-9]{12}|(622(?:12[6-9]|1[3-9][0-9]|[2-8][0-9][0-9]|9[01][0-9]|92[0-5])[0-9]{10})$") },
			{ "diners_club", new Regex(@"^3(?:0[0-5]|[68][0-9])[0-9]{11}$") },
			{ "jcb", new Regex(@"^(?:2131|1800|35[0-9]{3})[0-9]{11}$") },
		};

		/// <summary>
		/// Credit card number and cvv validations:
		///   1. Validate card number based on Luhn Algorithm
		///   2. Validate card number belongs in one of the supported card types
		///   3. Validate card cvv length according to the respective card type it belongs
		/// </summary>
		/// <param name="cardNumber"></param>
		/// <param name="cvv"></param>
		/// <returns>Whether the provided card details (cardNumber, cvv) are valid or not (boolean)</returns>
		public bool ValidateCardDetails(string cardNumber, string cvv)
		{
			if (string.IsNullOrEmpty(cardNumber) || string.IsNullOrEmpty(cvv)) return false;

			return IsCardNumberValid(cardNumber)
				&& IsCardNumberInSupportedCardTypes(cardNumber)
				&& IsCardCvvValid(cardNumber, cvv);
		}

		private static bool IsCardNumberValid(string cardNumber)
		{
			// remove all non digit characters
			var value = new string(cardNumber.Where(c => char.IsDigit(c)).ToArray());
			var sum = 0;
			var shouldDouble = false;

			// loop through values starting at the rightmost side
			for (var i = value.Length - 1; i >= 0; i--)
			{
				var digit = value[i] - '0';

				if (shouldDouble)
				{
					if ((digit *= 2) > 9) digit -= 9;
				}

				sum += digit;
				shouldDouble = !shouldDouble;
			}

			return (sum % 10) == 0;
		}

		private static bool IsCardNumberInSupportedCardTypes(string cardNumber)
		{
			// remove all non digit characters
			var value = new string(cardNumber.Where(c => char.IsDigit(c)).ToArray());

			foreach (var keyValuePair in _acceptedCreditCards)
			{
				if (keyValuePair.Value.IsMatch(value))
				{
					return true;
				}
			}

			return false;
		}

		private static bool IsCardCvvValid(string cardNumber, string cardCvv)
		{
			// remove all non digit characters
			var creditCard = new string(cardNumber.Where(c => char.IsDigit(c)).ToArray());
			var cvv = new string(cardCvv.Where(c => char.IsDigit(c)).ToArray());

			// american express has a 4 digits cvv
			if (_acceptedCreditCards["amex"].IsMatch(creditCard))
			{
				if (new Regex(@"^\d{4}$").IsMatch(cvv))
				{
					return true;
				}
			}
			else if (new Regex(@"^\d{3}$").IsMatch(cvv)) // other card types have a 3 digit cvv
			{
				return true;
			}

			return false;
		}
	}
}
