using InterWorks.Discounts.Strategies;

namespace InterWorks.Discounts.Models;

public class Order
{
    public List<IDiscountStrategy>? Discounts { get; init; }
    public decimal TotalAmount { get; init; }
}