namespace ReGeneration.PaymentGateway.Api.Application.Payments.Dtos
{
    public class CardDto
    {
        public string MaskedCardNumber { get; set; } = default!;

        public int ExpiryMonth { get; set; }

        public int ExpiryYear { get; set; }

        public string MaskedCVV { get; set; } = default!;
    }
}
