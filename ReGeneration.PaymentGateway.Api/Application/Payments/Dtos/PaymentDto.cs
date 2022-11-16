namespace ReGeneration.PaymentGateway.Api.Application.Payments.Dtos
{
    public class PaymentDto
    {
        public Guid Id { get; set; }

        public decimal Amount { get; set; }

        public string Currency { get; set; } = default!;

        public bool Approved { get; set; }

        public string Status { get; set; } = default!;

        public CardDto Source { get; set; } = default!;
    }
}
