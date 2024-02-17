namespace InterWorks.Discounts.Models;

public class DiscountApplicationResult
{
    public decimal TotalAmount { get; init; }
    public decimal DiscountAmount => Discounts.Sum(x => x.DiscountAmount);
    public decimal AmountAfterDiscount => TotalAmount - DiscountAmount;
    public List<Discount> Discounts { get; } = new();
}