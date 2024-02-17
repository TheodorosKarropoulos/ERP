namespace InterWorks.Discounts.Models;

public readonly struct Currency
{
    public decimal Amount { get; }
    public string CurrencyCode { get; }
    
    public Currency(decimal amount, string currencyCode)
    {
        Amount = amount;
        CurrencyCode = currencyCode;
    }

    public override string ToString()
    {
        return $"{Amount} {CurrencyCode}";
    }
}