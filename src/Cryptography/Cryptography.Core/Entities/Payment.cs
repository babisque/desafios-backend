namespace Cryptography.Entities;

public class Payment
{
    public long Id { get; set; }
    public string UserDocument { get; set; }
    public string CreditCardToken { get; set; }
    public long Value { get; set; }
}