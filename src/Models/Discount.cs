namespace InterWorks.Discounts.Models;

public class Discount
{
    public string DiscountName { get; }
    public decimal DiscountAmount { get; }

    public Discount(string name, decimal discountAmount)
    {
        DiscountName = name;
        DiscountAmount = discountAmount;
    }
}