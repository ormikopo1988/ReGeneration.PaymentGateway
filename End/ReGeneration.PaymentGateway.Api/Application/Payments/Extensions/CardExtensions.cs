using ReGeneration.PaymentGateway.Api.Application.Payments.Dtos;
using ReGeneration.PaymentGateway.Api.Domain;

namespace ReGeneration.PaymentGateway.Api.Application.Payments.Extensions
{
    public static class CardExtensions
    {
        public static CardDto ToCardDto(this Card card)
        {
            return new CardDto
            {
                ExpiryMonth = card.ExpiryMonth,
                ExpiryYear = card.ExpiryYear,
                MaskedCardNumber = MaskCardNumber(card.CardNumber, 6, 4),
                MaskedCVV = MaskCVV(card.CVV)
            };
        }

        private static string MaskCardNumber(string cardNumber, int lengthFromStart, int indexFromEnd)
        {
            var firstDigits = cardNumber[..lengthFromStart];
            var lastDigits = cardNumber.Substring(cardNumber.Length - indexFromEnd, indexFromEnd);

            var requiredMask = new string('*', cardNumber.Length - firstDigits.Length - lastDigits.Length);

            return $"{firstDigits}{requiredMask}{lastDigits}";
        }

        private static string MaskCVV(string cvv)
        {
            var cvvLength = cvv.Length;
            return cvv[cvvLength..].PadLeft(cvvLength, '*');
        }
    }
}
