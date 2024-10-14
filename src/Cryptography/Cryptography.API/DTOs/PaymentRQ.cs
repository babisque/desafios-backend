namespace Cryptography.API.DTOs;

public class PaymentRQ
{
    public string UserDocument { get; set; }
    public string CreditCardToken { get; set; }
    public long Value { get; set; }
}